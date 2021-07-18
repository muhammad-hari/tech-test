using GitWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;
using Octokit.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GitWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Private Helpers

        private readonly ILogger<HomeController> logger;
        private readonly GitConfig gitConfig;
        private readonly GitHubClient client;

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger, GitConfig options)
        {
            this.logger = logger;
            this.gitConfig = options;

            client = new GitHubClient(new ProductHeaderValue(options.GitClient));
        }

        #endregion

        #region Action Methods

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(HomeController.Index));

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (!string.IsNullOrEmpty(login.PersonalAccessToken) )
            {
                 client.Credentials = new Credentials(login.PersonalAccessToken);

                try
                {
                    var user = await client.User.Current();
                    var claims = new List<Claim>
                    {
                        new Claim("AccessToken", login.PersonalAccessToken),
                        new Claim("Username", user.Login)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authProps = new AuthenticationProperties { IsPersistent = false };

                    if (!string.IsNullOrEmpty(returnUrl))
                        authProps.RedirectUri = returnUrl;

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToLocal(returnUrl);

                }
                catch (Exception ex)
                {
                    return View();
                }
               
            }

            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            var authProps = new AuthenticationProperties { RedirectUri = "/Home/Login" };

            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login), "Home");
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var token = @User.FindFirst("AccessToken").Value;
            client.Credentials = new Credentials(token);
            var repositories = await client.Repository.GetAllForCurrent();
            var model = new IndexViewModel(repositories);

            return View(model);

        }

        #endregion

        [Authorize]
        public async Task<IActionResult> IssueDetail(string repositoryName)
        {
            if (string.IsNullOrEmpty(repositoryName))
                return RedirectToAction(nameof(HomeController.Index));

            var user = @User.FindFirst("Username").Value;

            var issue = await client.Issue.GetAllForRepository(user, repositoryName);
            var model = new IssueViewModel(issue);

            return View(model);

        }

        #region Ajax Methods

        [HttpPost]
        public async Task<JsonResult> GetIssue(string repositoryName)
        {
            var user = @User.FindFirst("Username").Value;
            var issue = await client.Issue.GetAllForRepository(user, repositoryName);
            return Json(issue);
        }

        #endregion

        #region Private Actions
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        #endregion
    }
}
