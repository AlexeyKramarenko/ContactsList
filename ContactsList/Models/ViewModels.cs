using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsList.Models
{
    public class CompanyItemViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ActivityName { get; set; }
        public string TownName { get; set; } 
    }
    public class UserViewmodel
    {
        [Display(Name ="Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        //[MinLength(4, ErrorMessage ="Пароль должен быть не меньше 4 символов")]
        public string Password { get; set; }
    }
}
