using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models
{
    public class Summary
    {
        private readonly List<ChosenItem> _summaryLst;

        public IEnumerable<ChosenItem> SummaryLst => _summaryLst;


        public Summary()
            => _summaryLst = new List<ChosenItem>();


        public void Addition(Bike bike, int qty)
        {
            ChosenItem item = _summaryLst.FirstOrDefault(item => item.Bike.BikeId == bike.BikeId);

            if (item == null)
            {
                _summaryLst.Add(new ChosenItem { Bike = bike, Qty = qty });
            }
            else
            {
                item.Qty += qty;
            }
        }


        public void Deletion(Bike bike)
            => _summaryLst.RemoveAll(item => item.Bike.BikeId == bike.BikeId);


        public void DeleteAll()
            => _summaryLst.Clear();


        public decimal ComputeTotal()
            => _summaryLst.Sum(item => item.Bike.Cost * item.Qty);
    }
}
