using CRM.Interface;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRM.Bll.Concrete
{
    public class UserServer:IUserServer
    {
        //数据操作类
        private IUserRepository _repository;
        public UserServer(IUserRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsers()
        {
            var _res = new JsonResult();
            List<User> _users = _repository.GetUsers();
            _res.Data = _users;
            _res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return _res;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(string userStr)
        {
            User _user = new User();
            string[] _userStrArray = SplitUserStr(userStr);
            _user.Id = 0;
            _user.Name = _userStrArray[2];
            if (_userStrArray[3].Equals("true"))
            {
                _user.Sex = true;
            }
            else
            {
                _user.Sex = false;
            }
            _user.Birthday = Convert.ToDateTime(_userStrArray[4]);
            _user.PhoneNumber = _userStrArray[5];
            _user.Address = _userStrArray[6];
            _user.PictureUrl = _userStrArray[7];
            //todo待完成将字符串分割
            int _count = _repository.Add(_user);
            return _count;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult Find(string name)
        {
            var _res = new JsonResult();
            List<User> _users = _repository.Find(name);
            _res.Data = _users;
            return _res;

        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        public int Edit(int id, string eUserStr)
        {
            User _user = new User();
            string[] _userStrArray = SplitUserStr(eUserStr);
            _user.Id = id;
            _user.Name = _userStrArray[2];
            if (_userStrArray[3].Equals("true"))
            {
                _user.Sex = true;
            }
            else
            {
                _user.Sex = false;
            }
            _user.Birthday = Convert.ToDateTime(_userStrArray[4]);
            _user.PhoneNumber = _userStrArray[5];
            _user.Address = _userStrArray[6];
            _user.PictureUrl = _userStrArray[7];
            int _count = _repository.Edit(_user);
            return _count;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int _count = _repository.Delete(id);
            return _count;
        }
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SelectUser(int id)
        {
            var _res = new JsonResult();
            User _user = _repository.SelectUser(id);
            _res.Data = _user;
            return _res;
        }
        /// <summary>
        /// 根据当前页面序号得到当前页面的数据
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public JsonResult GetPageUsers(int currentPage)
        {
            var _res = new JsonResult();
            List<User> _users = _repository.GetPageUsers(currentPage);
            _res.Data = _users;
            return _res;
        }
        /// <summary>
        /// 得到信息总数
        /// </summary>
        /// <returns></returns>
        public int GetUserNumSize()
        {
            int _count = _repository.GetUserNumSize();
            return _count;
        }
        /// <summary>
        /// 用来切分前端传来的User字符串,返回数组,数组下标1`7为用户信息字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string[] SplitUserStr(string str)
        {
            string[] _sArray = str.Split(new char[2] { '\"', ',' });
            return _sArray;
        }

    }
}
