namespace KutuphaneProje.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
