using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RumahMakanPadang.dal.Models
{
    public class Chef
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [StringLength(100)]
        public string Nama { get; set; }

        [Required]
        [StringLength(16)]
        public string KTP { get; set; }

        public int Umur { get; set; }

        [StringLength(255)]
        public string Spesialisasi { get; set; }

        public List<Masakan> Masakans { get; set; }

        public Chef()
        {
            DateAdded = DateTime.UtcNow;
        }

        
    }
}
