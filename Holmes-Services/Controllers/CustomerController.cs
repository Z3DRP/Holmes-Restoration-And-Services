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
            string defaultSort = nameof(Customer.First_Name);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Customer>
            {
                Includes = "Designs.Id, Jobs.Id",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };

            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = c => c.First_Name;
            else
                options.OrderBy = c => c.Last_Name;

            var vm = new CustomerListViewModel
            {
                Customers = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var customer = data.Get(new QueryOptions<Customer>
            {
                Where = c => c.Id == id
            });

            CustomerDTO cDTO = new CustomerDTO();
            cDTO.Load(customer);

            return View(cDTO);
        }
        public IActionResult GetMyId(string firstname, string lastname)
        {
            Customer customer = (Customer)hctx.Customers.Where(c => c.First_Name == firstname && c.Last_Name == lastname);

            // add id to the session and redirect to GetMyId page
            // need to implement 
            if (customer != null)
            {
                var session = new CustomerSession(HttpContext.Session);
                session.SetId(customer.Id);
                session.SetFirstname(firstname);
                session.SetLastname(lastname);
            }

            return View(customer);
        }

        public IActionResult MyAccount(int id)
        {
            var customer = data.Get(new QueryOptions<Customer>
            {
                Includes = "Designs.Id, Jobs.Id",
                Where = c => c.Id == id
            });

            CustomerUnitDTO cdto = new CustomerUnitDTO();
            cdto.Load(customer);

            return View(cdto);
        }

        public IActionResult MyDesigns(int id)
        {
            var designs = hctx.Designs.Where(d => d.Customer_Id == id);
            List<DesignDTO> dtos = new List<DesignDTO>();
            DesignDTO? dto = new DesignDTO();

            foreach (Design design in designs)
            {
                dto.Load(design);
                dtos.Append(dto);
            }

            return View(dtos);
        }
        public IActionResult MyJobs(int id)
        {
            var jobs = hctx.Jobs.Where(j => j.Customer_Id == id);
            List<JobDTO> dtos = new List<JobDTO>();
            JobDTO dto = new JobDTO();

            foreach (Job job in jobs)
            {
                dto.Load(job);
                dtos.Append(dto);
            }

            return View(dtos);
        }
    }
}
