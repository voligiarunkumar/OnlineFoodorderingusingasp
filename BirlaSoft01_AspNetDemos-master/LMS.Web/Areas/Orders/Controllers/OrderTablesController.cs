using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodOrdering.Web.Models;
using LMS.Web.Data;
using Microsoft.Data.SqlClient;

namespace FoodOrdering.Web.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrderTablesController : Controller
    {
        private readonly ApplicationDbContext _context;
      

        public OrderTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: Orders/OrderTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrdersTable.Include(o => o.FoodTable);
           // var lastOrder=_context.OrdersTable.Include(o => o.FoodTable).Max(m => m.OrderID);
                

            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
            var applicationDbContext = _context.OrdersTable.Include(o => o.FoodTable);
            return View(await applicationDbContext.ToListAsync());
        }
        

        // GET: Orders/OrderTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrdersTable
                .Include(o => o.FoodTable)
                .FirstOrDefaultAsync(m => m.OrderID == id);

        // var lastOrder=_context.OrdersTable.Include(o => o.FoodTable).Max(m => m.OrderID);


            if (orderTable == null)
            {
                return NotFound();
            }

            return View(orderTable);
        }

        // GET: Orders/OrderTables/Create
        public IActionResult Create()
        {
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e=>e.FoodType=="veg"), "FoodName", "FoodName");
            return View();
        }
        public IActionResult Create1()
        {
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "Non-veg"), "FoodName", "FoodName");
            return View();
        }

        // POST: Orders/OrderTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserName,FoodName,DateofOrder,Quantity,CustomerAddress")] OrderTable orderTable)
        {
            bool isDuplicateFoundusername
                    = _context.CustomersTable.Any(c => c.UserName == orderTable.UserName);


            if (isDuplicateFoundusername)
            {
                _context.Add(orderTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else{
                ModelState.AddModelError("UserName", "UserName invalid with our database you may new to us please Register");
            }
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "veg"), "FoodName", "FoodName");
            return View(orderTable);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("OrderID,UserName,FoodName,DateofOrder,Quantity,CustomerAddress")] OrderTable orderTable)
        {
            bool isDuplicateFoundusername
                    = _context.CustomersTable.Any(c => c.UserName == orderTable.UserName);


            if (isDuplicateFoundusername)
            {
                _context.Add(orderTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("UserName", "UserName invalid with our database you may new to us please Register");
            }
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "Non-veg"), "FoodName", "FoodName");
            return View(orderTable);
        }

        // GET: Orders/OrderTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrdersTable.FindAsync(id);
            if (orderTable == null)
            {
                return NotFound();
            }
            ViewData["FoodName"] = new SelectList(_context.FoodTables, "FoodName", "FoodName", orderTable.FoodName);
            return View(orderTable);
        }

        // POST: Orders/OrderTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserName,FoodName,DateofOrder,Quantity,CustomerAddress")] OrderTable orderTable)
        {
            if (id != orderTable.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderTableExists(orderTable.OrderID))
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
            ViewData["FoodName"] = new SelectList(_context.FoodTables, "FoodName", "FoodName", orderTable.FoodName);
            return View(orderTable);
        }

        // GET: Orders/OrderTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrdersTable
                .Include(o => o.FoodTable)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orderTable == null)
            {
                return NotFound();
            }

            return View(orderTable);
        }

        // POST: Orders/OrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderTable = await _context.OrdersTable.FindAsync(id);
            _context.OrdersTable.Remove(orderTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderTableExists(int id)
        {
            return _context.OrdersTable.Any(e => e.OrderID == id);
        }
    }
}
