using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TREX.Entities;
using TREX.Utilities;

namespace TREX
{
    /// <summary>
    /// Interaction logic for MarketDataView.xaml
    /// </summary>
    public partial class MarketDataView : UserControl
    {
        // For sorting the GridView columns
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private MarketDataViewModel _marketDataViewModel;
        public static string filterOption;
        public static string filterString;

        public MarketDataView()
        {
            InitializeComponent();         
            _marketDataViewModel = new MarketDataViewModel(MarketDataListView);
            DataContext = _marketDataViewModel;

            //Initialize default settings for stock chart
            TimespanBtn1D.Focus();
            _marketDataViewModel.stockString = (_marketDataViewModel.SelectedStock == null ? "" : _marketDataViewModel.SelectedStock.Symbol);
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Day_1d;
            _marketDataViewModel.chartType = YahooChart.ChartType.Candle_c;
            _marketDataViewModel.movingAvgIntervalList = new List<YahooChart.MovingAverageIndicator>();
        }

        // Sort the GridView Column when the column header is clicked
        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                MarketDataListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            MarketDataListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           filterOption = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
           filterString = FilterTextBox.Text;

          //if (MarketDataListView.SelectedItem == null) 
          //     StockSummaryDataGrid.Visibility = Visibility.Hidden;
          // else 
          //     StockSummaryDataGrid.Visibility = Visibility.Visible;
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)FilterComboBox.SelectedItem).Content == null) return;
            filterOption = ((ComboBoxItem)FilterComboBox.SelectedItem).Content.ToString();
            filterString = FilterTextBox.Text;

            //if (MarketDataListView.SelectedItem == null)
            //    StockSummaryDataGrid.Visibility = Visibility.Hidden;
            //else
            //    StockSummaryDataGrid.Visibility = Visibility.Visible;
        }
        private void MarketDataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSelectedLiveFeed();
        }
        private void WatchlistListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSelectedWatchlist();
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            var highTouchTradePopupView = new HighTouchTradeView(_marketDataViewModel, true, _marketDataViewModel.SelectedStock);
            highTouchTradePopupView.Show();
        }

        private void SellBtn_Click(object sender, RoutedEventArgs e)
        {
            var highTouchTradePopupView = new HighTouchTradeView(_marketDataViewModel, false, _marketDataViewModel.SelectedStock);
            highTouchTradePopupView.Show();
        }

        #region TimespanBtn click

        private void TimespanBtn1D_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Day_1d;
        }
        private void TimespanBtn5D_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c5Days_5d;
        }

        private void TimespanBtn3M_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c3Months_3m;
        }

        private void TimespanBtn6M_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c6Months_6m;
        }

        private void TimespanBtn1Y_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c1Year_1y;
        }

        private void TimespanBtn2Y_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c2Years_2y;
        }

        private void TimespanBtn5Y_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.c5Years_5y;
        }

        private void TimespanBtnMAX_Click(object sender, RoutedEventArgs e)
        {
            _marketDataViewModel.timeSpan = YahooChart.ChartTimeSpan.cMaximum_my;
        }

        #endregion

        private void LineTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_marketDataViewModel == null) return;

            switch (((ComboBoxItem)LineTypeComboBox.SelectedItem).Content.ToString())
            {
                case "Candle":
                    _marketDataViewModel.chartType = YahooChart.ChartType.Candle_c;
                    break;
                case "Line":
                    _marketDataViewModel.chartType = YahooChart.ChartType.Line_l;
                    break;
                case "Bar":
                    _marketDataViewModel.chartType = YahooChart.ChartType.Bar_b;
                    break;
                default:
                    _marketDataViewModel.chartType = YahooChart.ChartType.Candle_c;
                    break;
            }
        }

        #region MovingAverageIndicator checked
        private void MA5_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA5.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m5_5);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m5_5);
        }

        private void MA10_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA10.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m10_10);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m10_10);
        }

        private void MA20_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA20.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m20_20);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m20_20);
        }

        private void MA50_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA50.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m50_50);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m50_50);
        }

        private void MA100_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA100.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m100_100);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m100_100);
        }

        private void MA200_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)MA200.IsChecked) _marketDataViewModel.movingAvgIntervalList.Add(YahooChart.MovingAverageIndicator.m200_200);
            else _marketDataViewModel.movingAvgIntervalList.Remove(YahooChart.MovingAverageIndicator.m200_200);
        }

        #endregion

        private void AddWatchlistButton_Click(object sender, RoutedEventArgs e)
        {
            // Control safety access to WatchList
            lock (_marketDataViewModel.SyncWatchListLock)
            {

                ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                    MarketDataListView.SelectedIndex = MarketDataListView.ItemContainerGenerator.IndexFromContainer(lvi);
                if (_marketDataViewModel.SelectedStock != null)
                {
                    if (_marketDataViewModel.WatchList.Count == 0)
                        WatchlistListView.SelectedIndex = 0;

                    string symbolToAdd = _marketDataViewModel.SelectedStock.Symbol;
                    int watchListCount = _marketDataViewModel.WatchList.Count;
                    bool isFound = false;
                    for (int i = 0; i < watchListCount; i++)
                    { 
                        if (_marketDataViewModel.WatchList[i].Symbol == symbolToAdd)
                        {
                            isFound = true;
                            break;
                        }
                    }
                        
                    if (!isFound)
                        _marketDataViewModel.WatchList.Add(_marketDataViewModel.SelectedStock);

                }

            }
        }

        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {

            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);

        }

        private void RemoveWatchlistButton_Click(object sender, RoutedEventArgs e)
        {
            // Control safety access to WatchList
            lock (_marketDataViewModel.SyncWatchListLock)
            {
                ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

                if (lvi != null)
                    WatchlistListView.SelectedIndex = WatchlistListView.ItemContainerGenerator.IndexFromContainer(lvi);
                if (_marketDataViewModel.SelectedStock != null)
                {
                    string symbolToRemove = _marketDataViewModel.SelectedStock.Symbol;
                    int indexToRemove = -1;
                    int watchListCount = _marketDataViewModel.WatchList.Count;                    
                    for (int i = 0; i < watchListCount; i++)
                    {
                        if (_marketDataViewModel.WatchList[i].Symbol == symbolToRemove)
                        {
                            indexToRemove = i;
                            break;
                        }
                    }
                    if (indexToRemove != -1) _marketDataViewModel.WatchList.RemoveAt(indexToRemove);
                }
            }
                
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LiveFeedsTab.IsSelected)
            {
                setSelectedLiveFeed();
            }
            else if (WatchlistTab.IsSelected)
            {

                setSelectedWatchlist();
            }
        }

        private void setSelectedLiveFeed() 
        {
            if (!LiveFeedsTab.IsSelected) return;

            if (MarketDataListView.SelectedItem != null)
            {
                _marketDataViewModel.SelectedStock = (Stock)MarketDataListView.SelectedItem;
                
                // Retrieve the selected stock's summary
                _marketDataViewModel.getStockSummary();

                // For retrieving the corresponding chart
                if (_marketDataViewModel.SelectedStock != null) _marketDataViewModel.stockString = _marketDataViewModel.SelectedStock.Symbol;
            }
        }
        private void setSelectedWatchlist()
        {
            if (!WatchlistTab.IsSelected) return;

            if (WatchlistListView.SelectedItem != null)
            {
                _marketDataViewModel.SelectedStock = (Stock)WatchlistListView.SelectedItem;                

                // Retrieve the selected stock's summary
                _marketDataViewModel.getStockSummary();

                // For retrieving the corresponding chart
                if (_marketDataViewModel.SelectedStock != null) _marketDataViewModel.stockString = _marketDataViewModel.SelectedStock.Symbol;
            }                        
        }

    }

    

}
