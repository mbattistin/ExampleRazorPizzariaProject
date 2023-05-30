using ExampleRazorPizzariaProject.Data;
using ExampleRazorPizzariaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleRazorPizzariaProject.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {

        [BindProperty]
        public List<ToppingModel> AvailableToppings { get; set; }

        [BindProperty]
        public double BasePricePizza { get { return 5.00; } }

		[BindProperty]
        public OrderModel Order { get; set; }

        private readonly IDataRepository _repository;

        public CustomPizzaModel(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var query = "select * from topping order by Name";
                AvailableToppings = await _repository.ListAsync<ToppingModel>(query);
                return null;
            }
            catch
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var selectedToppings = AvailableToppings.Where(t => t.IsSelected == true);
                var insertOrderquery = $"insert into pizzas.order (IdPizza, CustomerName, Address, TotalPrice) values (null, '{Order.CustomerName}', '{Order.Address}', " +
                    $"{BasePricePizza + selectedToppings.Sum(t => t.ToppingAditionalPrice)}); " +
                    $"SELECT LAST_INSERT_ID();";

                var order = await _repository.SaveAsync<ulong>(insertOrderquery);

                foreach (var topping in selectedToppings)
                {
                    var insertOrderTopping = $"insert into pizzas.order_topping (IdTopping, IdOrder) values ({topping.Id}, {order});SELECT LAST_INSERT_ID();";
                    await _repository.SaveAsync<ulong>(insertOrderTopping);
                }

                return RedirectToPage("/Checkout/Checkout", new { Order = order });
            }
            catch
            {
                return RedirectToPage("/Error");
            }

        }
    }
}
