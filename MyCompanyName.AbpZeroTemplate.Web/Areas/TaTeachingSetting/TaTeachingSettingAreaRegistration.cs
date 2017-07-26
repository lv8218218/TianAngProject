using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaTeachingSetting
{
    public class TaTeachingSettingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TaTeachingSetting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TaTeachingSetting_default",
                "TaTeachingSetting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}