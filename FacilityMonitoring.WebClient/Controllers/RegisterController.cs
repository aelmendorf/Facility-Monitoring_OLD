using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace FacilityMonitoring.WebClient.Controllers
{
    public class RegisterController : Controller
    {

        public RegisterController() {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}