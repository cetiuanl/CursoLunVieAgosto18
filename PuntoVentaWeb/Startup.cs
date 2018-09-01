using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PuntoVentaWeb.Startup))]
namespace PuntoVentaWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
