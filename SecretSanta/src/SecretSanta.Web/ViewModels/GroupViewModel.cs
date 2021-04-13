using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecretSanta.Web.ViewModels
{
    public class GroupViewModel : IDataViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; } = "";
    }
}
