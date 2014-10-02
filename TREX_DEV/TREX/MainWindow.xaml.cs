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

namespace TREX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MarketDataView _marketDataView;
        StrategyView _strategyView;
        PortfolioView _portfolioView;
        TradeView _tradeView;
        PerformanceView _performanceView;

        public MainWindow()
        {
            InitializeComponent();
            MainWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _marketDataView = new MarketDataView();
            _strategyView = new StrategyView();
            _portfolioView = new PortfolioView();
            _tradeView = new TradeView();
            _performanceView = new PerformanceView();            
            
            ContentFrame.Content = _marketDataView; 
        }

        private void MarketDataBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _marketDataView;
        }

        private void StrategyBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _strategyView; 
        }

        private void PortfolioBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _portfolioView; 
        }

        private void TradeBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _tradeView; 
        }

        private void PerformanceBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _performanceView; 
        }
    }
}
