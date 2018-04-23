
using CRM.Interface;
using CRM.Model;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Dal.Concrete
{
    public class UserRepository: IUserRepository
    {
        private ModelContext db = new ModelContext();
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> _users = db.Users.ToList();
            return _users;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(User user)
        {
            db.Users.Add(user);
            int _count = db.SaveChanges();
            return _count;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<User> Find(string name)
        {
            IQueryable<User> _query = db.Users.Where(u => u.Name.Contains(name));
            List<User> _users = _query.ToList();
            return _users;
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        public int Edit(User eUser)
        {
            int _id = eUser.Id;
            User _user = db.Users.SingleOrDefault(u => u.Id == _id);
            if (_user.Name == eUser.Name
                && _user.Sex == eUser.Sex
                && _user.Birthday == eUser.Birthday
                && _user.Address == eUser.Address
                && _user.PhoneNumber == eUser.PhoneNumber
                && _user.PictureUrl == eUser.PictureUrl)
            {
                return 1;
            }
            else
            {
                _user.Name = eUser.Name;
                _user.Sex = eUser.Sex;
                _user.Birthday = eUser.Birthday;
                _user.Address = eUser.Address;
                _user.PhoneNumber = eUser.PhoneNumber;
                _user.PictureUrl = eUser.PictureUrl;
            }
            int _count = db.SaveChanges();
            return _count;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            User _user = db.Users.SingleOrDefault(u => u.Id == id);
            if (_user == null) return 0;
            db.Users.Remove(_user);
            int _count = db.SaveChanges();
            return _count;
        }
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUser(int id)
        {
            User _user = db.Users.SingleOrDefault(u => u.Id == id);
            return _user;
        }
        /// <summary>
        /// 根据当前页面序号得到当前页面的数据
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<User> GetPageUsers(int currentPage)
        {
            int _pageSize = 10;//一页显示10条信息
            int _num = currentPage * _pageSize;//要跳过的条数
            List<User> _users = db.Users.OrderBy(u => u.Id).Skip(_num).Take(_pageSize).ToList();
            return _users;
        }
        /// <summary>
        /// 得到信息总数
        /// </summary>
        /// <returns></returns>
        public int GetUserNumSize()
        {
            List<User> _users = db.Users.ToList();
            int _count = _users.Count;
            return _count;
        }
    }
}
