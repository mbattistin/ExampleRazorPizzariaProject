using ExampleRazorPizzariaProject.Data;
using ExampleRazorPizzariaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleRazorPizzariaProject.Pages
{
    public class PizzasModel : PageModel
    {
        [BindProperty]
        public List<PizzaModel> Pizzas { get; set; }

        private IDataRepository _repository;

        public PizzasModel(IDataRepository repository)
        {
            _repository = repository;
        }   

        public async Task OnGetAsync()
        {
            var query = "select * from pizza order by Name";
            Pizzas = await _repository.ListAsync<PizzaModel>(query);
        }
    }
}
