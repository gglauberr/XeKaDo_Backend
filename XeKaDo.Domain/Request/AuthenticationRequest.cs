using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Request
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class RegisterRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmSenha { get; set; }
    }
}
