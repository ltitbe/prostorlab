using System.ComponentModel.DataAnnotations;

namespace usersapp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
