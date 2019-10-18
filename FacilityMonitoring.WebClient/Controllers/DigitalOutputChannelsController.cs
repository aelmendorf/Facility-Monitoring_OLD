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
    public class DigitalOutputChannelsController : ControllerBase
    {

        private readonly FacilityContext _context;

        public DigitalOutputChannelsController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e => e.SensorType).Load();
        }


        [HttpGet]
        public async Task<object> DigitalOutputRegisters(DataSourceLoadOptions loadOptions) {
            var result = await Task.Run(
                () => DataSourceLoader.Load(this._context.Registers.OfType<DigitalOutputChannel>().Include(e => e.SensorType).OrderBy(e => e.RegisterIndex), loadOptions));
            return result;
        }


        [HttpPut]
        public async Task<IActionResult> OutputChannelUpdate(int key, string values) {
            var register = await this._context.Registers.OfType<DigitalOutputChannel>().Include(e => e.SensorType).SingleOrDefaultAsync(e => e.Id == key);
            JsonConvert.PopulateObject(values, register);
            this._context.Entry<DigitalOutputChannel>(register).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            return Ok();
        }
    }
}