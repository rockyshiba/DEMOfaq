using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace demofaq.Models
{
    [MetadataType(typeof(BuddyFaq))]
    public partial class FAQS
    {
        public FAQS()
        {
            Created_at = DateTime.Now;
        }
    }

    public class BuddyFaq
    {
        [Required(ErrorMessage = "Question required", AllowEmptyStrings = false)]
        [StringLength(300, ErrorMessage = "Question is too long")]
        public string Question { get; set; }
        public string Name { get; set; } = "Anonymous";

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Display(Name = "Asked at")]
        public DateTime? Created_at { get; set; }

        [Display(Name = "Category")]
        public Nullable<int> Category_id { get; set; }
    }
}