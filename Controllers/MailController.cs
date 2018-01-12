using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFDC.Controllers
{
    public class MailController : Controller
    {
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Branch> _branchrDataService;
        private IDataService<Puesto> _puestoDataService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Mail> _mailDataService;
        const string message = "_Message";

        public MailController(IDataService<Profile> service,
                                 UserManager<IdentityUser> managerService,
                                 IDataService<Branch> branchService,
                                 IDataService<Puesto> puestoService,
                                 RoleManager<IdentityRole> roleService,
                                 IDataService<Mail> mailService
            )
        {
            _profileDataService = service;
            _userManagerService = managerService;
            _branchrDataService = branchService;
            _puestoDataService = puestoService;
            _roleManagerService = roleService;
            _mailDataService = mailService;
        }

        [Authorize]
        public async Task<IActionResult> Index(bool act)
        {
            ////////// READ ALL MAIL TO ME  /////////////          
            IEnumerable<Mail> inboxToMe = _mailDataService.Query(ib => ib.MailToName == User.Identity.Name && ib.Visto == act);           
            ////////// READ ALL MAILS CC ME /////////////
            IdentityUser u = await _userManagerService.FindByNameAsync(User.Identity.Name);
            IEnumerable<Mail> inbox = _mailDataService.Query(ib => ib.Visto == act && ib.CC != null);
            var inboxCC = inbox.Where(x => x.CC.Contains(u.Id));

            MailIndexViewModel vm = new MailIndexViewModel
            {
                Total = inboxToMe.Count(),
                InBoxToMe = inboxToMe,
                InBoxCC = inboxCC,
                TotalCC = inboxCC.Count(),
                Act= act
            };
            return View(vm);
        }

        [Authorize]
        public IActionResult Details(int mailId, int mailIdcc)
        {
            ////////// Get Mail details  /////////////   
            Mail MailDetails;
            bool mailCC;
            int mailIdFinal;

            if (mailId ==0)
            {
                MailDetails = _mailDataService.GetSingle(md => md.MailId == mailIdcc);
                mailCC = true;
                mailIdFinal = mailIdcc;
            }
            else
            {
                MailDetails = _mailDataService.GetSingle(md => md.MailId == mailId);
                mailCC = false;
                mailIdFinal = mailId;
            }     
            

            MailDetailViewModel vm = new MailDetailViewModel
            {
                MailId = mailIdFinal,
                From = MailDetails.SenderName,
                Subject = MailDetails.Subject,
                Body = MailDetails.Body,
                Fecha = MailDetails.Fecha,
                mailcc= mailCC
            };

            MailDetails.Visto = true;
            _mailDataService.Update(MailDetails);

            return View(vm);
        }

        [Authorize]
        public IActionResult NewMail()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> GetUsersMail()
        {
            // Get the user Id
       //     IdentityUser u = await _userManagerService.FindByNameAsync(User.Identity.Name);
            IEnumerable<IdentityUser> users = await _userManagerService.GetUsersInRoleAsync("Mail");
            var us = users.Select(i => new { i.UserName, i.Id }).ToList();
            return Json(us);
        }

        [Authorize]
        public async Task<IActionResult> ReceiveMail( string CC, string Asunto, string Mensaje, string Dest)
        {
            IdentityUser u = await _userManagerService.FindByNameAsync(User.Identity.Name);
            IdentityUser mtn = await _userManagerService.FindByIdAsync(Dest);
           

            Mail correo = new Mail
            {
                SenderId = u.Id,
                SenderName = User.Identity.Name,
                Fecha = DateTime.Now,
                Subject = Asunto,
                MailTo = Dest,
                MailToName = mtn.UserName,
                CC = CC,
                Body = Mensaje,
                Visto= false,
                Estado = 1
            };
            _mailDataService.Create(correo);
            string ok = "{'status': 'OK'}";
            return Json(ok);
        }
        public IActionResult MarkAsUnread(int mailId)
        {
            Mail MailDetails = _mailDataService.GetSingle(md => md.MailId == mailId);
            MailDetails.Visto = false;
            _mailDataService.Update(MailDetails);

            string ok = "{'status': 'OK'}";
            return Json(ok);
        }
    }
}
