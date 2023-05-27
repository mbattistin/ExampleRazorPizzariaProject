namespace ExampleRazorPizzariaProject.Models
{
	public class OrderModel
	{
        public int Id { get; set; }
        public int? IdPizza { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public double TotalPrice { get; set; }

    }
}
