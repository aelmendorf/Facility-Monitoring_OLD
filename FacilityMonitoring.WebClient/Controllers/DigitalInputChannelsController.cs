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

namespace FacilityMonitoring.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalInputChannelsController : ControllerBase
    {

        private readonly FacilityContext _context;

        public DigitalInputChannelsController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e => e.SensorType).Load();
        }

        [HttpGet]
        public object DigitalInputRegisters(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(this._context.Registers.OfType<DigitalInputChannel>().Include(e => e.SensorType), loadOptions);
        }


        [HttpPut]
        public async Task<IActionResult> DigitalChannelUpdate(int key, string values) {
            var register = this._context.Registers.OfType<DigitalInputChannel>().Include(e => e.SensorType).SingleOrDefault(e => e.Id == key);
            JsonConvert.PopulateObject(values, register);
            this._context.Entry<DigitalInputChannel>(register).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            return Ok();
        }
    }
}