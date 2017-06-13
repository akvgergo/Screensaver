using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screensaver {
    public static class Util {

        public static T RandIndex<T>(this IEnumerable<T> stuff, Random rnd) {
            return stuff.ElementAt(rnd.Next(stuff.Count()));
        }

    }

}
