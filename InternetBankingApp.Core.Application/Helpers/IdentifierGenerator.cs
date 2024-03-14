using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Helpers
{
    public static class IdentifierGenerator
    {
        private static Random random = new Random();

        //public static int GenerateCode(HashSet<int> generatedCodes)
        //{
        //    while (true)
        //    {
        //        int code = random.Next(100000000, 1000000000);

        //        if (!generatedCodes.Contains(code))
        //        {
        //            return code;
        //        }
        //    }
        //}

        public static int GenerateCode()
        {
            HashSet<int> generatedCodes = new HashSet<int>();

            while (true)
            {
                int code = random.Next(100000000, 1000000000);

                if (!generatedCodes.Contains(code))
                {
                    return code;
                }
            }
        }

        
    }
}
