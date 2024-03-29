﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColmanAppStore.Data;
using ColmanAppStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace ColmanAppStore.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ColmanAppStoreContext _context;

        public PaymentsController(ColmanAppStoreContext context)
        {
            _context = context;
        }

        // GET: Payments
        [HttpGet]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Index()
        {
            String userName = User.Identity.Name;
            var usr = _context.User.Include(a => a.AppListUser).Include(p => p.PaymentMethods);
            foreach (var item in usr)
            {
                if (item.Name.Equals(userName))
                {
                    var payments = _context.Payment.Include(p => p.App).Include(p => p.PaymentMethod).
                        Where(a => item.AppListUser.Contains(a.App)).Where(p => item.PaymentMethods.Contains(p.PaymentMethod));
                    return View(await payments.ToListAsync());
                }
            }
            //in case user won't be found (never going to happen when logged in)
            return View(await _context.Payment.Include(p => p.App).Include(p => p.PaymentMethod).ToListAsync());
        }

        // GET: Payments/Details/5
        [HttpGet]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var payment = await _context.Payment.Include(p => p.App).Include(p => p.PaymentMethod).FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            int count = 0;
            string userName = User.Identity.Name;
            int methodId = payment.PaymentMethodId;
            var payMethod = _context.PaymentMethod.Include(u => u.Users).FirstOrDefault(m => m.Id == methodId);
            var usr = payMethod.Users;
            User myUsr = _context.User.Where(i => i.Name.Equals(userName)).First();
            int myId = myUsr.Id;
            foreach (var item in usr)
            {
                if (item.Id.Equals(myId))
                {
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            return View(payment);
        }

        // GET: Payments/Create
        [HttpGet]
        [Authorize(Roles = "Client,Admin,Programer")]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            ViewData["AppId"] = id;
            var application = new App();
            int count = 0;
            foreach (var item in _context.Apps)
            {
                if (item.Id == id)
                {
                    count++;
                    ViewData["App"] = item;
                    application = item;
                    break;
                }
            }
            if (count == 0) //there is no such app
            {
                return RedirectToAction("NotFound", "Home");
            }

            String userName = User.Identity.Name;
            User connectedUser = null;
            foreach (var item in _context.User)
            {
                if (item.Name.Equals(userName))
                {
                    connectedUser = item;
                    break;
                }
            }

            var usr = _context.User.Include(u => u.PaymentMethods).Include(u => u.AppListUser);

            List<PaymentMethod> paymentM = new List<PaymentMethod>();
            foreach (var item in usr)
            {

                if (item.Equals(connectedUser))
                {
                    foreach (var us in item.PaymentMethods)
                    {
                        if (item.AppListUser.Contains(application)) // checks if the user already purchased the app
                            return RedirectToAction("details", "Apps", new { id = id });
                        paymentM.Add(us);
                    }
                    break;
                }
            }

            ViewData["PaymentMethodId"] = new SelectList(paymentM, "Id", "CardNumber");

            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,PaymentMethodId,AppId")] Payment payment, string userName)
        {
            if (ModelState.IsValid)
            {
                App purchasedApp = null;
                foreach (var item in _context.Apps)
                {
                    if (item.Id == payment.AppId)
                    {
                        purchasedApp = item;
                        break;
                    }
                }

                var usr = _context.User.Include(u => u.PaymentMethods).Include(u => u.AppListUser);
                foreach (var item in usr)
                {
                    if (item.Name.Equals(userName))
                    {
                        if (item.AppListUser == null)
                            item.AppListUser = new List<App>();
                        item.AppListUser.Add(purchasedApp);
                        _context.Update(item);
                        break;
                    }
                }

                payment.Id = 0; //be updated after added to DB
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("HomePage", "Apps");
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", payment.AppId);

            return View(payment);
        }


        // GET: Payments/Edit/5
        [HttpGet]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", payment.AppId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "ExpiredDate", payment.PaymentMethodId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,PaymentMethodId,AppId")] Payment payment)
        {
            if (id != payment.Id)
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return RedirectToAction("NotFound", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AppId"] = new SelectList(_context.Apps, "Id", "Name", payment.AppId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "ExpiredDate", payment.PaymentMethodId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        [HttpGet]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var payment = await _context.Payment.Include(p => p.App).Include(p => p.PaymentMethod).FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);

            App purchasedApp = null;
            foreach (var item in _context.Apps)
            {
                if (item.Id == payment.AppId)
                {
                    purchasedApp = item;
                    break;
                }
            }

            String userName = User.Identity.Name;
            var usr = _context.User.Include(u => u.PaymentMethods).Include(u => u.AppListUser);
            foreach (var item in usr)
            {
                if (item.Name.Equals(userName))
                {
                    item.AppListUser.Remove(purchasedApp);
                    _context.Update(item);
                    break;
                }
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Client,Admin,Programer")]
        public async Task<IActionResult> SearchPayment(string query) //search by id
        {
            String userName = User.Identity.Name;
            var usr = _context.User.Include(a => a.AppListUser).Include(p => p.PaymentMethods);
            foreach (var item in usr)
            {
                if (item.Name.Equals(userName))
                {
                    var payments = _context.Payment.Include(p => p.App).Include(p => p.PaymentMethod).
                        Where(a => item.AppListUser.Contains(a.App)).Where(p => item.PaymentMethods.Contains(p.PaymentMethod))
                        .Where(x => x.App.Name.Contains(query));
                    return View("SearchPayment", await payments.ToListAsync());
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AppBuyers(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Join model = new Join();

            // Init:
            model.Users = null;

            var buyers = from p in _context.Payment.Include(r => r.App).Include(r => r.PaymentMethod)
                         join app in _context.Apps on p.AppId equals app.Id
                         where id == p.AppId
                         select p;

            if (buyers == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            model.Users = buyers.ToList();

            return View(model);
        }

        //Function for map
        public JsonResult GetCitiesList()
        {
            List<String> citiesLst = new List<string>();
            foreach (var item in _context.Payment)
            {
                if (!citiesLst.Contains(item.City))
                    citiesLst.Add(item.City);
            }
            return Json(citiesLst);
        }
    }
}
