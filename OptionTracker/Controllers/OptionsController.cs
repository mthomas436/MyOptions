using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Models.ViewModels;


namespace OptionTracker.Controllers
{
    public class OptionsController : Controller
    {
        private readonly IOptionsRepository _repo;
        public OptionsController(IOptionsRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

 
 


        // GET: Options/Create/1

        public async Task<IActionResult> Create(int? id)
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

            var optiontypes = await getOptionTypes();

            CreateTradeVM vm = new CreateTradeVM
            {
                Trade = trade,
                OptionTypes = optiontypes,
                Option = new Option()
            };
            vm.Option.Tradeid = trade.Tradeid;
            return View(vm);
        }

        // POST: Trades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OptionTypeId,StrikePrice,Tradeid,Optionid,ExpirationDate,Quantity,EntryPrice,StockPriceatPurchace,Commission,Notes")] Option option)
        {
            Trade trade = await _repo.GetTrade(option.Tradeid);
            if (ModelState.IsValid)
            {

                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (Userid != trade.Userid)
                    return NotFound();

                option.DateEntered = DateTime.Now;

                if (option.Commission == null)
                    option.Commission = 0;

                await _repo.AddOption(option);
                return RedirectToAction("Details", "Trades", new { @id=option.Tradeid});
            }
            return RedirectToAction("Details", "Trades", new { @id = option.Tradeid });
        }

        // GET: Options/Edit/1

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var option = await _repo.GetOption(id.Value);
            if (option == null)
            {
                return NotFound();
            }

  
            CreateTradeVM vm = new CreateTradeVM
            {
                OptionTypes = await getOptionTypes(),
                Option = option
            };
 
            return View(vm);
        }



        // POST: Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OptionTypeId,StrikePrice,Tradeid,Optionid,ExpirationDate,Quantity,EntryPrice,StockPriceatPurchace,Commission,Notes,ExitPrice")] Option option)
        {
            if (id != option.Optionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var optionfromDb = new Option();
                try
                {
                    optionfromDb = await _repo.GetOption(option.Optionid);
                    optionfromDb.StrikePrice = option.StrikePrice;
                    optionfromDb.ExpirationDate = option.ExpirationDate;
                    optionfromDb.Quantity = option.Quantity;
                    optionfromDb.EntryPrice = option.EntryPrice;
                    optionfromDb.StockPriceatPurchace = option.StockPriceatPurchace;
                    optionfromDb.Commission = option.Commission;
                    optionfromDb.Notes = option.Notes;

 

                    await _repo.UpdateOption(optionfromDb);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.Optionid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Trades", new { @id = optionfromDb.Tradeid });
            }
            return View(option);
        }



        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _repo.GetOption(id.Value);
            
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optiontodelete = await _repo.GetOption(id);
            var tradeid = optiontodelete.Tradeid;
            var status = await _repo.DeleteOption(optiontodelete);
            return RedirectToAction("Details", "Trades", new { @id = tradeid });
        }




        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _repo.GetOption(id.Value);
            if (option == null)
            {
                return NotFound();
            }

 
            return View(option);
        }


        private bool OptionExists(int id)
        {
            var option = _repo.GetOption(id);
            if (option == null)
                return false;
            return true;
        }

        private async Task<List<SelectListItem>> getOptionTypes()
        {
            var opttypes = await _repo.getOptionTypes();
            var optTypeList = new List<SelectListItem>();

            foreach (var item in opttypes)
            {
                optTypeList.Add(new SelectListItem { Text = item.Description, Value = item.OptionTypeId.ToString() });
            }

            return optTypeList;
        }





    }
}