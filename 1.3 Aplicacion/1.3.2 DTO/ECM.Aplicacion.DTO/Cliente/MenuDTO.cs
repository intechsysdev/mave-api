using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Cliente
{
    public class MenuDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SubName { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public IEnumerable<MenuDTO> Options { get; set; }
    }
}
