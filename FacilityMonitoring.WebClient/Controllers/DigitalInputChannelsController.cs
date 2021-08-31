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

namespace FacilityMonitoring.WebClient.Controllers
{
    [Route("api/[controller]")]
    public class DigitalInputChannelsController : ControllerBase
    {

        private readonly FacilityContext _context;
        private HubConnection _hubConnection;

        public DigitalInputChannelsController(FacilityContext context) {
            this._context = context;
            this._context.Registers.Include(e => e.SensorType).Load();
            this._hubConnection = new HubConnectionBuilder()
                .WithUrl("http://172.20.4.209:443/hubs/gasbay")
                //.WithUrl("http://localhost:5000/hubs/gasbay")
                .Build();
        }

        [HttpGet]
        public async Task<object> DigitalInputRegisters(DataSourceLoadOptions loadOptions) {
            var result = await Task.Run(() => {
                return DataSourceLoader.Load(this._context.Registers.OfType<DigitalInputChannel>().Include(e => e.SensorType).OrderBy(e => e.RegisterIndex), loadOptions);
            });
            await this._hubConnection.StartAsync();
            return result;
        }


        [HttpPut]
        public async Task<IActionResult> DigitalChannelUpdate(int key, string values) {
            var register = await this._context.Registers.OfType<DigitalInputChannel>().Include(e => e.SensorType).SingleOrDefaultAsync(e => e.Id == key);
            JsonConvert.PopulateObject(values, register);
            this._context.Entry<DigitalInputChannel>(register).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            await this._hubConnection.InvokeAsync("Reset");
            return Ok();
        }
    }
}