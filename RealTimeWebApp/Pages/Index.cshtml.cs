using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealTimeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration configuration;
        private readonly string loginUlr;

        [BindProperty]
        public UserLogInModel LoginModel { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            this.loginUlr = configuration["LoginUrl"];
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLogIn(string data)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var loginDataJson = JsonConvert.SerializeObject(new
                        {
                            login = LoginModel.Username,
                            password = LoginModel.Password
                        });
                        var content = new StringContent(loginDataJson, Encoding.UTF8, "application/json");
                        var responce = await httpClient.PostAsync(loginUlr, content);
                        if (responce.IsSuccessStatusCode)
                        {
                            HttpContext.Session.SetString("Token", await responce.Content.ReadAsStringAsync());
                            return this.RedirectToPage("/Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return this.Page();
        }
    }
}
