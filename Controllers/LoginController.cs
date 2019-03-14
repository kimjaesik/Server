using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class LoginController : Controller
    {

        IUserDao userDao;
        public LoginController(IUserDao userDao){
            this.userDao = userDao;
        }

        // GET Login/Get/Id
        [HttpGet("{id}")]
        public User Get(string user_loginID)
        {
            User user = userDao.GetUser(user_loginID);
            return user;
        }

        // GET Login/VersionCheck/{게임버전}  
        [HttpGet("{version}")]
        public ResultBase VersionCheck(int version)
        {
            ResultBase result = new ResultBase();

            //뉴로월드 처음시작할때 버전 체크용 . 나중에 어캐할까 구현예정.
            //일단은 패스  
            result.ErrorCode = "Sucess";

            return result;
        }

        // POST Login/RegisterByLoginID       //로그인 아이디로 등록.
        [HttpPost]
        public LoginResult RegisterByLoginID([FromBody] User requestUser)
        {

            LoginResult result = new LoginResult();
      
            Console.WriteLine(requestUser.User_loginID);

            User user = userDao.FindUserByLoginID(requestUser.User_loginID);
            
            if(user != null ){ // 이미 가입되어 있음
                //에러 보내야 함.
                result.Data = user;
                result.ErrorCode = "Sucess";
                result.ErrorNo = 1;

                return result;

            } else { // 없으면  가입시킴..

                userDao.InsertUser(requestUser);
                user = userDao.FindUserByLoginID(requestUser.User_loginID);
                result.Data = user;
                result.ErrorCode = "Sucess";
                result.ErrorNo = 2;

                return result;

            }

        }

    }
}
