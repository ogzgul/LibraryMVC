using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Xunit.Sdk;

namespace LibraryMVC.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Kimlik Numarası")]
        [Range(10000000000, 99999999999, ErrorMessage = "Bu alan 11 karakter olmalıdır")]
        public long Id { get; set; }

        [Column(TypeName = "nchar(100)")]
        [StringLength(100, ErrorMessage = "En az 2,en fazla 100 karakter")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Kütüphane")]
        public int LibraryId { get; set; }

        [ForeignKey("LibraryId")]
        [DisplayName("Kütüphane")]  
        public Library? Library { get; set; }

    }
}
