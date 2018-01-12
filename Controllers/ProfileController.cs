using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EFDC.Controllers
{
    public class ProfileController : Controller
    {
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Branch> _branchrDataService;
        private IDataService<Puesto> _puestoDataService;
        private RoleManager<IdentityRole> _roleManagerService;
        const string message = "_Message";

        public ProfileController(IDataService<Profile> service,  
                                 UserManager<IdentityUser> managerService, 
                                 IDataService<Branch> branchService,
                                 IDataService<Puesto> puestoService,
                                 RoleManager<IdentityRole> roleService
            )
        {
            _profileDataService = service;
            _userManagerService = managerService;
            _branchrDataService = branchService;
            _puestoDataService = puestoService;
            _roleManagerService = roleService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Branch> branchList = _branchrDataService.GetAll();
            IEnumerable<Profile> ProfileList = _profileDataService.GetAll();

            ProfileListViewModel vm = new ProfileListViewModel
            {
                Profiles = ProfileList,
                Branches = branchList
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateProfile(int id)
        {
            Profile pro = _profileDataService.GetSingle(p => p.ProfileId == id);
            IEnumerable<Branch> branchList = _branchrDataService.GetAll();
            IEnumerable<Puesto> puestoList = _puestoDataService.GetAll();

            ProfileUpdateViewModel vm = new ProfileUpdateViewModel
            {
                ProfileId = pro.ProfileId,
                UserName = pro.UserName,
                FirstName = pro.FirstName,
                LastName = pro.LastName,
                Role = pro.Role,
                BranchId = pro.BranchId,
                Legajo = pro.Legajo,
                FechaIngreso = pro.FechaIngreso,
                Preocupacional = pro.Preocupacional,
                VtoLibreta = pro.VtoLibreta,
                Activo  = pro.Activo,
                Birthday = pro.Birthday,
                CUIL = pro.CUIL,
                Nacionalidad = pro.Nacionalidad,
                Direccion = pro.Direccion,
                Localidad = pro.Localidad,
                CP = pro.CP,
                TelMobil = pro.TelMobil,
                LandLine = pro.LandLine,
                EstadoCivil = pro.EstadoCivil,
                Hijos = pro.Hijos,
                Branches = branchList,
                Puestos = puestoList
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateProfile(ProfileUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                 Profile p = new Profile
                {
                    UserName = User.Identity.Name,
                    ProfileId = vm.ProfileId,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Role = vm.Role,
                    BranchId = vm.BranchId,
                    Legajo = vm.Legajo,
                    FechaIngreso = vm.FechaIngreso,
                    Preocupacional = vm.Preocupacional,
                    VtoLibreta = vm.VtoLibreta,
                    Activo = vm.Activo,
                    Birthday = vm.Birthday,
                    CUIL = vm.CUIL,
                    Nacionalidad = vm.Nacionalidad,
                    Direccion = vm.Direccion,
                    Localidad = vm.Localidad,
                    CP = vm.CP,
                    TelMobil = vm.TelMobil,
                    LandLine = vm.LandLine,
                    EstadoCivil = vm.EstadoCivil,
                    Hijos = vm.Hijos,
                 };

                _profileDataService.Update(p);
                ////update Identity email
                //IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //user.Email = vm.Email;
                //await _userManagerService.UpdateAsync(user);
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("List", "Profile");
            }
            return View(vm);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RolesAssign(int id)
        {
            Profile pro = _profileDataService.GetSingle(p => p.ProfileId == id);
            // Get the user Id
            IdentityUser u = await _userManagerService.FindByNameAsync(pro.UserName);         
            // IEnumerable<IdentityRole> rolex =  _roleManagerService.Roles.OrderBy(x => x.Name); //// All Roles 
            // Roles for a User ( just list)
            IList <string> roles = await _userManagerService.GetRolesAsync(u);
            //Roles Available List
            IEnumerable<IdentityRole> AllRoles = _roleManagerService.Roles.ToList(); //Roles Enum Complete 
            IList<string> rolesAva = AllRoles.Select(x => x.Name).ToList(); // Roles List only Role Names
            IList<string>  RolesAvalilable = rolesAva.Except(roles).ToList(); // Roles Available list after compare with user's roles

            RolesAssignViewModel vm = new RolesAssignViewModel
            {
                Roles = roles,
                User = pro.UserName,
                RolesAvalilable = RolesAvalilable,
                ProfileId = id,
                Message = HttpContext.Session.GetString(message)
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RolesAssign(RolesAssignViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // create the user
                IdentityUser u = await _userManagerService.FindByNameAsync(vm.User);

                if (vm.rol == null)
                {                 
                    HttpContext.Session.SetString(message, "No Role left to perform the Action");
                    return RedirectToAction("RolesAssign", "Profile", new { id = vm.ProfileId });
                }
                else
                {
                    if (vm.Action == "add")
                    {
                        await _userManagerService.AddToRoleAsync(u, vm.rol);
                        HttpContext.Session.SetString(message, "Role added successfully");
                    }
                    else if (vm.Action == "remo")
                    {
                        await _userManagerService.RemoveFromRoleAsync(u, vm.rol);
                        HttpContext.Session.SetString(message, "Role removed successfully");
                    }
                    else
                    {
                    }
                    return RedirectToAction("RolesAssign", "Profile", new { id = vm.ProfileId });
                }
            }
            else
            {
                
            }
            return RedirectToAction("RolesAssign", "Profile", new { id = vm.ProfileId });   
        }
    }
}
  