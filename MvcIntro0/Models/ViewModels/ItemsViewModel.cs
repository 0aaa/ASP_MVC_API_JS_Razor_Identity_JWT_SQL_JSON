using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcIntro0.Models.ViewModels
{
    public class ItemsViewModel
    {
        public SelectList Line { get; set; }
        public SelectList Model { get; set; }
        public SelectList Frame { get; set; }
        public SelectList Fork { get; set; }
        public SelectList Shifter { get; set; }
        public SelectList Brake { get; set; }
        public SelectList Cost { get; set; }

        public List<Bike> Items { get; set; }

		public int ItemPagesTotalAmount { get; set; }
		public int ItemsCurrentPage { get; set; }


		public ItemsViewModel()
		{
            Items = new List<Bike>();
            ItemsCurrentPage = 1;
		}
    }
}
