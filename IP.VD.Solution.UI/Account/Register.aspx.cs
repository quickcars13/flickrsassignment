using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using IP.VD.Solution.UI.Models;
using IP.VD.Solution.Entity;
using IP.VD.Solution.Business;

namespace IP.VD.Solution.UI.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {

            User newuser = new User();
            newuser.Address = Address.Text;
            newuser.City = City.Text;
            newuser.Country = Country.Text;
            newuser.Email = Email.Text;
            newuser.FirstName = FirstName.Text;
            newuser.LastName = LastName.Text;
            newuser.Password = Password.Text;
            newuser.CreatedOn = DateTime.Now;

            string result= UserManager.Instance.RegisterUser(newuser);

            if (!string.IsNullOrEmpty(result) && result.Equals("SUCCESS"))
            {
                ErrorMessage.Text = "Registration successful. Please login with your email address and password.";
            }
            else
            {
                ErrorMessage.Text = result;
            }
           
        }
    }
}