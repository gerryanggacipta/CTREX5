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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TREX.Utilities;
using TREX.Common;
using System.ComponentModel;
using TREX.Entities;

namespace TREX
{
    /// <summary>
    /// Interaction logic for StrategyView.xaml
    /// </summary>
    /// 
    public partial class StrategyView : UserControl
    {
        private StrategyViewModel _strategyViewModel;
        private decimal _stockVol;
        private int _portfolioAvailableSize;
        private bool _isBuy;
        private bool _isLong;
        private SystemConstants.StrategyType _strategyType;

        // For sorting the GridView columns
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        public StrategyView()
        {
            InitializeComponent();
            _strategyViewModel = new StrategyViewModel();
            DataContext = _strategyViewModel;

            //Initialize default settings for stock chart
            TimespanBtn1D.Focus();
            _strategyViewModel.stockString = (_strategyViewModel.SelectedStock == null ? "" : _strategyViewModel.SelectedStock.Symbol);
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Day_1d;
            _strategyViewModel.chartType = YahooChart.ChartType.Candle_c;
            _strategyViewModel.movingAvgIntervalList = new List<YahooChart.MovingAverageIndicator>();

            // Initialize StockComboBox
            StockComboBox.ItemsSource = CompanyListManager.SymbolList;
            StockComboBox.SelectedIndex = 0;

            // Initialize ShortCheckBox value
            _isLong = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int strategyId = _strategyViewModel.SelectedStrategy == null ? -1 : _strategyViewModel.SelectedStrategy.Id;
            _strategyViewModel.saveStratConfig(strategyId, _strategyType.ToString(), ((Strategy)StrategyConfigListView.SelectedItem).Activated, StockComboBox.SelectedValue.ToString(), (bool)BuyRadio.IsChecked, (bool)ShortCheckBox.IsChecked, ConfigNameTextBox.Text, SizeTextBox.Text);
        }

