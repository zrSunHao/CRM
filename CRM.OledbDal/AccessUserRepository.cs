using CRM.Interface;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace CRM.OledbDal
{
    public class AccessUserRepository : IUserRepository
    {
        ConnDbForAcccess conn = new ConnDbForAcccess();

        string sql;
        string date;
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(User user)
        {
            date = user.Birthday.ToString("yyyy/MM/dd");
            sql = "insert into Users(Name,Sex,Birthday,PhoneNumber,Address,PictureUrl)values('"+user.Name+"',"+user.Sex+",'"+ date
                + "',"+user.PhoneNumber+",'"+user.Address+"','"+user.PictureUrl+"')";
            int _count = conn.ExeSQL(sql);
            return _count;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            sql = "DELETE FROM Users WHERE Id ="+id;
            int _count = conn.ExeSQL(sql);
            return _count;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="eUser"></param>
        /// <returns></returns>
        public int Edit(User eUser)
        {
            date = eUser.Birthday.ToString("yyyy/MM/dd");
            sql = "update Users set Name='"+ eUser.Name+ "',Sex="+eUser.Sex+",Birthday='"+date+
                "',PhoneNumber='"+eUser.PhoneNumber+"',Address='"+eUser.Address+
                "',PictureUrl='"+eUser.PictureUrl+"' where id="+eUser.Id+"";
            int _count = conn.ExeSQL(sql);
            return _count;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<User> Find(string name)
        {
            sql = "SELECT * FROM [Users] WHERE Name LIKE '%%"+name+"%%'";
            DataSet dataSet= conn.ReturnDataSet(sql);
            List<User> _users = (List<User>)DataSetToList<User>(dataSet, 0);
            return _users;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public List<User> GetPageUsers(int currentPage)
        {
            //string sql = "SELECT * FROM Users";
            if (currentPage == 0)
            {
                sql = "select top 10  *  from Users  order by ID desc";
            }
            else
            {
                sql = "select top 10 * from Users where ID not in(select top " + currentPage * 10 + " ID from Users order by ID)  order by ID";
            }
            DataSet _dataSet = conn.ReturnDataSet(sql);
            List<User> _users = (List<User>)DataSetToList<User>(_dataSet,0);
            return _users;
        }
        /// <summary>
        /// 用于得到存储信息的总数
        /// </summary>
        /// <returns></returns>
        public int GetUserNumSize()
        {
            string sql = "SELECT * FROM Users";
            return conn.ReturnSqlResultCount(sql);
        }
        /// <summary>
        /// 得到全部的数据
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            string sql = "SELECT * FROM Users";
            DataSet _dataSet = conn.ReturnDataSet(sql);
            List<User> _users = (List<User>)DataSetToList<User>(_dataSet, 0);
            return _users;
        }
        /// <summary>
        /// 加载修改页面时，查询用户信息，返回User对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUser(int id)
        {
            string sql = "SELECT * FROM Users WHERE Id="+id;
            //OleDbDataReader _dataReader = conn.ReturnDataReader(sql);
            DataSet _dataSet = conn.ReturnDataSet(sql);
            List<User> _users = (List<User>)DataSetToList<User>(_dataSet, 0);
            User _user = _users[0];
            return _user;
        }
        /// <summary>  
        /// DataSetToList  
        /// </summary>  
        /// <typeparam name="T">转换类型</typeparam>  
        /// <param name="dataSet">数据源</param>  
        /// <param name="tableIndex">需要转换表的索引</param>  
        /// <returns></returns>  
        public IList<User> DataSetToList<User>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效  
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable _dt = dataSet.Tables[tableIndex];

            IList<User> _list = new List<User>();

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                //创建泛型对象  
                User _user = Activator.CreateInstance<User>();
                //获取对象所有属性  
                PropertyInfo[] propertyInfo = _user.GetType().GetProperties();
                for (int j = 0; j < _dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值  
                        if (_dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (_dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_user, _dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_user, null, null);
                            }
                            break;
                        }
                    }
                }
                _list.Add(_user);
            }
            return _list;
        }
    }
}
