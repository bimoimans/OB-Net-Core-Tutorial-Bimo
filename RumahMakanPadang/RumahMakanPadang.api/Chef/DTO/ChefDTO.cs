using RumahMakanPadang.api.Masakan.DTO;

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
        public MasakanDTO Masakan{ get; set; }
    }
    
}
