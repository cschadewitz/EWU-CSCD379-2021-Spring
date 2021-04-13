using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel : IDataViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Gift Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Url]
        [Display(Name = "Url")]
        public string Url { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "{0} must be between {1} max and {2} min")]
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
    }
}
