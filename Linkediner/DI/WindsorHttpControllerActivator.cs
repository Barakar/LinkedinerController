using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace Linkediner.DI
{
    public class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorHttpControllerActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller =
            (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(new DisposableWrapper(() => _container.Release(controller)));

            return controller;
        }
    }
}