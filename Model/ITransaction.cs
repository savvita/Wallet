using System;

namespace Wallet.Model
{
    public interface ITransaction
    {
        DateTime Date { get; set; }
        decimal Sum { get; set; }

        string DisplaySum { get; }
        string DisplayDate { get; }
    }

}