        private void StrategyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)StrategyComboBox.SelectedItem).Content == null) return;

            switch (((ComboBoxItem)StrategyComboBox.SelectedItem).Content.ToString())
            {
                case "Two Moving Averages":
                    _strategyType = SystemConstants.StrategyType.MOVING_AVERAGE;
                    break;
                case "Bollinger Band":
                    _strategyType = SystemConstants.StrategyType.BOLLINGER_BAND;
                    break;
                default:
                    break;
            }
        }

        private void StockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StockComboBox == null) return;

            if (StockComboBox.SelectedValue.ToString() == null)
            {
                ExchangeVolLabel.Content = "Exchange Volume: -";
                AvailableSizeLabel.Content = "Available Size: -";            
            }

            // Set selected stock and get stock chart
            _strategyViewModel.SelectedStock = _strategyViewModel.getStock(StockComboBox.SelectedValue.ToString());
            _strategyViewModel.getStockChart(null);

            // Query MarketDataManager for stock vol
            _stockVol = _strategyViewModel.SelectedStock.Vol;
            ExchangeVolLabel.Content = "Exchange Volume: " + _stockVol;

            // Query portfoliomanager for available size
            _portfolioAvailableSize = _strategyViewModel.getPortfolioAvailableSize(StockComboBox.SelectedValue.ToString());
            //_portfolioAvailableSize = 0;
            AvailableSizeLabel.Content = "Available Size: " + _portfolioAvailableSize;            
        }

        private void BuyRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isBuy = true;

            if (!validateSizeTextBox()) return;

            //  If buy is selected, compare with exchange volume
            if (!validateStockVol()) return;

            setSizeTextBoxValid();
        }

        private void SellRadio_Checked(object sender, RoutedEventArgs e)
        {
            _isBuy = false;

            if (!validateSizeTextBox()) return;

            //  If sell is selected, compare with allowable size
            if (!validatePortfolioAvailableSize()) return;

            setSizeTextBoxValid();
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 num;

            if (!validateSizeTextBox()) return;

            if (!Int64.TryParse(SizeTextBox.Text, out num))
            {
                ErrorLabel.Content = "Only digits are allowed";
                SaveButton.IsEnabled = false;
                return;
            }

            if (Int64.Parse(SizeTextBox.Text) == 0)
            {
                ErrorLabel.Content = "Size must be more than 0";
                SaveButton.IsEnabled = false;
                return;
            }

            if (_isBuy)
            {
                //  If buy is selected, compare with exchange volume
                if (!validateStockVol()) return;
            }
            else
            {
                //  If sell is selected, compare with allowable size
                if (!validatePortfolioAvailableSize()) return;
            }

            setSizeTextBoxValid();
        }

        #region Validation Methods for SizeTextBox
        private bool validateSizeTextBox()
        {
            if (SizeTextBox == null || ErrorLabel == null) return false;

            if (SizeTextBox.Text.Length == 0)
            {
                ErrorLabel.Content = "";
                SaveButton.IsEnabled = false;
                return false;
            }
            return true;
        }

        private bool validateStockVol()
        {
            if (Int64.Parse(SizeTextBox.Text) > _stockVol)
            {
                ErrorLabel.Content = "Exceeded exchange volume";
                SaveButton.IsEnabled = false;
                return false;
            }
            return true;
        }
        private bool validatePortfolioAvailableSize()
        {
            if (Int64.Parse(SizeTextBox.Text) > _portfolioAvailableSize)
            {
                ErrorLabel.Content = "Exceeded available size";
                SaveButton.IsEnabled = false;
                return false;
            }
            return true;
        }

        private void setSizeTextBoxValid()
        {
            ErrorLabel.Content = "";
            SaveButton.IsEnabled = true;
        }
        #endregion

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string strategyId = _strategyType + "_" + ConfigNameTextBox.Text;

            StringBuilder message = new StringBuilder();
            StringBuilder sentMessage = new StringBuilder();
            StringBuilder unsentMessage = new StringBuilder();
            sentMessage.Append("Strategies executed: \n");
            unsentMessage.Append("Strategies not executed: \n");

            foreach (var strategyConfig in _strategyViewModel.StrategyList)
            {
                if (strategyConfig.Activated)
                {
                    Stock stock = _strategyViewModel.getStock(strategyConfig.Stock);
                    decimal price = strategyConfig.Buy ? stock.Ask : stock.Bid;

                    if (price != 0)
                    {
                        _strategyViewModel.addTrade(strategyConfig.Id.ToString(), strategyConfig.Stock, price, strategyConfig.Size, strategyConfig.Buy, !strategyConfig.Short, DateTime.Now.ToString());
                        sentMessage.Append(strategyConfig.Name + "\n");
                    }
                    else
                    {
                        unsentMessage.Append(strategyConfig.Name + "\n");
                    }
                    
                }
            }
            if (sentMessage.Length > 1)
                message.Append(sentMessage);
            if (unsentMessage.Length > 1)
                message.Append(unsentMessage);
            
            MessageBox.Show(message.ToString(), "Confirmation");
        }

        private void ShortCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ShortCheckBox.IsChecked == true)
                _isLong = false;
            else
                _isLong = true;
        }

        // Sort the GridView Column when the column header is clicked
        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                StrategyConfigListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            StrategyConfigListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private List<Strategy> lstMyObject = new List<Strategy>();

        private void StrategyConfigListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _strategyViewModel.SelectedStrategy = (Strategy)StrategyConfigListView.SelectedItem;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset configuration fields
            ConfigNameTextBox.Text = "";
            StrategyComboBox.SelectedIndex = 0;
            StockComboBox.SelectedIndex = 0;
            BuyRadio.IsChecked = true;
            ShortCheckBox.IsChecked = false;
            SizeTextBox.Text = "";
            SaveButton.IsEnabled = false;

            // Set the selected strategy to null
            _strategyViewModel.SelectedStrategy = null;
        }

        private void LineTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_strategyViewModel == null) return;

            switch (((ComboBoxItem)LineTypeComboBox.SelectedItem).Content.ToString())
            {
                case "Candle":
                    _strategyViewModel.chartType = YahooChart.ChartType.Candle_c;
                    break;
                case "Line":
                    _strategyViewModel.chartType = YahooChart.ChartType.Line_l;
                    break;
                case "Bar":
                    _strategyViewModel.chartType = YahooChart.ChartType.Bar_b;
                    break;
                default:
                    _strategyViewModel.chartType = YahooChart.ChartType.Candle_c;
                    break;
            }
        }

        #region MovingAverageIndicator checked
        private void MA5_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA5.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m5_5);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m5_5);
        }

        private void MA10_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA10.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m10_10);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m10_10);
        }

        private void MA20_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA20.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m20_20);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m20_20);
        }

        private void MA50_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA50.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m50_50);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m50_50);
        }

        private void MA100_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA100.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m100_100);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m100_100);
        }

        private void MA200_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA200.IsChecked) _strategyViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m200_200);
            else _strategyViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m200_200);
        }

        #endregion

        #region TimespanBtn click

        private void TimespanBtn1D_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Day_1d;
        }
        private void TimespanBtn5D_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c5Days_5d;
        }

        private void TimespanBtn3M_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c3Months_3m;
        }

        private void TimespanBtn6M_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c6Months_6m;
        }

        private void TimespanBtn1Y_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Year_1y;
        }

        private void TimespanBtn2Y_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c2Years_2y;
        }

        private void TimespanBtn5Y_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.c5Years_5y;
        }

        private void TimespanBtnMAX_Click(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.timeSpan = YahooChart.ChartTimeSpan.cMaximum_my;
        }

        #endregion

        #region Strategy type radiobutton events

        private void AllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_strategyViewModel != null) _strategyViewModel.getAllStratConfig();
        }

        private void MovingAvgRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.getStratConfigByType(SystemConstants.StrategyType.MOVING_AVERAGE.ToString());
        }

        private void BollingerBandRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _strategyViewModel.getStratConfigByType(SystemConstants.StrategyType.BOLLINGER_BAND.ToString());
        }

        #endregion

        private void ActivatedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            // Set the selected checkedbox to be the selected item
            if (lvi != null)
                StrategyConfigListView.SelectedIndex = StrategyConfigListView.ItemContainerGenerator.IndexFromContainer(lvi);

            _strategyViewModel.saveStratConfig(((Strategy)StrategyConfigListView.SelectedItem).Id, ((Strategy)StrategyConfigListView.SelectedItem).Type.ToString(), ((Strategy)StrategyConfigListView.SelectedItem).Activated, StockComboBox.SelectedValue.ToString(), (bool)BuyRadio.IsChecked, (bool)ShortCheckBox.IsChecked, ConfigNameTextBox.Text, SizeTextBox.Text);
        }

        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {

            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);

        }

    }

    
}
