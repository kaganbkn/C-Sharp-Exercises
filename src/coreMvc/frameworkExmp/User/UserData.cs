using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frameworkExmp.User
{
    public interface IUserData
    {
        UserDetail GetUser();
    }

    public class UserData : IUserData
    {
        public UserDetail GetUser()
        {
            return new UserDetail
            {
                Name = "kağan",
                Id = 1
            };
        }
    }

    public class UserDetail 
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}