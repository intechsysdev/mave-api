

using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobRperfilesDTO
    {
        public string RperCmpy { get; set; }
        public string RperCodPer { get; set; }
        public string RperDesPer { get; set; }
        public List<MobRmenuPpalDTO> MobRmenuPpal { get; set; }
    }
}
