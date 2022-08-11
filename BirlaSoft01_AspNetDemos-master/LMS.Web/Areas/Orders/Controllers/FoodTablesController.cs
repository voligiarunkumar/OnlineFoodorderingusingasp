using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodOrdering.Web.Models;
using LMS.Web.Data;

namespace FoodOrdering.Web.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class FoodTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders/FoodTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodTables.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
            return View(await _context.FoodTables.ToListAsync());
        }
        // GET: Orders/FoodTables/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTable = await _context.FoodTables
                .FirstOrDefaultAsync(m => m.FoodName == id);
            if (foodTable == null)
            {
                return NotFound();
            }

            return View(foodTable);
        }

        // GET: Orders/FoodTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/FoodTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,UserName,FoodName,FoodPrice,FoodType")] FoodTable foodTable)
        {
            bool isDuplicateFoundusername
                   = _context.AdminsTable.Any(c => c.AdminName == foodTable.UserName);
            bool isDuplicateFoundFoodName
                = _context.FoodTables.Any(c => c.FoodName == foodTable.FoodName);
            if (isDuplicateFoundusername && !isDuplicateFoundFoodName)
            {
                _context.Add(foodTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                ModelState.AddModelError("FoodName", "UserName Invalid Or Food Item Already Present");
            }
          
            return View(foodTable);
        }

        // GET: Orders/FoodTables/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTable = await _context.FoodTables.FindAsync(id);
            if (foodTable == null)
            {
                return NotFound();
            }
            return View(foodTable);
        }

        // POST: Orders/FoodTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,FoodName,FoodPrice,FoodType")] FoodTable foodTable)
        {
            if (id != foodTable.FoodName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodTableExists(foodTable.FoodName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodTable);
        }

        // GET: Orders/FoodTables/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodTable = await _context.FoodTables
                .FirstOrDefaultAsync(m => m.FoodName == id);
            if (foodTable == null)
            {
                return NotFound();
            }

            return View(foodTable);
        }

        // POST: Orders/FoodTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var foodTable = await _context.FoodTables.FindAsync(id);

            _context.FoodTables.Remove(foodTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodTableExists(string id)
        {
            return _context.FoodTables.Any(e => e.FoodName == id);
        }
    }
}
