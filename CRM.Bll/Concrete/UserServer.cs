using CRM.Bll.Abstract;
using CRM.Dal.Abstract;
using CRM.Dal.Concrete;
using CRM.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Bll.Concrete
{
    public class UserServer:IUserServer
    {
        private IUserRepository repository;
        public UserServer(IUserRepository UserRepository)
        {
            repository = UserRepository;
        }
        IUserRepository userRepository = new UserRepository();
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> users = repository.GetUsers();
            return users;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int add(User user)
        {
            int count = repository.add(user);
            return count;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<User> Find(string name)
        {
            List<User> user = repository.Find(name);
            return user;

        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        public int Edit(User eUser)
        {
            int count = repository.Edit(eUser);
            return count;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete(int id)
        {
            int count = repository.delete(id);
            return count;
        }
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUser(int id)
        {
            User user = repository.SelectUser(id);
            return user;
        }
        /// <summary>
        /// 根据当前页面序号得到当前页面的数据
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<User> GetPageUsers(int currentPage)
        {
            List<User> users = repository.GetPageUsers(currentPage);
            return users;
        }
        /// <summary>
        /// 得到信息总数
        /// </summary>
        /// <returns></returns>
        public int getUserNumSize()
        {
            int count = repository.getUserNumSize();
            return count;
        }


    }
}
