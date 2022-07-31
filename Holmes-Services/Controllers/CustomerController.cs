using Holmes_Services.Data_Access.Repos;
using Holmes_Services.Models;
using Holmes_Services.Models.DomainModels;
using Holmes_Services.Models.DTOs;
using Holmes_Services.Models.Extensions;
using Holmes_Services.Models.Grids;
using Holmes_Services.Models.QueryOptions;
using Holmes_Services.Models.Repositories;
using Holmes_Services.Models.Sessions;
using Holmes_Services.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Holmes_Services.Controllers
{
    public class CustomerController : Controller
    {
        private Repo<Customer> data { get; set; }
        private HolmesContext hctx { get; set; }
        public CustomerController(HolmesContext ctx)
        {
            this.hctx = ctx;
            data = new Repo<Customer>(ctx);
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(GridDTO vals)
        {
            IEnumerable<Customer> custoemrs = CustomerRepo.GetAllCustomers();
            return custoemrs == null ? View(Enumerable.Empty<Customer>()) : View(custoemrs);
        }

        public IActionResult Details(int id)
        {
            Customer customer = CustomerRepo.GetCustomerById(id);
            return customer == null ? View(new Customer()) : View(customer);
        }
        [HttpGet]
        public IActionResult GetMyId(string firstname, string lastname)
        {
            Customer customer = CustomerRepo.GetCustomerByName(firstname, lastname);
            // add id to the session and redirect to GetMyId page
            // need to implement 
            if (customer != null)
            {
                var session = new CustomerSession(HttpContext.Session);
                session.SetId(customer.Id);
                session.SetFirstname(firstname);
                session.SetLastname(lastname);
                TempData["message"] = "Customer Id Found";
            }
            else
                TempData["message"] = $"Customer Id Not Found For {firstname} {lastname}, please try again later";

            return View(customer);
        }

        public IActionResult MyAccount(int id)
        {
            // need sp joins to get designs and jobs
            // only pull certain columns from db and put in view model
        }

        [HttpGet]
        public IActionResult MyDesigns(int id)
        {
            IEnumerable<Design> designs = DesignRepo.GetCustomerDesigns(id);
            // need vm
            return designs == null ? View(Enumerable.Empty<Design>()) : View(designs);
        }
        public IActionResult MyJobs(int id)
        {
            IEnumerable<Job> jobs = JobRepo.GetCustomerJobs(id);
            return jobs == null ? View(Enumerable.Empty<Job>()) : View(jobs);
        }
    }
}
