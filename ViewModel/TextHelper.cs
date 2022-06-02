using Wallet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Wallet.ViewModel
{
    public static class TextHelper
    {
        public static string FullPath(this string fileName)
        {
            return Path.Combine(Settings.FilePath, fileName);
        }

        public static List<string> LoadFromFile(this string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new List<string>();
            }

            return File.ReadAllLines(fileName).ToList();
        }

        public static List<ITransaction> ConvertToTransactions(this List<string> lines)
        {
            List<ITransaction> transactions = new List<ITransaction>();

            foreach (string line in lines)
            {
                string[] cols = line.Split("||");

                ITransaction transaction;

                DateTime date = DateTime.Parse(cols[1]);
                decimal sum = Decimal.Parse(cols[2]);

                if (cols[0].Equals(typeof(IncomeTransactionModel).Name))
                {
                    transaction = new IncomeTransactionModel()
                    {
                        Date = date,
                        Sum = sum
                    };
                }
                else
                {
                    transaction = new OutgoTransactionModel()
                    {
                        Date = date,
                        Sum = sum,
                        Categorie = cols[3]
                    };
                }       

                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}
