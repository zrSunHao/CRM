using CRM.Dal;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Bll
{
    public class UserManager
    {
        /// <summary>
        /// 实例化上下文，操作数据
        /// </summary>
        ModelContext db = new ModelContext();
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> users = db.Users.ToList();
            return users;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int add(User user)
        {
            db.Users.Add(user);
            int count = db.SaveChanges();
            return count;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<User> Find(string name)
        {


            //IQueryable<User> Query= db.Users.Where(u => u.Id == id);
            IQueryable<User> Query = db.Users.Where(u => u.Name.Contains(name));
            List<User> user = Query.ToList();
            return user;

        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        public int Edit(User eUser)
        {
            int id = eUser.Id;
            User user = db.Users.SingleOrDefault(u => u.Id == id);
            if (user.Name == eUser.Name
                &&user.Sex == eUser.Sex
                &&user.Birthday == eUser.Birthday
                &&user.Address == eUser.Address
                &&user.PhoneNumber == eUser.PhoneNumber
                &&user.PictureUrl == eUser.PictureUrl)
            {
                return 1;
            }
            else
            {
                user.Name = eUser.Name;
                user.Sex = eUser.Sex;
                user.Birthday = eUser.Birthday;
                user.Address = eUser.Address;
                user.PhoneNumber = eUser.PhoneNumber;
                user.PictureUrl = eUser.PictureUrl;
            }
            int count = db.SaveChanges();
            return count;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete(int id)
        {
            User user = db.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) return 0;
            db.Users.Remove(user);
            int count = db.SaveChanges();
            return count;
        }
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUser(int id)
        {
            User user = db.Users.SingleOrDefault(u => u.Id == id);
            return user;

        }
        /// <summary>
        /// 根据当前页面序号得到当前页面的数据
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<User> GetPageUsers(int currentPage)
        {
            int pageSize = 10;//一页显示10条信息
            int num = currentPage * pageSize;//要跳过的条数
            List<User> users = db.Users.OrderBy(u => u.Id).Skip(num).Take(pageSize).ToList();
            return users;
        }
        /// <summary>
        /// 得到信息总数
        /// </summary>
        /// <returns></returns>
        public int getUserNumSize()
        {
            List<User> users = db.Users.ToList();
            int count = users.Count;
            return count;
        }
    }
}
