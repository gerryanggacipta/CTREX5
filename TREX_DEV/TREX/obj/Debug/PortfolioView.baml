   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization  5      �{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5       TREX   TREX.PortfolioView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      �DVrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      �DVC{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$ 	#FF313336��6    � Offset$ 1Q�61   � 5      $ 	#FF7A7A7A��6   � 5      $ 	#FF3B3E44��6   $ 0.712Q�61   � 5         BlueGrayGradient# �   6   $ 0.417Q�6J   5      .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��pieChart��5         Title$ 	Portfolio��6(      LegendTitle$ Stocks��6:   $	��370��6P   $	��380��6]   $��0q�6i   
��� ���	   Series.	 5      B   <System.Windows.Controls.DataVisualization.Charting.PieSeries  -   5        H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries
   IndependentValuePath$
 Stock��5   I      DependentValuePath$ 	TotalSize��6f      IsSelectionEnabled .    ItemsSource 6    �� *PortfolioEntries+6�   5        5      -   5        $��columnChart��5      $ Stock Performance��6+   $	��370��6E   $	��600��6R   $��0q�6^   
��� ���.	 5      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  -   5        ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Profit/Loss��5   #   $
 Stock��6`   $ IncrementalBalance��6}    .  67   �� *PortfolioEntries+6�   5      5      �� 5    
   $��1��6   $ 
Horizontal=�6"   $��	60,0,0,20q�6;   .+�5!      &   System.Windows.Controls.DataGrid  -   5        $��PortfolioEntryDataGrid��5!         AutoGenerateColumns .  
�������
������    
IsReadOnly .    RowBackground
 ���� 
������    GridLinesVisibility$
 None=�6�      HeadersVisibility$ Column=�6	  $   HorizontalScrollBarVisibility$
 Auto=�6$  "   VerticalScrollBarVisibility$ Visible=�6I  ��6Q   �� *PortfolioEntries+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Stock��6)      MinWidth$ 70Q�6R   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 68   �� *Stock+6R     5$      $ AvailableSize��6)   $ 70Q�6b    6@   �� *AvailableSize+6b     5%      $ 	TotalSize��6)   $ 90Q�6Z    6<   �� *	TotalSize+6Z     5&      $ IncrementalBalance��6)   $ 90Q�6l    6E   �� *IncrementalBalance+6l    � 	Resources 5(      =
  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%T   '
        �� 5)      " 
 6   .�5*      ��  � ��
 ���� 67   5+      5,      5-        5.      -   5        $��TradeListDataGrid��5.       .  $��20,0,0,0q�6u   
�������
������  . 
 ���� 
������ $
 None=�6�   $ Column=�6  $
 Auto=�61  $ Visible=�6V  ��6L   �� *TradeListByStock+ 5/        50      $ Id��6)   $ 20Q�6L    65   �� *Id+6L     51      $
 Auto��6)   $ 40Q�6P    67   �� *Auto+6P     52      $ 
StrategyID��6)   $ 80Q�6\    6=   �� *
StrategyID+6\     53      $ Stock��6)   $ 70Q�6R    68   �� *Stock+6R     54      $	 Buy��6)   $ 40Q�6N    66   �� *Buy+6N     55      $ Short��6)   $ 40Q�6R    68   �� *Short+6R     56      $ Price��6)   $ 90Q�6R    68   �� *Price+6R     57      $
 Size��6)   $ 50Q�6P    67   �� *Size+6P     58      $ Position��6)   $ 60Q�6X    6;   �� *
Position+6X     59      $	 PnL��6)   $ 30Q�6N    66   �� *PnL+6N     5:      $ 
WhenAsDate��6)   $ 10Q�6\    6=   �� *
WhenAsDate+6\    5<      ��%T   '
        �� 5=      " 
 6   .�5>      ��  � ��
 ���� 67   5?      5@      5A      5B      5C      5D      