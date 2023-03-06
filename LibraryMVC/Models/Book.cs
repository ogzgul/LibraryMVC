using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMVC.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName ="char(13)")]
        [Required(ErrorMessage ="Bu alan zorunludur.")]
        [StringLength(13,MinimumLength =13,ErrorMessage ="ISBN 13 karakter olmalıdır.")]
        public string ISBN { get; set; }
        

        [Column(TypeName = "nchar(1500)")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(1500, ErrorMessage = "En fazla 1500 karakter olmalıdır.")]
        [DisplayName("Adı")]
        public string Title { get; set; }
       
        
        [DisplayName("Yazar")]
        public long AuthorId { get; set; }
       
        
        [ForeignKey("AuthorId")]
        [DisplayName("Yazar")]
        public Author? Author { get; set; }
    }
}
