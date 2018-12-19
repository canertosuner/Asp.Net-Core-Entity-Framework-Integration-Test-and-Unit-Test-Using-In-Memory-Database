using System;
using System.Collections.Generic;
using Customer.Contract;
using Customer.Service;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customer
        [HttpGet]
        public ActionResult<List<CustomerDto>> Get()
        {
            return Ok(_customerService.GetAll());
        }

        // GET api/customer/id
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> Get(Guid id)
        {
            return Ok(_customerService.GetById(id));
        }

        // POST api/customer
        [HttpPost]
        public ActionResult Post([FromBody] CustomerDto customer)
        {
            _customerService.CreateNew(customer);
            return Ok();
        }

        // PUT api/customer
        [HttpPut]
        public ActionResult<CustomerDto> Put([FromBody] CustomerDto customer)
        {
            return Ok(_customerService.Update(customer));
        }

        // GET api/customer/getbycitycode/cityCode
        [HttpGet("getbycitycode/{cityCode}")]
        public ActionResult<List<CustomerDto>> GetByCityCode(string cityCode)
        {
            return Ok(_customerService.GetByCityCode(cityCode));
        }

    }
}
