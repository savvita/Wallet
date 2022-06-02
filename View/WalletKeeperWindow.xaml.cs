using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

using Wallet.ViewModel;

namespace Wallet.View
{
    /// <summary>
    /// Interaction logic for WalletKeeperWindow.xaml
    /// </summary>
    public partial class WalletKeeperWindow : Window
    {
        public WalletKeeperWindow()
        {
            InitializeComponent();
            DataContext = new WalletKeeperViewModel();
        }

        private void TransactionsCategory_MouseClick(object sender, RoutedEventArgs e)
        {
            this.Transactions.Visibility = Visibility.Visible;
            this.TransactionsCategory.Header = (sender as Button).Tag.ToString();
        }

        private void Coin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Control ctrl = (Control)sender;
            DragDrop.DoDragDrop(ctrl, ctrl.Name, DragDropEffects.Copy);
        }

        private void Coin_Drop(object sender, DragEventArgs e)
        {
            this.AddOutgoTransaction.Visibility= Visibility.Visible;
            
            this.Categorie.Header = (sender as Button).Tag.ToString();
            this.SumOutgo.Text = string.Empty;
        }

        private void CoinIncome_Drop(object sender, DragEventArgs e)
        {
            this.AddIncomeTransaction.Visibility = Visibility.Visible;

            this.SumIncome.Text = string.Empty;
        }

        private void BackFromAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            this.AddOutgoTransaction.Visibility = Visibility.Hidden;
        }

        private void BackFromAddIncome_Click(object sender, RoutedEventArgs e)
        {
            this.AddIncomeTransaction.Visibility = Visibility.Hidden;
        }

        private void BackFromTransactions_Click(object sender, RoutedEventArgs e)
        {
            this.Transactions.Visibility = Visibility.Hidden;
        }
    }
}
