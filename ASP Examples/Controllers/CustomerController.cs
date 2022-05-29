using System;
using Microsoft.AspNetCore.Mvc;
using ASP_Examples.Models;
using ASP_Examples.Repositories;

namespace ASP_Examples.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> repository;

        public CustomerController(IRepository<Customer> repository)
        { 
            this.repository = repository;
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = repository.All();

                return View(customers);
            }
            else
            {
                var customer = repository.Get(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
