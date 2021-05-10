using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "It is easier to work with strings in this case")]
        public string? Url { get; set; } = "";
        public int Priority { get; set; }
        [Display(Name="User")]
        public int UserId { get; set; }
    }
}
