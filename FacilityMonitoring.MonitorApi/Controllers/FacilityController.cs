using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacilityMonitoring.MonitorApi.Controllers {
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase {
        private readonly FacilityContext _context;

        public FacilityController(FacilityContext context) {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModbusDevice>>> GetModbusDevices() {
            return await this._context.ModbusDevices.ToListAsync();
        }

        //GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModbusDevice>> GetModbusDevice(int id) {
            var item = await this._context.ModbusDevices.FindAsync(id);
            if (item == null) {
                return NotFound();
            }
            return item;
        }
    }
}
