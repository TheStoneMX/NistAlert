using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioTechSysNistAlert.HelperCode
{
    class DateTimeHelper
    {

          static void FormatDateTime( ) 
          {
            DateTime dt = new DateTime(2000, 10, 11, 15, 32, 14);
            // Prints "2000-10-11T15:32:14"
            Console.WriteLine(dt.ToString( ));
            // Prints "Wednesday, October 11, 2000"
            Console.WriteLine("{0}", dt);
            // Prints "10/11/2000"
            Console.WriteLine("{0:d}", dt);
            // Prints "Wednesday, October 11, 2000"
            Console.WriteLine("{0:D}", dt);
            // Prints "Wednesday, October 11, 2000 3:32 PM"
            Console.WriteLine("{0:f}", dt);
            // Prints "Wednesday, October 11, 2000 3:32:14 PM"
            Console.WriteLine("{0:F}", dt);
            // Prints "10/11/2000 3:32 PM"
            Console.WriteLine("{0:g}", dt);
            // Prints "10/11/2000 3:32:14 PM"
            Console.WriteLine("{0:G}", dt);
            // Prints "October 11"
            Console.WriteLine("{0:m}", dt);
            // Prints "October 11"
            Console.WriteLine("{0:M}", dt);
            // Prints "Wed, 11 Oct 2000 22:32:14 GMT"
            Console.WriteLine("{0:r}", dt);
            // Prints "Wed, 11 Oct 2000 22:32:14 GMT"
            Console.WriteLine("{0:R}", dt);
            // Prints "3:32 PM"
            Console.WriteLine("{0:t}", dt);
            // Prints "3:32:14 PM"
            Console.WriteLine("{0:T}", dt);
            // Prints "2000-10-11 22:32:14Z"
            Console.WriteLine("{0:u}", dt);
            // Prints "Wednesday, October 11, 2000 10:32:14 PM"
            Console.WriteLine("{0:U}", dt);
            // Prints "October, 2000"
            Console.WriteLine("{0:y}", dt);
            // Prints "October, 2000"
            Console.WriteLine("{0:Y}", dt);
            // Prints "Wednesday the 11 day of October in the year 2000"
            Console.WriteLine(
              "{0:dddd 'the' d 'day of' MMMM 'in the year' yyyy}", dt);
          }

    }
}
