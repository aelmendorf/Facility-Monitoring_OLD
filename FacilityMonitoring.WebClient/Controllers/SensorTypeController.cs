using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using FacilityMonitoring.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacilityMonitoring.WebClient.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class SensorTypeController : ControllerBase
    {
        private readonly FacilityContext _context;
        public SensorTypeController(FacilityContext context) {
            this._context = context;
            this._context.Categories.OfType<SensorType>().Load();
        }

        [HttpGet]
        public async Task<object> SensorTypes(DataSourceLoadOptions loadOptions) {
            var result = await Task.Run(() => { 
                return DataSourceLoader.Load(this._context.Categories.OfType<SensorType>(), loadOptions);
            });
            return result;
        }
    }
}