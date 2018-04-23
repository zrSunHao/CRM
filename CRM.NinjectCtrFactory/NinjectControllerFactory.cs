using CRM.Bll.Concrete;
using CRM.Dal.Concrete;
using CRM.Interface;
using CRM.OledbDal;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRM.NinjectCtrFactory
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        /// <summary>
        /// 添加绑定
        /// </summary>
        private void AddBindings()
        {
            ninjectKernel.Bind<IUserServer>().To<UserServer>();
            if (false)
            {
                ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            }
            else
            {
                ninjectKernel.Bind<IUserRepository>().To<AccessUserRepository>();
            }
        }
    }
}
