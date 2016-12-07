using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Sorter
    {
        private ISort strategy;
        private bool sortDescending
        {
            get
            {
                return sortDescending;
            }
            set
            {
                this.sortDescending = value;
            }
        }

        Container sort(Container unsorted)
        {

            return strategy.sort(unsorted);
                
        }

        public void SetSortAlgorithm(ISort strategy)
        {

            this.strategy = strategy;

        }
    }
}
