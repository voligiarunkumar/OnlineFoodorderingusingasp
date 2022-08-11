﻿using System;
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
    public class CustomerTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders/CustomerTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomersTable.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
            return View(await _context.CustomersTable.ToListAsync());
        }

        // GET: Orders/CustomerTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTable = await _context.CustomersTable
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerTable == null)
            {
                return NotFound();
            }

            return View(customerTable);
        }

        // GET: Orders/CustomerTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/CustomerTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,UserName,FistName,LastName,CustomerAddress,CustomerContactNumber")] CustomerTable customerTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerTable);
        }

        // GET: Orders/CustomerTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTable = await _context.CustomersTable.FindAsync(id);
            if (customerTable == null)
            {
                return NotFound();
            }
            return View(customerTable);
        }

        // POST: Orders/CustomerTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,UserName,FistName,LastName,CustomerAddress,CustomerContactNumber")] CustomerTable customerTable)
        {
            if (id != customerTable.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTableExists(customerTable.CustomerId))
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
            return View(customerTable);
        }

        // GET: Orders/CustomerTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTable = await _context.CustomersTable
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerTable == null)
            {
                return NotFound();
            }

            return View(customerTable);
        }

        // POST: Orders/CustomerTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerTable = await _context.CustomersTable.FindAsync(id);
            _context.CustomersTable.Remove(customerTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerTableExists(int id)
        {
            return _context.CustomersTable.Any(e => e.CustomerId == id);
        }
    }
}
