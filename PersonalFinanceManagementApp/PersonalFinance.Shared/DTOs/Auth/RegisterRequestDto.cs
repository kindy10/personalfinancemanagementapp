using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Auth
{
    //Used when a user signs up
    public  class RegisterRequestDto
    {
        public string Name {  get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
