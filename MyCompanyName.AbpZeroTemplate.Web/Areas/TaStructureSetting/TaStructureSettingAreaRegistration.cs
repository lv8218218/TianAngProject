using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaStructureSetting
{
    public class TaStructureSettingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TaStructureSetting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TaStructureSetting_default",
                "TaStructureSetting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}