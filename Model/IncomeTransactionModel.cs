using System;

namespace Wallet.Model
{
    public class IncomeTransactionModel : ITransaction
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }

        public string DisplaySum
        {
            get
            {
                return $"{Sum:#.##}₴";
            }
        }

        public string DisplayDate
        {
            get
            {
                return Date.ToString("F");
            }
        }
    }

}
