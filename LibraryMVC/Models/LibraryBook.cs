using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LibraryMVC.Models
{
    public class LibraryBook
    {
        [Column(TypeName ="char(13)")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(13,MinimumLength =13,ErrorMessage ="ISBN 13 karakter olmalıdır.")]
        [DisplayName("ISBN")]
        public string ISBN { get; set; }
        [ForeignKey("ISBN")]
        [DisplayName("Kitap")]
        public Book? Book { get; set; }

        [DisplayName("Kütüphane")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int LibraryId { get; set; }
        [ForeignKey("LibraryId")]
        [DisplayName("Kütüphane")]
        public Library? Library { get; set; }

        [DisplayName("Dolap")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Column(TypeName = "char(5)")]
        [StringLength(5, ErrorMessage = "En fazla 5 karakter olmalıdır.")]
        public string Cabinet { get; set; }

        [DisplayName("Raf")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Column(TypeName = "char(5)")]
        [StringLength(5, ErrorMessage = "En fazla 5 karakter olmalıdır.")]
        [Range(1,20, ErrorMessage = "En fazla 20 karakter olmalıdır.")]
        public string Shelf { get; set; }

        [DisplayName("Adet")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Range(1, 250, ErrorMessage = "1 ile 250 arasında değer giriniz")]
        public byte Amount { get; set; }
    }
}
