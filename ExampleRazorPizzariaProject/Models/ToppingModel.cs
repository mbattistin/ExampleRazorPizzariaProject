namespace ExampleRazorPizzariaProject.Models
{
    public class ToppingModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public double ToppingAditionalPrice { get; set; }
    }
}
