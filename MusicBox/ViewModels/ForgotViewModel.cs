using System.ComponentModel.DataAnnotations;

namespace MusicBox.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}