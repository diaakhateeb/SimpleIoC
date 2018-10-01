using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoC;
using System;

namespace SimpleIoCUnitTest
{
    [TestClass]
    public class SimpleIocContainerUnitTests
    {
        [TestMethod]
        public void should_resolve_object()
        {
            var container = new IoCContainer();

            container.Register<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();

            Assert.ReferenceEquals(instance, typeof(ConcreteType));
        }

        [TestMethod]
        public void should_throw_exception_if_type_not_registered()
        {
            var container = new IoCContainer();

            Exception exception = null;
            try
            {
                container.Resolve<ITypeToResolve>();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.ReferenceEquals(exception, typeof(UnRegisteredException));
        }

        [TestMethod]
        public void should_resolve_object_with_registered_constructor_parameters()
        {
            var container = new IoCContainer();

            container.Register<ITypeToResolve, ConcreteType>();
            container.Register<ITypeToResolveWithConstructorParams, ConcreteTypeWithConstructorParams>();

            var instance = container.Resolve<ITypeToResolveWithConstructorParams>();

            Assert.ReferenceEquals(instance, typeof(ConcreteTypeWithConstructorParams));
        }

        [TestMethod]
        public void should_create_singleton_instance_by_default()
        {
            var container = new IoCContainer();

            container.Register<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();

            Assert.ReferenceEquals(container.Resolve<ITypeToResolve>(), instance);
        }

        [TestMethod]
        public void can_create_transient_instance()
        {
            var container = new IoCContainer();

            container.Register<ITypeToResolve, ConcreteType>(LifeCycle.Transient);

            var instance = container.Resolve<ITypeToResolve>();

            Assert.ReferenceEquals(container.Resolve<ITypeToResolve>(), instance);
        }
    }

    public interface ITypeToResolve
    {
    }

    public class ConcreteType : ITypeToResolve
    {
    }

    public interface ITypeToResolveWithConstructorParams
    {
    }

    public class ConcreteTypeWithConstructorParams : ITypeToResolveWithConstructorParams
    {
        public ConcreteTypeWithConstructorParams(ITypeToResolve typeToResolve)
        {
        }
    }
}
