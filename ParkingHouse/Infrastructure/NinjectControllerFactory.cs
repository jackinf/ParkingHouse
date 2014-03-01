﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Concrete;

namespace ParkingHouse.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<ICarsParkingRepository>().To<EfCarsRepository>();
            ninjectKernel.Bind<ISummaryRepository>().To<EfCurrentProfitRepository>();
        }
    }
}