   M S B A M L   `   `   `  ����   TREX    TREX.TradeView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� $��0q�.+��� .+��� $��	My Trades��$��1��$	��560��
�������$
��Bold"�$��0q�����   �� EndPoint$  0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF313336�� � Offset$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q��    BlueGrayGradient# �   $ 0.417Q�.��&   System.Windows.Controls.DataGrid  -   $��MyTradesDataGrid��   AutoGenerateColumns .  
�������
������    
IsReadOnly .    RowBackground
 ���� 
������    GridLinesVisibility$
 None=�	   HeadersVisibility$	 Column=�"
   VerticalScrollBarVisibility$

 Auto=����� *
MyTrades+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   MinWidth$ 70Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ 
StrategyID��$ 90Q� �� *
StrategyID+  $ Stock��$	 110Q� �� *Stock+  $
 Auto��$ 80Q� �� *Auto+  $ Position��$ 90Q� �� *
Position+  $	 Buy��$ 80Q� �� *Buy+  $ Price��$ 80Q� �� *Price+  $
 Size��$ 80Q� �� *Size+  $ 
WhenAsDate��$ 80Q� �� *
WhenAsDate+ � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%/   '        �� "  .���  � ��
 ���� 