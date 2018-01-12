using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDC.Controllers
{
    public class CategoryController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Product> _productDataService;

        public CategoryController(IDataService<Category> service, IDataService<Product> service1)
        {
            _categoryDataService = service;
            _productDataService = service1;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> list = _categoryDataService.GetAll();
            //Create vm
            CategoryIndexViewModel vm = new CategoryIndexViewModel
            {
                Categories = list,
                Total = list.Count()
            };
            return View(vm);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // map the vm
                Category c = new Category
                {
                    Name = vm.Name,
                    Details = vm.Details
                };
                // save to db
                _categoryDataService.Create(c);
                // Go gome
                return RedirectToAction("Index", "Category");
            }
            // if not valid
            return View(vm);
        }

        //public IActionResult Details(int id)
        //{
        //    // add categoruId to the session
        //    TempData["catId"] = id.ToString();
        //    //     TempData.Keep("catId") = id.ToString();

        //    //get the single category form DB
        //    Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);

        //    //get the list of products for this category
        //    IEnumerable<Product> productList = _productDataService.Query(p => p.CategoryId == id);
        //    // Create vm

        //    CategoryDetailsViewModel vm = new CategoryDetailsViewModel
        //    {
        //        Name = cat.Name,
        //        Details = cat.Details,
        //        Products = productList,
        //        Total = productList.Count()
        //    };

        //    TempData["catName"] = cat.Name.ToString();

        //    return View(vm);
        //}

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int id)
        {
            //get the Branch from database
            Category cat = _categoryDataService.GetSingle(p => p.CategoryId == id);
            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryId = id,
                Name = cat.Name,
                Details = cat.Details
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(CategoryUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category
                {
                    CategoryId = vm.CategoryId,
                    Name = vm.Name,
                    Details = vm.Details
                };

                _categoryDataService.Update(c);
                return RedirectToAction("Index", "Category");
            }
            return View(vm);
        }
    }
}
