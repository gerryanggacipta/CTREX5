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
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;
using TREX.Entities;

namespace TREX
{
    /// <summary>
    /// Interaction logic for PortfolioView.xaml
    /// </summary>
    public partial class PortfolioView : UserControl
    {
        private PortfolioViewModel _portfolioViewModel;
        public PortfolioView()
        {
            InitializeComponent();
            _portfolioViewModel = new PortfolioViewModel();
            DataContext = _portfolioViewModel;
        }

        private void PieSeries_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string selectedStock = ((PortfolioEntry)(((PieSeries)sender).SelectedItem)).Stock;
            _portfolioViewModel.getAllTradesBySymbol(selectedStock);
        }

        private void ColumnSeries_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string selectedStock = ((PortfolioEntry)(((ColumnSeries)sender).SelectedItem)).Stock;
            _portfolioViewModel.getAllTradesBySymbol(selectedStock);
        }
    }
}
