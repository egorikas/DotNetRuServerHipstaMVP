using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Dto.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetRuServerHipstaMVP.UI.Pages
{
    public class LoginModel : PageModel
    {       
        [BindProperty]
        public AuthTokenRequest TokenRequest { get; set; }
        
        
        public void OnGet()
        {
        }
    }
}