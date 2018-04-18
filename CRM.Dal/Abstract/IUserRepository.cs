﻿using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dal.Abstract
{
    public interface IUserRepository
    {
        
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int add(User user);
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<User> Find(string name);
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        int Edit(User eUser);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int delete(int id);
        /// <summary>
        /// 根据id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User SelectUser(int id);
        /// <summary>
        /// 根据当前页面序号得到当前页面的数据
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        List<User> GetPageUsers(int currentPage);
        /// <summary>
        /// 得到信息总数
        /// </summary>
        /// <returns></returns>
        int getUserNumSize();



    }
}
