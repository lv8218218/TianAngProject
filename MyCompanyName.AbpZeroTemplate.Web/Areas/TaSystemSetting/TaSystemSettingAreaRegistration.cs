using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaSystemSetting
{
    public class TaSystemSettingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TaSystemSetting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TaSystemSetting_default",
                "TaSystemSetting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}