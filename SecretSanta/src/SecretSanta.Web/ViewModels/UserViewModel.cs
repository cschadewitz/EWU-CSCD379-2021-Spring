using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name="First Name")]
        public string? FirstName { get; set; } = "";
        [Display(Name="Last Name")]
        public string? LastName { get; set; } = "";

        //public virtual string FullName { get => $"{FirstName} {LastName}"; }
    }
}
