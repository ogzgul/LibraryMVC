using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMVC.Models
{

    public class Author
    {
        public long Id { get; set; }

        [Column(TypeName="nchar(100)")]
        [StringLength(100, ErrorMessage = "En az 2,en fazla 100 karakter")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [DisplayName("Doğum Tarihi (opsiyonel)")]
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; } // zorunlu olmasın diye ? koyuldu.

        [DisplayName("Ölüm Tarihi (opsiyonel)")]
        public DateTime? DeathDate { get; set; }

        [DisplayName("Cinsiyet (opsiyonel)")]
        public bool? Gender { get; set; }
    }
}
