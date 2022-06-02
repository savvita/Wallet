using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Wallet.Model;

namespace Wallet.ViewModel
{
    public class WalletKeeperViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ITransaction> _transactions;
        private FileController _controller;

        private decimal _amount;

        public decimal Amount
        {
            get => _amount;

            private set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public ObservableCollection<ITransaction> Transactions
        {
            get => _transactions;

            set
            {
                _transactions = value;
                OnPropertyChanged(nameof(Transactions));
            }
        }

        public WalletKeeperViewModel()
        {
            _controller = new FileController();

            if(_controller.Transactions != null)
                Transactions = new ObservableCollection<ITransaction>( _controller.Transactions);

            Amount = SetAmount();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private decimal SetAmount()
        {
            decimal amount = 0;

            if (_controller.Transactions != null)
            {
                foreach (ITransaction transaction in _controller.Transactions)
                {
                    if (transaction is IncomeTransactionModel)
                        amount += transaction.Sum;
                    else
                        amount -= transaction.Sum;
                }
            }

            return amount;
        }

        #region Commands
        private readonly RelayCommand showIncomesCommand;
        private readonly RelayCommand<object> selectCategoryCommand;
        private readonly RelayCommand<object> addOutgoTransactionCommand;
        private readonly RelayCommand<object> addIncomeTransactionCommand;
        private readonly RelayCommand? showAllCommand;
        private readonly RelayCommand? closeCommand;

        public RelayCommand ShowIncomesCommand
        {
            get
            {
                return showIncomesCommand ?? new RelayCommand(
                    () => Transactions = new ObservableCollection<ITransaction>(_controller.Transactions.
                    Where(item => item is IncomeTransactionModel)));
            }
        }

        public RelayCommand ShowAllCommand
        {
            get
            {
                return showAllCommand ?? new RelayCommand(
                    () => Transactions = new ObservableCollection<ITransaction>(_controller.Transactions.OrderByDescending(item => item.Date)));
            }
        }

        public RelayCommand<object> SelectCategoryCommand
        {
            get
            {
                return selectCategoryCommand ?? new RelayCommand<object>(
                    (obj) => Transactions = new ObservableCollection<ITransaction>(_controller.Transactions.OrderByDescending(item => item.Date).
                    Where(item => item is OutgoTransactionModel).
                    Where(item => (item as OutgoTransactionModel).Categorie.Equals(obj as string))));
            }
        }

        public RelayCommand<object> AddOutgoTransactionCommand
        {
            get
            {
                return addOutgoTransactionCommand ?? new RelayCommand<object>(
                    (obj) =>
                    {
                        try
                        {
                            string[] values = ((string)obj).Split("||");
                            decimal sum = Decimal.Parse(values[0]);

                            if (sum > 0)
                            {
                                OutgoTransactionModel transaction = new OutgoTransactionModel()
                                {
                                    Sum = sum,
                                    Categorie = values[1],
                                    Date = DateTime.Now
                                };

                                _controller.Transactions.Add(transaction);
                                Transactions.Add(transaction);
                                Amount = SetAmount();
                            }
                        }
                        catch { }
                    });
            }
        }

        public RelayCommand<object> AddIncomeTransactionCommand
        {
            get
            {
                return addIncomeTransactionCommand ?? new RelayCommand<object>(
                    (obj) =>
                    {
                        try
                        {
                            decimal sum = Decimal.Parse(obj as string);

                            if (sum > 0)
                            {
                                IncomeTransactionModel transaction = new IncomeTransactionModel()
                                {
                                    Sum = sum,
                                    Date = DateTime.Now
                                };

                                _controller.Transactions.Add(transaction);
                                Transactions.Add(transaction);
                                Amount = SetAmount();
                            }
                        }
                        catch { }
                    });
            }
        }

        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ?? new RelayCommand(() =>
                {
                    _controller.SaveToFile();
                });
            }
        } 
        #endregion
    }
}
