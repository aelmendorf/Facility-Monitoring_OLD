using FacilityMonitoring_WebClient.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace FacilityMonitoring_WebClient.Controllers {

    [Route("api/[controller]")]
    public class OrdersController : Controller {

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions) {

            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

    }
}