﻿namespace BookApi.Models
{
    public class BookDetails
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string Author { get; set; }
        public string CategoryId { get; set; }
    }
}
