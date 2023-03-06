using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LibraryMVC.Models
{
    public class Library
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "En az 2,en fazla 30 karakter")]
        [DisplayName("İsim")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }

        [DisplayName("Kitap Sayısı")]
        [Range(1,50000,ErrorMessage ="1 ile 500 arasında bir değer giriniz.")]
        public int BookCount { get; set; }
       
        [DisplayName("Kapasite")]
        [Range(1, 500, ErrorMessage = "1 ile 500 arasında bir değer giriniz.")]
        public short Capacity { get; set; }
       
        [DisplayName("Kuruluş Tarihi")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Column(TypeName ="date")]
        public DateTime EstablishmentDate { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("İlçe")]
        public short DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        
        [DisplayName("İlçe")]
        public District? District { get; set; }

    }
}
