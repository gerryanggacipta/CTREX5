﻿using System;
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
    /// Interaction logic for TradeView.xaml
    /// </summary>
    public partial class TradeView : UserControl
    {
        private TradeViewModel _tradeViewModel;
        public TradeView()
        {
            InitializeComponent();
            _tradeViewModel = TradeViewModel.Instance;
            DataContext = _tradeViewModel;
        }
    }
}
