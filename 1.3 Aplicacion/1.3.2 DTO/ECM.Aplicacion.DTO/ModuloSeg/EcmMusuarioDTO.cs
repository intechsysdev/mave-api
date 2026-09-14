using System;

namespace ECM.Aplicacion.DTO.ModuloSeg
{
    public class EcmMusuarioDTO
    {
        public string MusuaEmprId { get; set; }
        public string MusuaCust { get; set; }
        public string MusuaSuccli { get; set; }
        public string MusuaName { get; set; }
        public string MusuaPassword { get; set; }
        public string MusuaPhone { get; set; }
        public string MusuaMail { get; set; }
        public string MusuaApprove { get; set; }
        public string MusuaPassEncrip { get; set; }
        public string MusuaToken { get; set; }
        public DateTime? MusuaExpirationToken { get; set; }
    }
}
