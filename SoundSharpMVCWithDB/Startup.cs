using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoundSharpMVCWithDB.Startup))]
namespace SoundSharpMVCWithDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
