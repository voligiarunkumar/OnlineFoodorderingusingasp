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
            //listing only veg items into the drop down list
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e=>e.FoodType=="veg"), "FoodName", "FoodName");
            return View();
        }
        public IActionResult Create1()
        {
            //listing only non-veg items into the drop dowm list
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "Non-veg"), "FoodName", "FoodName");
            return View();
        }

        // POST: Orders/OrderTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //this is for veg create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserName,FoodName,DateofOrder,Quantity,CustomerAddress")] OrderTable orderTable)
        {
            //checking wether the duplicate is found or not if found user should be able to add the order else need to register

            bool isDuplicateFoundusername
                    = _context.CustomersTable.Any(c => c.UserName == orderTable.UserName);

           
            if (isDuplicateFoundusername)
            {
                //cheking wether selected date is in the present date and future one not in the past
                if (System.DateTime.Now.Date <= orderTable.DateofOrder.Date)
                {
                    _context.Add(orderTable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("DateofOrder", "Date of order should be future not past");
                }
            }
            else{
                ModelState.AddModelError("UserName", "UserName invalid with our database you may new to us please Register");
            }
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "veg"), "FoodName", "FoodName");
            return View(orderTable);
        }
        // this is for non-veg create1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("OrderID,UserName,FoodName,DateofOrder,Quantity,CustomerAddress")] OrderTable orderTable)
        {
            //checking wether the duplicate is found or not if found user should be able to add the order else need to register
            bool isDuplicateFoundusername
                    = _context.CustomersTable.Any(c => c.UserName == orderTable.UserName);


            if (isDuplicateFoundusername)
            {
                //checking with the date where date should not be in the past it should be future one.
                if (System.DateTime.Now.Date <= orderTable.DateofOrder.Date)
                {
                    _context.Add(orderTable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("DateofOrder", "Date of order should be future not past");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "UserName invalid with our database you may new to us please Register");
            }
            //displaying to drop down list to select particular item from the list
            ViewData["FoodName"] = new SelectList(_context.FoodTables.Where(e => e.FoodType == "Non-veg"), "FoodName", "FoodName");
            return View(orderTable);
        }

        // GET: Orders/OrderTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //cheking whether the item is null or not and returning
            if (id == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrdersTable.FindAsync(id);
            if (orderTable == null)
            {
                return NotFound();
            }
            //displaying to drop down list to select particular item from the list
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
            //displaying to drop down list to select particular item from the list
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
        //deleting with respect to the id that provided by the admin whlie clicking on the particular item to be deleted
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
