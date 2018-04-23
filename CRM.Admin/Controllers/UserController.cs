using CRM.Interface;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CRM.Admin.Controllers
{
    public class UserController : Controller
    {
        //服务类，用来处理业务逻辑
        private IUserServer _server;
        public UserController(IUserServer UserServer)
        {
            _server = UserServer;
        }
        static int _tempId = 0;//临时存储要修改的用户对象的ID
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
            return _server.GetUsers();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult FindUser(string name)
        {
            var _res = _server.Find(name);
            _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return _res;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult RedictAddView()
        {
                return View("AddUser");
        }
        /// <summary>
        /// 添加用户并返回操作结果标志
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddUser()
        {
            Stream _postData = Request.InputStream;
            StreamReader _sRead = new StreamReader(_postData);
            string _userStr = _sRead.ReadToEnd();
            return _server.Add(_userStr);
        }
        /// <summary>
        /// 删除方法，返回是否删除成功的标志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(int id)
        {
            int _count = _server.Delete(id);
            return View("ListView");
        }
        /// <summary>
        /// 处理提交的编辑用户的id，返回user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public int DoEdit()
        {
            Stream _postData = Request.InputStream;
            StreamReader _sRead = new StreamReader(_postData);
            string _eUserStr = _sRead.ReadToEnd();
            return _server.Edit(_tempId, _eUserStr);
        }
        /// <summary>
        /// 跳转到编辑页面，并存储待修改的用户id
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectEditView(int id)
        {
            _tempId = id;
            return View("EditUser");
        }
        /// <summary>
        /// 返回要修改对象的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEditUser()
        {
            int _id = _tempId;
            var _res = _server.SelectUser(_id);
            _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return _res;
        }
        /// <summary>
        /// 添加用户文件上传处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int UploadAddImage()
        {
            HttpPostedFileBase _file = Request.Files[0];
            if (_file != null)
            {
                string _filePath = Server.MapPath("~/UploadImage/" + _file.FileName);
                _file.SaveAs(_filePath);
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
            var _res = _server.GetPageUsers(currentPage);
            _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return _res;
        }
        /// <summary>
        /// 得到信息总量
        /// </summary>
        /// <returns></returns>
        public int GetUserNumSize()
        {
            return _server.GetUserNumSize();
        }

    }
}