using ETicket.Data;
using ETicket.Models;
using ETicket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment webHostEnvironment;
        public EmployeeController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var employees = from s in db.Employees
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.EmployeeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.EmployeeName);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.JoiningDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.JoiningDate);
                    break;

            }

            int pageSize = 3;
            return View(await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            var employeeViewModel = new EmployeeViewModel()
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Designation = employee.Designation,
                Address = employee.Address,
                ContactNo = employee.ContactNo,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                ExistingImage = employee.ProfilePicture
            };

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Employee auther = new Employee
                {
                    EmployeeName = model.EmployeeName,
                    Designation = model.Designation,
                    Address = model.Address,
                    ContactNo = model.ContactNo,
                    Email = model.Email,
                    JoiningDate = model.JoiningDate,
                    ProfilePicture = uniqueFileName
                };

                db.Add(auther);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees.FindAsync(id);
            var employeeViewModel = new EmployeeViewModel()
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Designation = employee.Designation,
                Address = employee.Address,
                ContactNo = employee.ContactNo,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                ExistingImage = employee.ProfilePicture
            };

            if (employee == null)
            {
                return NotFound();
            }
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]    
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await db.Employees.FindAsync(model.Id);
                employee.EmployeeName = model.EmployeeName;
                employee.Designation = model.Designation;
                employee.Address = model.Address;
                employee.ContactNo = model.ContactNo;
                employee.Email = model.Email;
                employee.JoiningDate = model.JoiningDate;

                if (model.EmployeePicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    employee.ProfilePicture = ProcessUploadedFile(model);
                }
                db.Update(employee);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            var employeeViewModel = new EmployeeViewModel()
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Designation = employee.Designation,
                Address = employee.Address,
                ContactNo = employee.ContactNo,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                ExistingImage = employee.ProfilePicture
            };
            if (employee == null)
            {
                return NotFound();
            }

            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auther = await db.Employees.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", auther.ProfilePicture);
            db.Employees.Remove(auther);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return db.Employees.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.EmployeePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.EmployeePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.EmployeePicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
