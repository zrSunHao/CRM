using CRM.Bll;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Admin.Controllers
{
    public class UserController : Controller
    {
        UserManager uMr = new UserManager();
        static int tempId = 0;
        //临时存储要修改的用户对象
        /// <summary>
        /// 默认
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("ListView");
        }
        /// <summary>
        /// 返回用户列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ListUser()
        {
            return View("ListView");
        }
        /// <summary>
        /// 显示用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserList()
        {
            var res = new JsonResult();
            List<User> users = new List<User>();
            users = uMr.GetUsers();
            res.Data = users;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult FindUser(string name)
        {
            List<User> users = new List<User>();
            users = uMr.Find(name);
            var res = new JsonResult();
            res.Data = users;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult AddUser(User user)
        {
            if (user.Name == null||user.Name=="")
                return View("AddUser");
            uMr.add(user);
            return View("ListView");
        }
        /// <summary>
        /// 删除方法，返回是否删除成功的标志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(int id)
        {
            int count = uMr.delete(id);
            return View("ListView");
        }
        /// <summary>
        /// 处理提交的编辑用户的id，返回user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DoEdit(User user)
        {
            if (user.Name == null || user.Name == "")
                return 0;
            int j = user.Id;
            int c = uMr.Edit(user);
            return c;
            //
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser(int id)
        {
            tempId = id;
            User user = uMr.SelectUser(id);
            return View("EditUser", Json(user, JsonRequestBehavior.AllowGet));
        }
        /// <summary>
        /// 返回要修改对象的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEditUser()
        {
            int id = tempId;
            User user = uMr.SelectUser(id);
            return Json(user, JsonRequestBehavior.AllowGet);
            //return View("ListView");
        }
        /// <summary>
        /// 添加用户文件上传处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int UploadAddImage()
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file != null)
            {
                string filePath = Server.MapPath("~/UploadImage/" + file.FileName);
                file.SaveAs(filePath);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 分页查询，并返回页面数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserListPage(int currentPage)
        {
            List<User> users = new List<User>();
            users = uMr.GetPageUsers(currentPage);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 得到信息总量
        /// </summary>
        /// <returns></returns>
        public int getUserNumSize()
        {
            int UserNumSize = uMr.getUserNumSize();
            return UserNumSize;
        }

    }
}