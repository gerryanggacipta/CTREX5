   M S B A M L   `   `   `  ����  TREX    TREX.TradeView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��0q�6   .+�5	   
   �� .+�5
      �� $��	My Trades��6   $��1��6*   $	��560��67   
�������$
��Bold"�6W   $��0q�6i   ��5      �� 5        �� EndPoint$  0.5,1&�6*    � MappingMode$ RelativeToBoundingBox=�6;    �� 
StartPoint$ 0.5,0&�6_   .��5      �  � Color$ 	#FF313336��6'    � Offset$ 1Q�69   � 5      $ 	#FF7A7A7A��6'   � 5      $ 	#FF3B3E44��6'   $ 0.712Q�69   � 5         BlueGrayGradient# �   6'   $ 0.417Q�6R   5      .��5      &   System.Windows.Controls.DataGrid  -   5        $��MyTradesDataGrid��5         AutoGenerateColumns .  
�������
������    
IsReadOnly .    RowBackground
 ���� 
������    GridLinesVisibility$
 None=�6�   	   HeadersVisibility$	 Column=�6�   "
   VerticalScrollBarVisibility$

 Auto=�6  ��6O   �� *
MyTrades+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      MinWidth$ 70Q�6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5      $ 
StrategyID��6-   $ 90Q�6`    6A   �� *
StrategyID+6`     5      $ Stock��6-   $	 110Q�6V    6<   �� *Stock+6V     5      $
 Auto��6-   $ 80Q�6T    6;   �� *Auto+6T     5      $ Position��6-   $ 90Q�6\    6?   �� *
Position+6\     5      $	 Buy��6-   $ 80Q�6R    6:   �� *Buy+6R     5      $ Price��6-   $ 80Q�6V    6<   �� *Price+6V     5      $
 Size��6-   $ 80Q�6T    6;   �� *Size+6T     5      $ 
WhenAsDate��6-   $ 80Q�6`    6A   �� *
WhenAsDate+6`    � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%T   '        �� 5       "  6    .�5!      ��  � ��
 ���� 6;   5"      5#      5$      5%      5&      5'      5(      