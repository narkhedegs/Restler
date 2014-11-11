using System;
using System.Collections.Generic;
using System.IO;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Restler
{
    public interface IServiceProvider
    {
        T Get<T>();
        IEnumerable<T> GetAll<T>();
    }

    public class ServiceProvider : IServiceProvider
    {
        private readonly StandardKernel _kernel;

        public ServiceProvider()
        {
            _kernel = new StandardKernel();

            _kernel.Bind(
                scanner =>
                    scanner.FromAssembliesInPath(AppDomain.CurrentDomain.BaseDirectory)
                        .SelectAllClasses()
                        .BindAllInterfaces());

            var defaultAddInsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AddIns");
            if (Directory.Exists(defaultAddInsDirectory))
            {
                var addInDirectories = new DirectoryInfo(defaultAddInsDirectory).GetDirectories("*", SearchOption.AllDirectories);
                foreach (var directoryInfo in addInDirectories)
                {
                    var directory = directoryInfo;
                    _kernel.Bind(
                        scanner =>
                            scanner.FromAssembliesInPath(directory.FullName).SelectAllClasses().BindAllInterfaces());
                }
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _kernel.GetAll<T>();
        }
    }
}