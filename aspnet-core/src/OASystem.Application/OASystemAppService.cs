using System;
using System.Collections.Generic;
using System.Text;
using OASystem.Localization;
using Volo.Abp.Application.Services;

namespace OASystem
{
    /* Inherit your application services from this class.
     */
    public abstract class OASystemAppService : ApplicationService
    {
        protected OASystemAppService()
        {
            LocalizationResource = typeof(OASystemResource);
        }
    }
}
