using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Razor.Hosting;
using System.Reflection;
using System.ComponentModel;

namespace LibraryMVC.Models
{
    public class City
    {   
        [Key] //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  //Identity kapatma
        [Range(1,81,ErrorMessage ="1 ile 81 arasında bir değer girin")]
        [Required(ErrorMessage ="Bu alan zorunludur.")]
        [DisplayName("Plaka Kodu")]
        public byte PlateCode { get; set; }

        [Column(TypeName = "NCHAR(20)")] // veritabanı 
        [Required(ErrorMessage ="Bu alan zorunludur.")] // Ekrana 
        [StringLength(20,MinimumLength =3,ErrorMessage ="En az 3,en fazla 20 karakter")] // ekrana
        [DisplayName("İsim")]
        public string Name { get; set; }
        
    }
}
