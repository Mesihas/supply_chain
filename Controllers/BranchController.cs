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
    public class BranchController : Controller
    {
        private IDataService<Branch> _branchDataService;
        public BranchController(IDataService<Branch> service)
        {        
            _branchDataService = service;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            IEnumerable<Branch> list = _branchDataService.GetAll();

            BranchIndexViewModel vm = new BranchIndexViewModel
            {
                Total = list.Count(),
                Branches = list
            };
            return View(vm);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(BranchCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Map to object Order
                Branch b = new Branch
                {
                    Name = vm.Name,
                    Address = vm.Address,
                    Phone = vm.Phone

                };
                // save to db
                _branchDataService.Create(b);
                // Go gome
                return RedirectToAction("Index", "Branch");
            }
            // if not valid
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int id)
        {
            //get the Branch from database
            Branch bra = _branchDataService.GetSingle(p => p.BranchId == id);
            BranchUpdateViewModel vm = new BranchUpdateViewModel
            {
                BranchId = id,
                Name = bra.Name,
                Address = bra.Address,
                Phone = bra.Phone,              
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(BranchUpdateViewModel vm, int BranchId)
        {
            if (ModelState.IsValid)
            {
                Branch b = new Branch
                {
                    BranchId = BranchId,
                    Name = vm.Name,
                    Address = vm.Address,
                    Phone = vm.Phone,
                };

                _branchDataService.Update(b);
                return RedirectToAction("Index", "Branch");
            }
            return View(vm);
        }
    }
}
