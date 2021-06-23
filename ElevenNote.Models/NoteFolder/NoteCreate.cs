using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteCreate
    {
        [Required]
        [MaxLength(16, ErrorMessage = "You type too long. You need shorter title! 16 Characters! MAXIMUM!")]
        [Display(Name = "Title For A Note")]
        public string Title { get; set; }
        [Required]
        [MinLength(120, ErrorMessage = "Must type out at LEAST a few sentences for the content. 120 chars minimum.")]
        [MaxLength(500, ErrorMessage = "Try being a bit more concise. A note should not be more than 500 characters.")]
        public string Content { get; set; }
        public int? CategoryId { get; set; }
    }
}
