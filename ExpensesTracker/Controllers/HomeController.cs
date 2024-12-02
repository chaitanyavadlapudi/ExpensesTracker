using ExpensesTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpensesTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  readonly ExpenseTrackerDbcontext _dbcontext;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbcontext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Expenses()
        {
            var allExpenses = _dbcontext.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(x =>x.Value);
            ViewBag.Expenses = totalExpenses;
            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? Id)
        {
            if (Id != null)
            {
                var expenseInDb = _dbcontext.Expenses.SingleOrDefault(expense => expense.Id == Id);
                return View(expenseInDb);

            }

            return View();
        }public IActionResult DeleteExpense(int Id)
        {
            var expenseInDb = _dbcontext.Expenses.SingleOrDefault(expense => expense.Id == Id);
            _dbcontext.Expenses.Remove(expenseInDb);
            _dbcontext.SaveChanges();

            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                _dbcontext.Expenses.Add(model);
            }
            else
            {
                _dbcontext.Expenses.Update(model);
            }
            _dbcontext.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
