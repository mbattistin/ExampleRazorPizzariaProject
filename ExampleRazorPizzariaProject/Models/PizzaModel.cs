﻿namespace ExampleRazorPizzariaProject.Models
{
    public class PizzaModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
    }
}
