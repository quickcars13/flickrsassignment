using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP.VD.Solution.Entity;

namespace IP.VD.Solution.Business
{
    public class UserManager
    {
        #region Private constants declaration
        private static volatile UserManager _instance = null;
        private static object _syncRoot = new object();

        private const string ConnectionString = "FlickrDataEntities";

        #endregion

        #region Constructors section
        /// <summary>
        /// 
        /// </summary>
        private UserManager()
        {

        }
        #endregion

        #region Public Properties
        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserManager();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        #region Public methods



        public bool CheckUserDetails(string email, string password)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
            bool result = false;
            result = (from r in dbContext.Users where r.Email == email && r.Password==password select r).Any();
            return result;
        }

        public User retrieveUser(string email)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
           User retrievedUser= null;


            retrievedUser = (from f in dbContext.Users
                             where f.Email.ToUpper() == email.ToUpper()
                             select f).First();

            return retrievedUser;
        }

        public bool CheckIfEmailExist(string email)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
            bool result = false;
            if(!string.IsNullOrEmpty(email))
                result = (from r in dbContext.Users where r.Email.ToUpper() == email.ToUpper()  select r).Any();
            return result;
        }

        public string RegisterUser(User newuser)
        {
            string result = string.Empty;

            

            FlickrDataEntities dbContext = new FlickrDataEntities();

            if (!CheckIfEmailExist(newuser.Email))
            {
                dbContext.Users.Add(newuser);
                dbContext.SaveChanges();
                result = "SUCCESS";
            }
            else
            {
                result = "Email already exists";
            }
            return result;
        }
        
        
        #endregion


    }
}
