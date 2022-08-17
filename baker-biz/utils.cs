using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using baker_biz.BakeryItems;

namespace baker_biz
{
    internal class utils
    {
        public static int GetValidatedPositiveInt()
        {
            string readLine = Console.ReadLine() ?? "";
            int readInt;

            while (!int.TryParse(readLine, out readInt) || readInt < 0)
            {
                Console.WriteLine("Amount must be zero or a positive whole number, please try again.");
                readLine = Console.ReadLine() ?? "";
            }

            return readInt;
        }

        public static int RequestAvailableQuantity(String request)
        {
            Console.WriteLine(request);
            return utils.GetValidatedPositiveInt();
        }
    }
}
