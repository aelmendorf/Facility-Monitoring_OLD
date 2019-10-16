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

namespace FacilityMonitoring_WebClient.Controllers
{
    [Route("api/[controller]")]
    public class MonitorBoxRegisterController : Controller
    {
        private readonly FacilityContext _context;

        public MonitorBoxRegisterController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e=>e.SensorType).Load();
        }

        [HttpGet]
        public object Registers(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(this._context.Registers.OfType<AnalogChannel>(), loadOptions);
        }


    }


}