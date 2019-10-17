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
using Newtonsoft.Json;

namespace FacilityMonitoring_WebClient.Controllers
{
    [Route("api/[controller]")]
    public class RegistersController : Controller
    {
        private readonly FacilityContext _context;

        public RegistersController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e=>e.SensorType).Load();
        }

        [HttpGet]
        public object AnalogRegisters(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(this._context.Registers.OfType<AnalogChannel>().Include(e=>e.SensorType), loadOptions);
        }


        [HttpPut]
        public async Task<IActionResult> AnalogChannelUpdate(int key,string values) {
            var register = this._context.Registers.OfType<AnalogChannel>().Include(e=>e.SensorType).SingleOrDefault(e => e.Id == key);
            JsonConvert.PopulateObject(values, register);
            this._context.Entry<AnalogChannel>(register).State=EntityState.Modified;
            await this._context.SaveChangesAsync();
            return Ok();
        }
    }


}