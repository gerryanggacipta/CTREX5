   M S B A M L   `   `   `  ���� |  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization  �{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting   TREX   TREX.PortfolioView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     �DVrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     �DVC{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     .��� $��10q�  � RowDefinitions  �� $
��Auto ��� $��* �����  �� EndPoint$ 0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF313336�� � Offset$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q��    BlueGrayGradient# �   $ 0.417Q�.+��� $��0�� �� Orientation$ 
Horizontal=�$��Center=�.+�>   8System.Windows.Controls.DataVisualization.Charting.Chart  -   $��pieChart��   Title$ 	Portfolio��   LegendTitle$ Stocks��$	��370��$	��380��$��0q�
��� ���	   Series.	 B   <System.Windows.Controls.DataVisualization.Charting.PieSeries  -   H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries
   IndependentValuePath$
 Stock��   DependentValuePath$ 	TotalSize��   IsSelectionEnabled .    ItemsSource �� *PortfolioEntries+  -   $��columnChart��$ Stock Performance��$	��370��$	��600��$��0q�
��� ���.	 E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  -   ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Profit/Loss��$
 Stock��$ IncrementalBalance�� .  �� *PortfolioEntries+�� $��1��$ 
Horizontal=�$��	60,0,0,20q�.+�&   System.Windows.Controls.DataGrid  -   $��PortfolioEntryDataGrid��   AutoGenerateColumns .  
�������
������    
IsReadOnly .    RowBackground
 ���� 
������    GridLinesVisibility$
 None=�   HeadersVisibility$ Column=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$ Visible=����� *PortfolioEntries+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Stock��   MinWidth$ 70Q�1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *Stock+  $ AvailableSize��$ 70Q� �� *AvailableSize+  $ 	TotalSize��$ 90Q� �� *	TotalSize+  $ IncrementalBalance��$ 90Q� �� *IncrementalBalance+ � 	Resources =
  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%/   '
        �� " 
 .���  � ��
 ����   -   $��TradeListDataGrid�� .  $��20,0,0,0q�
�������
������  . 
 ���� 
������ $
 None=�$ Column=�$
 Auto=�$ Visible=����� *TradeListByStock+   $ Id��$ 20Q� �� *Id+  $
 Auto��$ 40Q� �� *Auto+  $ 
StrategyID��$ 80Q� �� *
StrategyID+  $ Stock��$ 70Q� �� *Stock+  $	 Buy��$ 40Q� �� *Buy+  $ Short��$ 40Q� �� *Short+  $ Price��$ 90Q� �� *Price+  $
 Size��$ 50Q� �� *Size+  $ Position��$ 60Q� �� *
Position+  $	 PnL��$ 30Q� �� *PnL+  $ 
WhenAsDate��$ 10Q� �� *
WhenAsDate+ ��%/   '
        �� " 
 .���  � ��
 ���� 