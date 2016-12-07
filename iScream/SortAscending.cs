using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class SortAscending : ISort
    {
        Container sorted;
        Container sort(Container unsorted)
        {
            sorted = unsorted;
            sorted.Games.Sort((x, y) => x.Name.CompareTo(y.Name));
            sorted.Users.Sort((x, y) => x.Name.CompareTo(y.Name));

            return unsorted;
        }
    }
}
