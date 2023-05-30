using ExampleRazorPizzariaProject.Data;
using ExampleRazorPizzariaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleRazorPizzariaProject.Pages.Forms
{
    public class PizzaSelectionModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "Pizza")]
        public int PizzaId { get; set; }

        [BindProperty]
        public OrderModel Order { get; set; }

        [BindProperty]
        public List<ToppingModel> AvailableToppings { get; set; }

        [BindProperty]
        public PizzaModel Pizza { get; set; }

        public string PizzaToppings { get; set; }

        private IDataRepository _repository;

        public PizzaSelectionModel(IDataRepository repository)
        {
            _repository = repository;
            Order = new OrderModel();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (PizzaId <=0 )
                    return RedirectToPage("/Error");

                Pizza = await _repository.GetAsync<PizzaModel>($"select * from Pizza where Id={PizzaId}");
                var toppings = await _repository.ListAsync<ToppingModel>($"SELECT t.* FROM pizzas.pizza_topping as pt join pizzas.topping as t on pt.IdTopping = t.Id where pt.IdPizza = {PizzaId}");
                PizzaToppings = String.Join(", ", toppings.Select(t => t.Name));

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
                var insertOrderquery = $"insert into pizzas.order (IdPizza, CustomerName, Address, TotalPrice) values ({PizzaId}, '{Order.CustomerName}', '{Order.Address}', " +
                    $"{Pizza.Price + selectedToppings.Sum(t => t.ToppingAditionalPrice)}); " +
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
