using Interview1.Task.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Interview1.Task.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<StudentDAL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}