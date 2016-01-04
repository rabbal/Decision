using System.Web;
using Decision.Web;

[assembly: PreApplicationStartMethod(typeof(RegisterModules), "Start")]
namespace Decision.Web
{
    public class RegisterModules
    {
        public static void Start()
        {
            //    DynamicModuleDecision.Utility.RegisterModule(typeof(DosAttackModule));
            //    DynamicModuleDecision.Utility.RegisterModule(typeof(Decision.Common.HttpCompress.HttpModule));
        }
    }
}