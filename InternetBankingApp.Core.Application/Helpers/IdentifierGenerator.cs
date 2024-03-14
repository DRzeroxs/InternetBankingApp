using InternetBankingApp.Core.Application.Interfaces.IServices;
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

        public static int GenerateCode(List<int> generatedCodes)
        {
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
