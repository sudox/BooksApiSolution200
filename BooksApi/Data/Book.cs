﻿namespace BooksApi.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public bool RemovedFromInventory { get; set; }
       
    }
}
