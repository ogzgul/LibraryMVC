using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMVC.Models
{
    public class District
    {
        public short Id { get; set; }  // otomatik primary key olarak algılar
        
        [StringLength(20, MinimumLength = 2, ErrorMessage = "En az 2,en fazla 20 karakter")]
        [Required(ErrorMessage ="Bu alan zorunludur")]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Bu alan zorunludur.")]
        [DisplayName("Şehir")]
        public byte CityId { get; set; }
       
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [DisplayName("Nüfus")]
        [Range(1,5000000,ErrorMessage ="1 ile 5.000.000 arasında bir değer girin")]
        public int Population { get; set; }

        [ForeignKey("CityId")]
        [DisplayName("Şehir")]
        public City? City { get; set; }

    }
}
