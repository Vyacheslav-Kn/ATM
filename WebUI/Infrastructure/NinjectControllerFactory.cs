using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ATM.Domain.Concrete;
using ATM.Domain.Abstract;

namespace ATM.WebUI.Infrastructure
{
    // реализация пользовательской фабрики контроллеров, наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,  Type controllerType)
        {
            // получение объекта контроллера из контейнера используя его тип
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // конфигурирование контейнера
            ninjectKernel.Bind<ICardRepository>().To<EFCardRepository>();
            ninjectKernel.Bind<IEripRepository>().To<EFEripRepository>(); 
        }
    }
}