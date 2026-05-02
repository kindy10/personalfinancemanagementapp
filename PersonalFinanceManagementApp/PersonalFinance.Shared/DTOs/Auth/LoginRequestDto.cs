using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Auth
{
    //User for login
    public  class LoginRequestDto
    {
        public string Email { get ; set; }
        public string Password { get; set; }
    }
}
