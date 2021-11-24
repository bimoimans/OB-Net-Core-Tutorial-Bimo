using RumahMakanPadang.api.Masakan.DTO;
using System.Collections.Generic;

namespace RumahMakanPadang.api.Chef.DTO
{
    public class ChefDTO
    {

        public string Nama { get; set; }
        public int Umur { get; set; }
        public string Spesialisasi { get; set; }
        public string KTP { get; set; }

    }

    public class ChefWithMasakanDTO : ChefDTO
    {
        public List<MasakanDTO> Masakans { get; set; }
    }
    
}
