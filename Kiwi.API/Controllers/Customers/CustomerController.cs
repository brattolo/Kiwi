using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Kiwi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IGetCustomersListQuery _query;

        public CustomersController(IGetCustomersListQuery query)
        {
            _query = query;
        }

        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _query.Execute();

            return Ok(customers);
        }
    }
}
