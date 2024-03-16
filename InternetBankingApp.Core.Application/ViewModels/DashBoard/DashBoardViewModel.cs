using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.DashBoard
{
    public class DashBoardViewModel
    {
        public int ActiveUsers {  get; set; }
        public int InactiveUsers { get; set; }
        public double PayToday { get; set; }
        public double PayInitial { get; set; }
        public int TransactionsToday { get; set; }
        public int TransactionsInitial { get; set; }
        public List<int> ProductsCount { get; set; }
    }
}
