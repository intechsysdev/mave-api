using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Itdear.Infraestructura.Transversal.Adaptador;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Itdear.Transversal.NetCore.Adaptador
{
    /// <summary>
    /// Fabrica de adaptadores basada en AutoMapper.
    ///
    /// La configuracion se construye una sola vez y se comparte: recorrer los ensamblados
    /// en busca de perfiles es costoso y AutoMapper valida el mapa completo al crearlo.
    /// </summary>
    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory
    {
        private static readonly object SyncRoot = new object();

        private static IMapper _mapper;

        private readonly ILoggerFactory _loggerFactory;

        public AutomapperTypeAdapterFactory() : this(null)
        {
        }

        public AutomapperTypeAdapterFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
        }

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter(GetMapper());
        }

        private IMapper GetMapper()
        {
            if (_mapper != null)
            {
                return _mapper;
            }

            lock (SyncRoot)
            {
                if (_mapper == null)
                {
                    var profiles = DiscoverProfileTypes().ToArray();

                    var configuration = new MapperConfiguration(
                        cfg => cfg.AddProfiles(profiles.Select(Activator.CreateInstance).Cast<Profile>()));

                    _mapper = configuration.CreateMapper();
                }
            }

            return _mapper;
        }

        /// <summary>
        /// Busca los perfiles de AutoMapper en los ensamblados de la solucion.
        /// Los perfiles viven en la capa de DTO, a la que el ensamblado de entrada solo
        /// llega de forma transitiva, por lo que hay que recorrer el grafo de referencias
        /// forzando la carga de las que aun no esten en memoria.
        /// </summary>
        private static IEnumerable<Type> DiscoverProfileTypes()
        {
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var assemblies = new List<Assembly>();
            var pending = new Queue<Assembly>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(IsCandidate))
            {
                if (visited.Add(assembly.GetName().Name))
                {
                    assemblies.Add(assembly);
                    pending.Enqueue(assembly);
                }
            }

            var entry = Assembly.GetEntryAssembly();

            if (entry != null && visited.Add(entry.GetName().Name))
            {
                assemblies.Add(entry);
                pending.Enqueue(entry);
            }

            while (pending.Count > 0)
            {
                foreach (var reference in pending.Dequeue().GetReferencedAssemblies())
                {
                    if (IsFrameworkAssembly(reference.Name) || !visited.Add(reference.Name))
                    {
                        continue;
                    }

                    try
                    {
                        var loaded = Assembly.Load(reference);
                        assemblies.Add(loaded);
                        pending.Enqueue(loaded);
                    }
                    catch (System.Exception)
                    {
                        // Una referencia que no se puede cargar no debe impedir el arranque:
                        // solo significa que no aportara perfiles de mapeo.
                    }
                }
            }

            return assemblies.SelectMany(GetLoadableTypes)
                .Where(type => typeof(Profile).IsAssignableFrom(type) && !type.IsAbstract && type.IsPublic);
        }

        private static bool IsCandidate(Assembly assembly)
        {
            return !assembly.IsDynamic && !IsFrameworkAssembly(assembly.GetName().Name);
        }

        private static bool IsFrameworkAssembly(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }

            return name.StartsWith("System", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("Newtonsoft", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("Serilog", StringComparison.OrdinalIgnoreCase)
                || name.StartsWith("IBM", StringComparison.OrdinalIgnoreCase);
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(type => type != null);
            }
        }
    }

    /// <summary>
    /// Adaptador de tipos respaldado por un <see cref="IMapper"/> de AutoMapper.
    /// </summary>
    internal sealed class AutomapperTypeAdapter : ITypeAdapter
    {
        private readonly IMapper _mapper;

        public AutomapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return source == null ? null : _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return source == null ? null : _mapper.Map<TTarget>(source);
        }

        public object Adapt(object source, Type sourceType, Type targetType)
        {
            return source == null ? null : _mapper.Map(source, sourceType, targetType);
        }
    }
}
