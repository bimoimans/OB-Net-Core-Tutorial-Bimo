using RumahMakanPadang.api.Chef.DTO;

namespace RumahMakanPadang.api.Masakan.DTO
{
    public class MasakanDTO
    {
        public string Nama { get; set; }
        public int Harga { get; set; }
        public string Deskripsi { get; set; }
    }

    public class MasakanWithChefDTO : MasakanDTO
    {
        public ChefDTO Chef { get; set; }
    }
}
