using ExampleRazorPizzariaProject.Data;
using ExampleRazorPizzariaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleRazorPizzariaProject.Pages.Checkout
{
    public class CheckoutModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "Order")]
        public int OrderNumber { get; set; }

        [BindProperty]
        public double BasePricePizza { get { return 5.00; } }

        [BindProperty]
        public List<ToppingModel> SelectedToppings { get; set; }

        [BindProperty]
        public OrderModel Order { get; set; }

        [BindProperty]
        public PizzaModel Pizza { get; set; }

        private readonly IDataRepository _repository;

        public CheckoutModel(IDataRepository repository)
        {
            _repository = repository;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (OrderNumber <= 0)
                    return RedirectToPage("/Error");

                Order = await _repository.GetAsync<OrderModel>($"select * from pizzas.order where Id = {OrderNumber}");

                if (Order.IdPizza != null)
                {
                    Pizza = await _repository.GetAsync<PizzaModel>($"select * from pizzas.pizza where Id = {Order.IdPizza}");
                }

                SelectedToppings = await _repository.ListAsync<ToppingModel>("SELECT t.* FROM pizzas.order_topping as ot " +
                    " join pizzas.topping as t on ot.IdTopping = t.Id " +
                    $" where ot.IdOrder = {Order.Id}");
                return null;
            }
            catch 
            {
                return RedirectToPage("/Error");
            }


        }
    }
}
