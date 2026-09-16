using System;

namespace Itdear.Infraestructura.Transversal.Adaptador
{
    /// <summary>
    /// Traductor entre entidades del dominio y DTOs. Abstrae la libreria de mapeo
    /// concreta que se este usando.
    /// </summary>
    public interface ITypeAdapter
    {
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new();

        TTarget Adapt<TTarget>(object source) where TTarget : class;

        object Adapt(object source, Type sourceType, Type targetType);
    }

    /// <summary>
    /// Fabrica del adaptador de tipos.
    /// </summary>
    public interface ITypeAdapterFactory
    {
        ITypeAdapter Create();
    }

    /// <summary>
    /// Punto de acceso estatico al adaptador configurado en el arranque.
    ///
    /// Los metodos de extension ProjectedAs / ProjectedAsCollection se invocan desde
    /// entidades y colecciones sueltas, donde no hay inyeccion de dependencias
    /// disponible; por eso la fabrica se registra una sola vez aqui.
    /// </summary>
    public static class TypeAdapterFactory
    {
        private static ITypeAdapterFactory _currentFactory;

        /// <summary>Registra la fabrica que se usara en toda la aplicacion.</summary>
        public static void SetCurrent(ITypeAdapterFactory factory)
        {
            _currentFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>Crea un adaptador. Falla si no se llamo antes a <see cref="SetCurrent"/>.</summary>
        public static ITypeAdapter CreateAdapter()
        {
            if (_currentFactory == null)
            {
                throw new InvalidOperationException(
                    "No se ha configurado el adaptador de tipos. Invoque TypeAdapterFactory.SetCurrent en el arranque de la aplicacion.");
            }

            return _currentFactory.Create();
        }

        /// <summary>Indica si ya hay una fabrica configurada.</summary>
        public static bool IsConfigured => _currentFactory != null;
    }
}
