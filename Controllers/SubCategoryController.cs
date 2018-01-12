using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EFDC.Controllers
{
    public class SubCategoryController : Controller
    {
        private IDataService<SubCategory> _subCcategoryDataService;
        private IDataService<Category> _categoryDataService;
        public SubCategoryController(IDataService<SubCategory> service, IDataService<Category> service1)
        {
            _subCcategoryDataService = service;
            _categoryDataService = service1;
        }
       
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int Id)
        {
            IEnumerable<SubCategory> list = _subCcategoryDataService.Query(s => s.CategoryId == Id);
            //Get Category name 
            Category cat = _categoryDataService.GetSingle(p => p.CategoryId == Id);
            //put CategoryName into session
   
            //Create vm
            SubCategoryIndexViewModel vm = new SubCategoryIndexViewModel
            {               
                Total = list.Count(),
                SubCategories = list,
                CategoryName = cat.Name,
                CategoryId = Id
            };
           return View(vm);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create(int Id)
        {
            //Get Category name 
            Category cat = _categoryDataService.GetSingle(p => p.CategoryId == Id);
            SubCategoryCreateViewModel vm = new SubCategoryCreateViewModel
            {
                CategoryName = cat.Name,
                CategoryId = Id
            };
            return View(vm);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(SubCategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // map the vm
                SubCategory s = new SubCategory
                {
                    SubCategoryName = vm.SubCategoryName,
                    CategoryId = vm.CategoryId
                };
                // save to db
                _subCcategoryDataService.Create(s);
                // Go gome
                ModelState.Clear();
                return RedirectToAction("Index", "SubCategory", new { Id = vm.CategoryId });
            }
            // if not valid
            return View(vm);
        }

        //[Authorize(Roles = "Administrator")]
        //[HttpGet]
        //public IActionResult Update(int Id)
        //{
        //    //Get Category name 
        //    Category cat = _categoryDataService.GetSingle(p => p.CategoryId == Id);
        //    SubCategoryCreateViewModel vm = new SubCategoryCreateViewModel
        //    {
        //        CategoryName = cat.Name,
        //        CategoryId = Id
        //    };
        //    return View(vm);
        //}

        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //public IActionResult Update(SubCategoryCreateViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // map the vm
        //        SubCategory s = new SubCategory
        //        {
        //            SubCategoryName = vm.SubCategoryName,
        //            CategoryId = vm.CategoryId
        //        };
        //        // save to db
        //        _subCcategoryDataService.Create(s);
        //        // Go gome
        //        ModelState.Clear();
        //        return RedirectToAction("Index", "SubCategory", new { Id = vm.CategoryId });
        //    }
        //    // if not valid
        //    return View(vm);
        //}
    }
}
