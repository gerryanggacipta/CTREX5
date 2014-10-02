using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TREX.Entities;

namespace TREX
{
    /// <summary>
    /// Interaction logic for ManualTradeView.xaml
    /// </summary>
    public partial class HighTouchTradeView : Window
    {
        private HighTouchTradeViewModel _highTouchTradeViewModel;
        private static bool _isBuy;
        private static bool _isLong;
        private Stock _selectedStock;
        public HighTouchTradeView(MarketDataViewModel marketDataViewModel, bool isBuy, Stock selectedStock)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _selectedStock = selectedStock;
            _highTouchTradeViewModel = new HighTouchTradeViewModel(isBuy, _selectedStock);
            DataContext = _highTouchTradeViewModel;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            _highTouchTradeViewModel.addTrade(_highTouchTradeViewModel.SelectedStock.Symbol, PriceTextBox.Text, SizeTextBox.Text, _isBuy, _isLong, DateTime.Now.ToString());

            MessageBox.Show(this, "Trade has been made!");

            this.Close();
        }

        private void BuyRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isBuy = true;
        }
        private void SellRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isBuy = false;
        }
        private void LongRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isLong = true;
        }

        private void ShortRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isLong = false;
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int num;
                if (SizeTextBox.Text.Length != 0 && !int.TryParse(SizeTextBox.Text, out num))
                {
                    ErrorLabel.Content = "Only digits are allowed";
                    ConfirmBtn.IsEnabled = false;
                    return;
                }

                if (Convert.ToInt32(SizeTextBox.Text) > _selectedStock.Vol)
                {
                    ErrorLabel.Content = "Maximum allowable volume: " + _selectedStock.Vol;
                    ConfirmBtn.IsEnabled = false;
                }
                else
                {
                    ErrorLabel.Content = "";
                    ConfirmBtn.IsEnabled = true;
                }
            }
            catch (Exception ex) {
                
            } 

        }

        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double num;
            if (PriceTextBox.Text.Length != 0 && !double.TryParse(PriceTextBox.Text, out num))
            {
                ErrorLabel.Content = "Only digits are allowed";
                ConfirmBtn.IsEnabled = false;
                return;
            }
        }

    }
}
