using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RumahMakanPadang.dal.Models
{
    public class Masakan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        [StringLength(100)]
        public string Nama { get; set; }

        public int Harga { get; set; }

        [StringLength(255)]
        public string Deskripsi { get; set; }

        public string ChefKTP { get; set; }
        public Chef Chef { get; set; }

        public Masakan()
        {
            DateAdded = DateTime.UtcNow;
        }

    }
}
