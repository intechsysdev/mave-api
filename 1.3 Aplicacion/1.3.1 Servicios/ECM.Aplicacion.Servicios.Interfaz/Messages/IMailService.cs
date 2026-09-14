using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.Messages
{
    public interface IMailService
    {
        Task Send(string keyApp, string from, string to, string subject, int templateId, object parameters, bool intermediateReport = true, bool generateException = false);
    }
}
