using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Category Title must be more than six characters.")]
        [MaxLength(16, ErrorMessage = "Category Title too Long.")]
        public string Title { get; set; }
        public virtual List<Note> Notes { get; set; } = new List<Note>();
    }
}
