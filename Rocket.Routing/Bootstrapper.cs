using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Rocket.Routing
{
    [Export]
    internal class Bootstrapper
    {
        private static A a;

        public static void Initialize(HttpConfiguration httpConfiguration)
        {
            LazyInitializer.EnsureInitialized(ref a, () => new A(httpConfiguration));
        }

        private class A
        {
            [Import]
            public IBootstrapper BootstrapperExp { get; set; }

            public A(HttpConfiguration httpConfiguration)
            {
                httpConfiguration.MessageHandlers
                    .Add(new MessageHeadersHandler());

                System.Uri uri = new System.Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                string path = Path.GetDirectoryName(uri.LocalPath);
                //var catalog = new DirectoryCatalog(path);

                //Trace.WriteLine(catalog.FullPath);

                //An aggregate catalog that combines multiple catalogs
                var catalog = new AggregateCatalog();

                //Adds all the parts found in same directory where the application is running!
                ////var currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location);

                //catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(HttpRuntime.AppDomainAppPath, "bin")));
                catalog.Catalogs.Add(new DirectoryCatalog(path));


                //var catalog = new DirectoryCatalog(@".\");
                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                BootstrapperExp.Configure(httpConfiguration);


                
            }
        }
    }
}