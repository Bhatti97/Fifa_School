using Fifa_school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fifa_school.ManagingUsers
{
    public class UserCredentials
    {
        ApplicationDbcontext db = new ApplicationDbcontext();
        public int IsUserLoginwithRole(string Role,Users user)
        {
            if (user != null)
            {
                var foundUser = db.Users.Find(user.user_id);
                if (foundUser != null)
                {
                    //User Found
                    if (foundUser.user_role == Role)
                    {
                        return 1;       //Role Detected
                    }
                    return 2;           //Role Not Detected
                }
            }  
            return 0;               //User Not Found
        }
        public int IsUserLoginwithMultipleRole(string[] Roles, Users user)
        {
            if (user != null)
            {
                int UserStatus = 0;
                foreach (var item in Roles)
                {
                    if (user != null)
                    {
                        var foundUser = db.Users.Find(user.user_id);
                        if (foundUser != null)
                        {
                            //User Found
                            if (foundUser.user_role == item)            //item stands for current role
                            {
                                return 1;       //Role Detected
                            }
                            UserStatus = 2;           //Role Not Detected
                        }
                        UserStatus = 0;               //User Not Found
                    }

                }
                return UserStatus;
            }
            return 0;   //Session not set
        }
        public bool IsUserLogin( Users user)
        {
            if (user != null)
            {
                var foundUser = db.Users.Find(user.user_id);
                if (foundUser != null && foundUser.status)
                {
                    //User Found
                    return true;
                }
            }
            return false;
        }
    }
}