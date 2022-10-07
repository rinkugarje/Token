using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginTokenTask.Models
{
    public class LoginUser
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //public string RefreshToken { get; set; } = string.Empty;
        //public DateTime TokenCreated { get; set; }
        //public DateTime TokenExpires { get; set; }
    }
}
