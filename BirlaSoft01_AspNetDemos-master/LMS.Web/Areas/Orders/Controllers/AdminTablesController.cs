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
    public class AdminTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders/AdminTables
        //admin has all rights to remove customer , edit customer details, delete customer, and manage there orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminsTable.ToListAsync());

        }
        public async Task<IActionResult> Index1()
        {
            return View(await _context.AdminsTable.ToListAsync());

        }



        // GET: Orders/AdminTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminTable = await _context.AdminsTable
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (adminTable == null)
            {
                return NotFound();
            }

            return View(adminTable);
        }

        // GET: Orders/AdminTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/AdminTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminName,Password")] AdminTable adminTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminTable);
        }

        // GET: Orders/AdminTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminTable = await _context.AdminsTable.FindAsync(id);
            if (adminTable == null)
            {
                return NotFound();
            }
            return View(adminTable);
        }

        // POST: Orders/AdminTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminName,Password")] AdminTable adminTable)
        {
            if (id != adminTable.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminTableExists(adminTable.AdminId))
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
            return View(adminTable);
        }

        // GET: Orders/AdminTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminTable = await _context.AdminsTable
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (adminTable == null)
            {
                return NotFound();
            }

            return View(adminTable);
        }

        // POST: Orders/AdminTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminTable = await _context.AdminsTable.FindAsync(id);
            _context.AdminsTable.Remove(adminTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminTableExists(int id)
        {
            return _context.AdminsTable.Any(e => e.AdminId == id);
        }
    }
}
