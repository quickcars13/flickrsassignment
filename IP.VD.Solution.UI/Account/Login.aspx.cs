using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using IP.VD.Solution.UI.Models;
using IP.VD.Solution.Business;
using System.Web.Security;
using IP.VD.Solution.Entity;

namespace IP.VD.Solution.UI.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (UserManager.Instance.CheckIfEmailExist(Email.Text))
                {
                    if(UserManager.Instance.CheckUserDetails(Email.Text, Password.Text))
                    {
                        FormsAuthentication.SetAuthCookie(Email.Text, true);
                        User user = UserManager.Instance.retrieveUser(Email.Text);

                        FormsAuthenticationTicket oTicket = new FormsAuthenticationTicket(1, user.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), true, user.FirstName+" "+user.LastName);
                        string sCookie = FormsAuthentication.Encrypt(oTicket);
                        HttpCookie ckCookie = new HttpCookie(FormsAuthentication.FormsCookieName, sCookie);
                        ckCookie.Path = FormsAuthentication.FormsCookiePath;
                        Response.Cookies.Add(ckCookie);
                        FormsAuthentication.RedirectFromLoginPage(Email.Text, true);
                    }
                    else
                    {
                        FailureText.Text = "Invalid Login Details";
                        ErrorMessage.Visible = true;
                    }
                }
                else
                {
                    FailureText.Text = "Email does not exist";
                    ErrorMessage.Visible = true;
                }                
            }
        }
    }
}