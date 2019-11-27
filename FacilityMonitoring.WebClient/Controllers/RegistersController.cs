using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace FacilityMonitoring_WebClient.Controllers
{
    [Route("api/[controller]")]
    public class RegistersController : Controller
    {
        private readonly FacilityContext _context;
        private HubConnection _hubConnection;

        public RegistersController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e=>e.SensorType).Load();
        }

        [HttpGet]
        public async Task<object> AnalogRegisters(DataSourceLoadOptions loadOptions) {
            var result = await Task.Run(() => {
                return DataSourceLoader.Load(this._context.Registers.OfType<AnalogChannel>().Include(e => e.SensorType).OrderBy(e => e.RegisterIndex), loadOptions);
            });

            return result;
        }


        [HttpPut]
        public async Task<IActionResult> AnalogChannelUpdate(int key,string values) {
            var register = await this._context.Registers.OfType<AnalogChannel>().Include(e=>e.SensorType).SingleOrDefaultAsync(e => e.Id == key);
            JsonConvert.PopulateObject(values, register);
            this._context.Entry<AnalogChannel>(register).State=EntityState.Modified;
            await this._context.SaveChangesAsync();
            this._hubConnection = new HubConnectionBuilder()
            //.WithUrl("http://172.20.4.209:443/hubs/gasbay")
            .WithUrl("http://localhost:5000/hubs/gasbay")
            .Build();
            await this._hubConnection.StartAsync();
            await this._hubConnection.InvokeAsync("Reset");
            return Ok();
        }
    }


}