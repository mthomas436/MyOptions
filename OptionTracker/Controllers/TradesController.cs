using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OptionTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class TradesController : Controller
    {
        private readonly ITradeRepository _repo;
 

        public TradesController(ITradeRepository repo)
        {
            _repo = repo;
 
        }


        // GET: Trades
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tradeTypes = await _repo.GetTradeTypes();
            var trades = await _repo.GetTrades(userId);

 
            TradeListVm vm = new TradeListVm
            {
               Trades = trades,
               TradeTypes = tradeTypes
               
            };
 
            return View(vm);
        }

        // GET: Trades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _repo.GetTrade(id.Value);
            if (trade == null)
            {
                return NotFound();
            }

            var options = await _repo.GetOptionDetail(id.Value);

            CreateTradeVM vm = new CreateTradeVM
            {
                Trade = trade,
                Options = options
            };

            return View(vm);
        }

        private async Task<List<SelectListItem> > getTradeTypes()
        {
            var ttypes = await _repo.GetTradeTypes();
            var ttypeList = new List<SelectListItem>();

            foreach (var item in ttypes)
            {
                ttypeList.Add(new SelectListItem { Text = item.Description, Value = item.TradeTypeId.ToString() });
            }

            return ttypeList;
        }

        // GET: Trades/Create
        public async Task<IActionResult> Create()
        {
            var ttypeList = await getTradeTypes();

            CreateTradeVM vm = new CreateTradeVM
            {

                Trade = new Trade(),
                TradeTypes = ttypeList
            };
            return View(vm);
        }

        // POST: Trades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tradeid,Description,Notes,StockSymbol,TradeTypeId")] Trade trade)
        {
            if (ModelState.IsValid)
            {
                trade.Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                trade.DateEntered = DateTime.Now;
                trade.StockSymbol = trade.StockSymbol.ToUpper();
                await _repo.AddTrade(trade);
                return RedirectToAction(nameof(Index));
            }
            return View(trade);
        }

        // GET: Trades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _repo.GetTrade(id.Value);
            if (trade == null)
            {
                return NotFound();
            }

            var ttypeList = await getTradeTypes();

            CreateTradeVM vm = new CreateTradeVM
            {

                Trade = trade,
                TradeTypes = ttypeList
            };

            return View(vm);
        }

        // POST: Trades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tradeid,Description,Notes,StockSymbol,TradeTypeId")] Trade trade)
        {
            if (id != trade.Tradeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tradeFromDb = await _repo.GetTrade(trade.Tradeid);
                    tradeFromDb.Description = trade.Description;
                    tradeFromDb.Notes = trade.Notes;
                    tradeFromDb.StockSymbol = trade.StockSymbol.ToUpper();
                    tradeFromDb.TradeTypeId = trade.TradeTypeId;

                    if (trade.DateClosed != null)
                        tradeFromDb.DateClosed = DateTime.Now;

                    await _repo.UpdateTrade(tradeFromDb);
 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradeExists(trade.Tradeid))
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
            return View(trade);
        }

        // GET: Trades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _repo.GetTrade(id.Value);
            if (trade == null)
            {
                return NotFound();
            }

            return View(trade);
        }

        // POST: Trades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trade = await _repo.DeleteTrade(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TradeExists(int id)
        {
            var trade =  _repo.GetTrade(id);
            if(trade == null)
                return false;
            return true;
        }
    }
}
