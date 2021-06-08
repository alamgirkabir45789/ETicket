using ETicket.Models;
using ETicket.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentRepository db;
        private readonly IHostingEnvironment appEnvironment;


        public AgentController(IAgentRepository db, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(db.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateRolePolicy")]      
        public async Task<ActionResult> Create(Agent agent)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";

                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(appEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            //var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;

                            }

                        }
                    }
                }

                var data = new Agent()
                {
                    Name = agent.Name,
                    Address = agent.Address,
                    Email = agent.Email,
                    PhoneNo = agent.PhoneNo,
                    JoiningDate = agent.JoiningDate,
                    Age = agent.Age,
                    UrlImage = UrlImage,

                };

                db.Add(data);
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Index");
        }
     
        [Authorize(Policy = "DeleteRolePolicy")]
        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(db.GetAgent(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]      
        [Authorize(Policy = "EditRolePolicy")]    
        public async Task<ActionResult> Edit(int id, Agent agent)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(appEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;
                            }

                        }
                    }
                }
                var data = db.GetAgent(id);
                data.Name = agent.Name;
                data.Address = agent.Address;
                data.Email = agent.Email;
                data.PhoneNo = agent.PhoneNo;
                data.JoiningDate = agent.JoiningDate;
                data.Age = agent.Age;
                data.UrlImage = UrlImage;

                db.Update(data);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.GetAgent(id));
        }
    }
}
