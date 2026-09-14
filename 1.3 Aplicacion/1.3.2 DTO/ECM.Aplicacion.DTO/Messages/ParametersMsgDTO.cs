namespace ECM.Aplicacion.DTO.Messages
{
    public class ParametersMsgDTO
    {
        public string Url { get; set; }
        public string FromMail { get; set; }
        public string Subject { get; set; }
        public string FromSms { get; set; }
        public string TextSms { get; set; }
        public int TemplateId { get; set; }
    }
}
