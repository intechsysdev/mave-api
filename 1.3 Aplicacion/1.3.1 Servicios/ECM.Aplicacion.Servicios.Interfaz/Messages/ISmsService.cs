using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.Messages
{
    public interface ISmsService
    {
        Task Send(string keyApp, string from, string to, string text, bool generateException = false);
    }
}
