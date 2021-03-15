using OASystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OASystem.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class OASystemController : AbpController
    {
        protected OASystemController()
        {
            LocalizationResource = typeof(OASystemResource);
        }
    }
}