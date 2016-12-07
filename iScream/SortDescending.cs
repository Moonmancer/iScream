using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class SortDescending:ISort
    {
        Container sorted;
        Container sort(Container unsorted)
        {
            sorted = unsorted;
            sorted.Games.Sort((x, y) => y.Name.CompareTo(x.Name));
            sorted.Users.Sort((x, y) => y.Name.CompareTo(x.Name));

            return unsorted;
        }
    }
}
