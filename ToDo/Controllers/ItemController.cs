using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ItemController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var items = dbContext.items;
            return View(items.ToList());
        }
        public IActionResult Error()
        {
           

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (item != null)
            {
                var newItem = dbContext.items.Add(new Item()
                {
                    Title = item.Title,
                    Description = item.Description,
                    DeadLine = item.DeadLine,
                });
                dbContext.SaveChanges();
                TempData["Notification"] = "Created Item Successfully";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }
        [HttpGet]
        public IActionResult Edit(int itemId)
        {
            var item = dbContext.items.Find(itemId);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            if (item != null) {
                var editItem = dbContext.items.Update(new Item()
                {
                    DeadLine = item.DeadLine,
                    Description = item.Description,
                    Title = item.Title
                });
                dbContext.SaveChanges();
                TempData["Notification"] = "Edited Item Successfully";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }
        public IActionResult Delete(int itemId)
        {
            var item = dbContext.items.Find(itemId);
            if (item != null)
            {
                dbContext.items.Remove(item);
                dbContext.SaveChanges();
                TempData["Notification"] = "Deleted Item Successfully";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Error));
        }
    }
}
