   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  I ETREX.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TREX.Utilities;assembly=TREX.UtilitiesTREX.Utilities  TREX   TREX.MarketDataView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     Flocal4clr-namespace:TREX.Utilities;assembly=TREX.Utilities       � 	Resources     GrayGradient  
GridHeader �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� TextAlignment  ChangeCellTemplate �� Path(  "TREX.Utilities.ZeroToDashConverter  ZeroToDashConverter*  $TREX.Utilities.ByteToSourceConverter  ByteToSourceConverter��%
  &	        &	      &	 �     &	 �     &	 �     ��	#FF404040���� " ��.���  � �� �� " ��.R��� #��z��$ 5,0,0,0q�#��z��$
 Left=�
�������$
��Bold"��� .R��� $
��Bold"����� $ ChangeForeground����� $ Change�    .��� $��0q� � ColumnDefinitions �� $
��Auto ��� $��* �	 � RowDefinitions	 �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��
�������$��0,0,10,0q�$��0q����� 
 �� EndPoint$
 0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF313336�� � Offset$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q��   BlueGrayGradient# �  $ 0.417Q����� $
��Bold"�$��14(�Market Data.���� .+���  �� Orientation$ 
Horizontal=�.+��� -   $��FilterComboBox��$��25��$��70��$��Center=�.@���  �� 
IsSelected . $��Symbol���� $
��Name���� $��Exchange���� -   $��FilterTextBox��$��10q�$	��178���� -   ���� $
 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q�� # �  $ 0.417Q�.@��� -   $��LiveFeedsTab��$��
Live Feeds��.���� -    ��Name$ MarketDataListView�� �� SelectedIndex$ 0��$	��450��
������ 
������ $��1.000001q����� *	AllStocks+ �� Mode$ OneWay=�����  �� RelativeSource ��  �� AncestorType$ ListViewItemL� �� Mode$ FindAncestor=�$ 
Foreground�   �� Triggers P� Property P� Value��%�   '��       �� " �� P�  �@��
IsSelected . . ���  � ��
 �   �P�  �@J�IsMouseOver . . ���  � ��
 �   �.���  � ��
 ����� �� View �� ! �� ColumnHeaderContainerStyle# [ .P���  �� CellTemplate �� .R���  ��Name$ AddWatchlistButton��-   $��0q�$��Center=�
������ 
������ $��0q�$��-4q�.���� $��Images/addIcon.png��$��13����  �� Width$ 80�� �� DisplayMemberBinding �� *Symbol+���� -   
  � Tag$  Symbol��.��Stock�� $ 80�� �� *Bid+! �� 	Converter#! [ ���� -   $	  Bid��.��Bid�� $ 80��# [ ���� -	   $  Change��.��Change�� -
   $��WatchlistTab��$��	Watchlist��.���� -   $ WatchlistListView��$	��290��$	��440��
������ 
������ $��0q����� *	WatchList+����  �� $ ListViewItemL�$ FindAncestor=�$ 
Foreground�  ��%�   '��       �� " �� P�  �@��
IsSelected . . ���  � ��
 �   �P�  �@J�IsMouseOver . . ���  � ��
 �   �.���  � ��
 ����� �� # [ .P���  �� .R��� $ RemoveWatchlistButton��-   $��0q�$��Center=�
������ 
������ $��0q�$��-4q�.���� $��Images/removeIcon.png��$��13���� $ 80�� �� *Symbol+���� -   $  Stock��.��Stock�� $ 80�� �� *Bid+���� -   $	  Bid��.��Bid�� $ 80��# [ ���� -   $  Change��.��Change�� $��0��$��1��$	��202��
�������$��0q����� $
 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q�� # �  $ 0.417Q����� $
��Bold"�$��14(�Stock Summary.���� .+��� $ 
Horizontal=�" �� Left$" 19��
# �� Top$# 26��.+��� -   $��StockNameLbl��$��18(�$��Global Sans Serif)�
����������� *SelectedStock.NameSymbol+�� -   $��StockDateLbl��$��12(�$��Global Sans Serif)�
����������� *SelectedStock.When+�� -   $��StockPriceLbl��$$��26                            (�$" 19��$# 65��$
��Bold"�$��Global Sans Serif)�
����������� *SelectedStock.Bid+#! [ �� $��14��$	" 170��$# 78������ *SelectedStock.ChangeImage+�� -   $��StockChangeLbl��$��20(�$	" 185��$# 70��$��Global Sans Serif)����� *SelectedStock.ChangeString+���� *SelectedItem.ChangeForeground+$ �� ElementName$$ MarketDataListView���� -   $��BuyBtn��$	��Buy��$" 70��$	# 115��$	��Top=�$��70��$��35��$��20(�
����������� $
 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ White��$ 1Q����� $
 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF89D1E6��$ 1Q��   BlueGradient# �  $ 1Q�� $ 	#FF1B8DFF��$ 0.514Q�� $ 	#FF61AEF1���� -   $��SellBtn��$
��Sell��$	" 155��$	# 115��$	��Top=�$��70��$��35��$��20(�
����������� $
 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ White��$ 1Q����� $
 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF89D1E6��$ 1Q�� # �  $ 1Q�� $ 	#FF1B8DFF��$ 0.514Q�� $ 	#FF61AEF1��&   System.Windows.Controls.DataGrid  -   $��StockSummaryDataGrid��%   AutoGenerateColumns% .  $	" 435��$# 35��& J� IsHitTestVisible& .  
�������
������ '   RowBackground
' ���� 
������ (   GridLinesVisibility$
( None=�)   HeadersVisibility$
) None=����� *StockSummary+*   Columns* 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn+   Header$
+ Name��,   Width5  /System.Windows.Controls.DataGridLengthConverter$	, 120 1  +System.Windows.Controls.DataGridBoundColumn-   Binding- �� *SummaryName+  $+ Value��.   
FontWeight$
. Bold"�- �� *SummaryValue+�� $��1��$��1��
�������$��0q����� $
 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q�� # �  $ 0.417Q����� $
��Bold"�$��14(�
Live Chart.���� $ 
Horizontal=�.+��� $ Vertical=�$��	10,30,0,0q�$��Center=�$
��auto��.+��� $ 
Horizontal=�$��0,0,20,0q�.+��� -   $��TimespanBtn1D��$��1D��$��11(��� -   $��TimespanBtn5D��$��5D��$��11(��� -   $��TimespanBtn3M��$��3M��$��11(��� -   $��TimespanBtn6M��$��6M��$��11(��� -   $��TimespanBtn1Y��$��1Y��$��11(��� -   $��TimespanBtn2Y��$��2Y��$��11(��� -   $��TimespanBtn5Y��$��5Y��$��11(��� -   $��TimespanBtnMAX��$	��MAX��$��11(��� $ 
Horizontal=�$��	0,15,0,10q�.+��� $��	Line Type��
��������� -   $��LineTypeComboBox��$��65��$��Center=�.@��� $��Candle�� . �� $
��Line���� $	��Bar���� $��Moving Average Interval��
��������� $ 
Horizontal=�$��10,0,0,0q�.+��� $��0,0,10,0q�.+��� -    $	��MA5��$��5��
��������� -!   $
��MA10��$��10��
��������� $��0,0,10,0q�.+��� -"   $
��MA20��$��20��
��������� -#   $
��MA50��$��50��
��������� .+��� -$   $��MA100��$	��100��
��������� -%   $��MA200��$	��200��
��������� ���� $ 
StockImage�#! [ 