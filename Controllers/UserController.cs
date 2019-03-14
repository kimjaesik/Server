using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        IUserDao userDao;

        public UserController(IUserDao userDao){
            this.userDao = userDao;
        }

        [HttpGet]
        public UserResult Info(string user_loginID){

            UserResult result = new UserResult();
            result.Data = userDao.GetUser(user_loginID);
            return result;

        }

        [HttpPost]
        public UserResult Update([FromBody] User requestUser){

            UserResult result = new UserResult();
            userDao.UpdateUser(requestUser);
            
            result.Data = userDao.GetUser(requestUser.User_loginID);

            result.ErrorNo = 1;
            result.ErrorCode = "Success";

            return result;
        }

    }

}