using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDC.Controllers
{
    public class ProductController : Controller
    {
        private IDataService<Product> _productDataService;
        private IDataService<SubCategory> _subCategoryDataService;
        private IDataService<Category> _categoryDataService;
        private IDataService<Branch> _branchDataService;
        private IDataService<ProductAvalilability> _productAvailabilityDataService;

        public ProductController(IDataService<Product> service, 
                                IDataService<SubCategory> service1, 
                                IDataService<Category> service2,
                                IDataService<Branch> service3,
                                IDataService<ProductAvalilability> service4
                                )
        {
            _productDataService = service;
            _subCategoryDataService = service1;
            _categoryDataService = service2;
            _branchDataService = service3;
            _productAvailabilityDataService = service4;
        }

        public IActionResult Index(int Id)
        {
            //get the list of products for this category
            IEnumerable<Product> productList = _productDataService.Query(p => p.SubCategoryId == Id);
            SubCategory subcat = _subCategoryDataService.GetSingle(p => p.SubCategoryId == Id);
            //    string CatName = TempData["catName"].ToString();
            Category cat = _categoryDataService.GetSingle(p => p.CategoryId == subcat.CategoryId);
            // Create vm
            ProductIndexViewModel vm = new ProductIndexViewModel
            {
                Total = productList.Count(),
                Products = productList,
                SubCategoryId = Id,
                SubCategoryName = subcat.SubCategoryName,
                CategoryName = cat.Name,
                CategoryId = subcat.CategoryId
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(int subCatId)
        {
            SubCategory subcat = _subCategoryDataService.GetSingle(p => p.SubCategoryId == subCatId);
            Category cat = _categoryDataService.GetSingle(p => p.CategoryId == subcat.CategoryId);
            //map vm
            ProductCreateViewModel vm = new ProductCreateViewModel
            {
                CategoryName = subcat.SubCategoryName,
                SubCategoryName = cat.Name,
                CategoryId = cat.CategoryId,
                SubCategoryId = subCatId
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(ProductCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Product pro = new Product
                {
                    CategoryId = vm.CategoryId,
                    SubCategoryId = vm.SubCategoryId,
                    Name = vm.Name,
                    Details = vm.Details,
                    Codigo = vm.Codigo
                };

                _productDataService.Create(pro);
                return RedirectToAction("Index", "Product", new { id = vm.SubCategoryId });
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int prodId)
        {
            //get the product from database
            Product pro = _productDataService.GetSingle(p => p.ProductId == prodId);
            SubCategory subcat = _subCategoryDataService.GetSingle(s => s.SubCategoryId == pro.SubCategoryId);
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == subcat.CategoryId);

            ProductUpdateViewModel vm = new ProductUpdateViewModel
            {
                CategoryId = pro.CategoryId,
                SubCategoryId = pro.SubCategoryId,
                Name = pro.Name,
                Codigo = pro.Codigo,
                Details = pro.Details,
                ProductId = prodId,
                CategoryName = cat.Name,
                SubCategoryName = subcat.SubCategoryName
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(ProductUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Product pro = new Product
                {
                    ProductId = vm.ProductId,
                    Name = vm.Name,
                    Details = vm.Details,
                    Codigo = vm.Codigo,
                    SubCategoryId = vm.SubCategoryId,
                    CategoryId = vm.CategoryId
                };

                _productDataService.Update(pro);
                return RedirectToAction("Index", "Product", new { Id = vm.SubCategoryId });
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Availability()
        {
            IEnumerable<Branch> branches = _branchDataService.GetAll();
            IEnumerable<Category> catList = _categoryDataService.GetAll();

            ProductAvailabilityViewModel vm = new ProductAvailabilityViewModel
            {        
                Categories = catList,
                Branches = branches
            };
            return View(vm);
        }

        public IActionResult AvailAyax(int CategoryId)
        {
            IEnumerable<SubCategory> subcats = _subCategoryDataService.Query(s => s.CategoryId == CategoryId);
            var cats = subcats.Select(i => new { i.SubCategoryId, i.SubCategoryName }).ToList();
            return Json(cats);
        }

        public IActionResult AvailAyaxProd(int subCategoryId, int branchid)
        {
        //    IEnumerable<Product> products = _productDataService.Query(p => p.SubCategoryId == subCategoryId);
            IEnumerable<Product> products = _productDataService.Query(p => p.SubCategoryId == subCategoryId);
            IEnumerable<ProductAvalilability> listProdNotAvail = _productAvailabilityDataService.Query(p => p.BranchId == branchid
                                                                                                         && p.SubCategoryId == subCategoryId);
            var result = products.Where(p => !listProdNotAvail.Any(l => p.ProductId == l.ProductId));
            var prods = result.Select(i => new { i.ProductId, i.Name }).ToList();
            
            return Json(prods);
        }

        public IActionResult MakeNotAvailable(int prodId, int branchid)
        {
            //       System.Diagnostics.Debug.WriteLine("xxxxx");
             Product pro = _productDataService.GetSingle(x => x.ProductId == prodId);

            ProductAvalilability pa = new ProductAvalilability
            {
                BranchId = branchid,
                ProductId = prodId,
                CategoryId = pro.CategoryId,
                SubCategoryId = pro.SubCategoryId,
                ProductName = pro.Name,
                Available =  false
            };
            _productAvailabilityDataService.Create(pa);
            
            IEnumerable<ProductAvalilability> listProdAvail = _productAvailabilityDataService.Query(p => p.BranchId == branchid);
            return RedirectToAction("Availability", "Product");
        }

        public IActionResult ListNotAvailableByBranch(int branchid, int CategoryId, int subCategoryId)
        {
            IEnumerable<Product> products = _productDataService.Query(p => p.SubCategoryId == subCategoryId && p.CategoryId == CategoryId);
            IEnumerable<ProductAvalilability> listProdNotAvail = _productAvailabilityDataService.Query(p => p.BranchId == branchid 
                                                                                                         && p.SubCategoryId == subCategoryId);

            return Json(listProdNotAvail);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult MakeAvailable(int prodId, int branchid )
        {
            ProductAvalilability ProdNotAvail = _productAvailabilityDataService.GetSingle(p => p.BranchId == branchid
                                                                                                  && p.ProductId == prodId);        
            _productAvailabilityDataService.Delete(ProdNotAvail);
            IEnumerable<ProductAvalilability> listProdAvail = _productAvailabilityDataService.Query(p => p.BranchId == branchid);
            return Json(listProdAvail);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Charts()
        {
            return View();
        }
    }
}
