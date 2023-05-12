using ExampleRazorPizzariaProject.Data;
using ExampleRazorPizzariaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleRazorPizzariaProject.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {

        [BindProperty]
        public IEnumerable<ToppingModel> AvailableToppings { get; set; }

        [BindProperty]
        public double BasePricePizza { get { return 5.00; } }

        private readonly IDataRepository _repository;

        public CustomPizzaModel(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            var query = "select * from topping order by Name";
            AvailableToppings = await _repository.ListAsync<ToppingModel>(query);
        }
    }
}
