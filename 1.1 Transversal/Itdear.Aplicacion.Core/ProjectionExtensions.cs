using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Itdear.Infraestructura.Transversal.Adaptador;

namespace Itdear.Aplicacion.Core
{
    /// <summary>
    /// Proyeccion de entidades del dominio a DTOs usando el adaptador configurado en
    /// el arranque. Se expone como metodos de extension para poder encadenarlos al
    /// final de una consulta.
    /// </summary>
    public static class ProjectionExtensions
    {
        /// <summary>
        /// Proyecta un objeto al DTO indicado. Devuelve null si el origen es null, para
        /// que las consultas que no encuentran registro no obliguen a validar antes.
        /// </summary>
        public static TTarget ProjectedAs<TTarget>(this object source) where TTarget : class
        {
            if (source == null)
            {
                return null;
            }

            return TypeAdapterFactory.CreateAdapter().Adapt<TTarget>(source);
        }

        /// <summary>
        /// Proyecta una coleccion al DTO indicado. Una coleccion nula se proyecta como
        /// coleccion vacia.
        /// </summary>
        public static IEnumerable<TTarget> ProjectedAsCollection<TTarget>(this IEnumerable source)
            where TTarget : class
        {
            if (source == null)
            {
                return Enumerable.Empty<TTarget>();
            }

            var adapter = TypeAdapterFactory.CreateAdapter();

            var result = new List<TTarget>();

            foreach (var item in source)
            {
                result.Add(item == null ? null : adapter.Adapt<TTarget>(item));
            }

            return result;
        }
    }
}
