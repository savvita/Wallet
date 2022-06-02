using Wallet.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace Wallet.ViewModel
{
    public class FileController
    {
        private string walletFileName = "wallet.csv".FullPath();

        public List<ITransaction>? Transactions { get; private set; }

        public FileController()
        {
            LoadFromFile();
        }

        public void SaveToFile()
        {
            StringBuilder sb = new StringBuilder();

            if (Transactions != null)
            {

                foreach (ITransaction transaction in Transactions)
                {
                    sb.Append($"{transaction.GetType().Name}||");
                    sb.Append($"{transaction.Date.ToString()}||");
                    sb.Append($"{transaction.Sum}");

                    if(transaction is OutgoTransactionModel)
                    {
                        sb.Append($"||{(transaction as OutgoTransactionModel).Categorie}");
                    }

                    sb.AppendLine();
                }
                File.WriteAllText(walletFileName, sb.ToString());
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists(walletFileName.FullPath()))
            {
                try
                {
                    Transactions = walletFileName.LoadFromFile().ConvertToTransactions();
                }
                catch { }
            }
            else
            {
                Transactions = new List<ITransaction>();
            }
        }
    }
}
