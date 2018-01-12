using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDC.Controllers
{
    public class OrderController : Controller
    {
        private IDataService<Order> _orderDataService;
        private IDataService<OrderDetail> _orderDetailDataService;
        private IDataService<Category> _categoryDataService;
        private IDataService<Branch> _branchDataService;
        private IDataService<Product> _productDataService;
        private IDataService<SubCategory> _subCategoryDataService;
        private UserManager<IdentityUser> _userManagerService;
        public OrderController(IDataService<Order> service,
                                IDataService<Category> service1,
                                IDataService<Branch> service2,
                                IDataService<Product> service3,
                                IDataService<SubCategory> service4,
                                IDataService<OrderDetail> service5,
                                 UserManager<IdentityUser> service6)
        {
            _orderDataService = service;
            _categoryDataService = service1;
            _branchDataService = service2;
            _productDataService = service3;
            _subCategoryDataService = service4;
            _orderDetailDataService = service5;
            _userManagerService = service6;
        }
      
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            //Create vm
            IEnumerable<Order> list = _orderDataService.GetAll();
            IEnumerable<Branch> branchList = _branchDataService.GetAll();
            IEnumerable<Category> categoryList = _categoryDataService.GetAll();

            OrderIndexViewModel vm = new OrderIndexViewModel
            {
                Total = list.Count(),
                Orders = list,
                Categories = categoryList,
                Branches = branchList
            };

            return View(vm);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            //Create vm
            IEnumerable<Branch> branchList = _branchDataService.GetAll();
            IEnumerable<Category> categoryList = _categoryDataService.GetAll();

            OrderCreateViewModel vm = new OrderCreateViewModel
            {
                Categories = categoryList,
            };

            return View(vm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(OrderCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create2", "Order", new { id = vm.CategoryId });
            }
            // if not valid
            return View(vm);
        }
        [HttpGet]
       [Authorize]
        public IActionResult Create2(int id)
        {
            IEnumerable<Product> productList = _productDataService.Query(p => p.CategoryId == id).OrderBy(s => s.SubCategoryId);
       //     IEnumerable<Product> productList = _productDataService.Query(p => p.CategoryId == id);
            Category category = _categoryDataService.GetSingle(c => c.CategoryId == id);
            IEnumerable<SubCategory> subCategoryiesList = _subCategoryDataService.Query(s => s.CategoryId == id);
            OrderCreate2ViewModel vm2 = new OrderCreate2ViewModel
            {
                Products = productList,
                Total = productList.Count(),
                Category = category.Name,
                CategoryId = id,
                Subcategories = subCategoryiesList
            };
            return View(vm2);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create2(OrderCreate2ViewModel vm2)
        {
            IEnumerable<Product> productList = _productDataService.Query(p => p.CategoryId == vm2.CategoryId);
            // Get the user Id
            IdentityUser u = await _userManagerService.FindByNameAsync(User.Identity.Name);
            Order o = new Order
            {
                CategoryId = vm2.CategoryId,
                OrderStateId = 1,
                Date = System.DateTime.Now,
                BranchId = 1,
                UserCreateId = u.Id
            };
            _orderDataService.Create(o);

            foreach (string key in HttpContext.Request.Form.Keys)
            {
                string field = key;
                string s = HttpContext.Request.Form[key];
                if (s == ",,")
                {
                    //Go new iteration
                }
                else if (field == "CategoryId" || field == "__RequestVerificationToken")
                {
                    //Go new iteration
                }
                else
                {
                    var ss = s.Split(',').Select(x => int.Parse(x)).Take(2).ToArray();
                    int Stock = ss[0];
                    int Cantidad = ss[1];
                    string Obs = s.Split(',').Select(x => x).Last();

                    if (Cantidad <= 0)
                    {
                        //Go new iteration
                    }
                    else
                    {
                        int fieldInt = Convert.ToInt32(key);
                        OrderDetail od = new OrderDetail
                        {
                            OrderId = o.OrderId,
                            SubCategory = 1,
                            Category = vm2.CategoryId,
                            Quantity = Cantidad,
                            ProductId = fieldInt,
                            Stock = Stock,
                            ObsOrder = Obs
                        };
                        _orderDetailDataService.Create(od);
                    }                         
                 }                    
            }
            return RedirectToAction("Index", "Order");
        }

        [Authorize]
        public IActionResult Detail(int orderId)
        {
            //Create vm
            Order order = _orderDataService.GetSingle(o => o.OrderId == orderId);
            IEnumerable<OrderDetail> orderDetail = _orderDetailDataService.Query(o => o.OrderId == orderId);
            var subcategorias = orderDetail.Select(sc => sc.SubCategory).Distinct().ToList();
            Branch branch = _branchDataService.GetSingle(b => b.BranchId == order.BranchId);
            Category category = _categoryDataService.GetSingle(c => c.CategoryId == order.CategoryId);        
            IEnumerable<SubCategory> subCategoryiesList = _subCategoryDataService.Query(s => s.CategoryId == order.CategoryId);
            //Order by SubCategory !!!!!!
            IEnumerable<Product> ProductList = _productDataService.Query(p => p.CategoryId == order.CategoryId).OrderBy(s => s.SubCategoryId);

            OrderDetailViewModel vm = new OrderDetailViewModel
            {
                Total = orderDetail.Count(),
                OrderId = orderId,
                BranchName = branch.Name,
                CategoryName = category.Name,
                Subcategories = subCategoryiesList,
                //    State 
                Date = order.Date,
                OrderDetails = orderDetail,
                Products = ProductList
            };

            return View(vm);
        }
    }
}


