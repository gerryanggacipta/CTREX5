   M S B A M L   `   `   `  ���� I  ETREX.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TREX.Utilities;assembly=TREX.UtilitiesTREX.Utilities   TREX   TREX.StrategyView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Flocal4clr-namespace:TREX.Utilities;assembly=TREX.Utilities       � 	Resources     
GridHeader �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� TextAlignment*   $TREX.Utilities.ByteToSourceConverter  ByteToSourceConverter��%�   &	        &	 m     �� " ��.���  � �� �� " ��.R��� #��z��$ 5,0,0,0q�#��z��$
 Left=�
�������$
��Bold"�  .��� $��5q� � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��
�������$��0q�$	��250��$��0,0,10,0q����� 	 �� EndPoint$	 0.5,1&�
 � MappingMode$
 RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF313336�� � Offset$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q��   BlueGrayGradient# �  $ 0.417Q����� $
��Bold"�$��14(�
Strategies.���� .+���  �� Orientation$ 
Horizontal=�$��Right=�.+��� -   $��StartButton��$��START��$��50��$��Right=�
����������� $	 0.5,1&�$
 RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF89D1E6��$ 1Q��   BlueGradient# �  $ 1Q�� $ 	#FF1B8DFF��$ 0.514Q�� $ 	#FF61AEF1���� -   $��	NewButton��$��+ New��$��50��$��Right=�
����������� $		 0,1&�$	 0,0&�.��� $ 	#FFFFA254��$ 0Q�� $ 	#FFF97930��$
 0.47Q�� $ 	#FFF97930��$
 0.53Q�� $ 	#FFFFA254��$ 0.992Q��� $ 
Horizontal=�.+�	� -   $��AllRadioButton�� 	� 	GroupName$ strategyOpt��$	��All�� d� 	IsChecked$
 TrueY�
�������$��0,0,10,0q�	� -   $��MovingAvgRadioButton��$ strategyOpt��$��Two Moving Averages��
�������$��0,0,10,0q�	� -   $��BollingerBandRadioButton��$ strategyOpt��$��Bollinger Band��
�������$��0,0,10,0q��� -    ��Name$ StrategyConfigListView��$	��410��$	��185��$
��Left=�$	��Top=�
�������
������ $ �� IsSynchronizedWithCurrentItem$
 TrueY����� *StrategyList+   �� Triggers P� Property P� Value��%�   '��       �� " �� P�  �@��
IsSelected . . ���  � ��
 �   �P�  �@J�IsMouseOver . . ���  � ��
 �   �.���  � ��
 ����� �� View �� ! �� ColumnHeaderContainerStyle# [  P���  �� CellTemplate �� .R��� $��ActivatedCheckBox��-   
 � Tag$ 	Activated�� �� *	Activated+���� -   $ 	Activated��.��	Activated��  �� DisplayMemberBinding �� *Name+���� -	   $
 Name��.��Config Name�� $��0��$��1��$��2��
�������$��0q����� $	 0.5,1&�$
 RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q�� # �  $ 0.417Q����� $
��Bold"�$��14(�
Live Chart.���� $ Vertical=�.+��� $ 
Horizontal=�$��10,0,0,0q�$��Center=�$
��auto��.+��� $ 
Horizontal=�$��0,0,0,0q�$��20��.+��� -
   $��TimespanBtn1D��$��1D��$��11(��� -   $��TimespanBtn5D��$��5D��$��11(��� -   $��TimespanBtn3M��$��3M��$��11(��� -   $��TimespanBtn6M��$��6M��$��11(��� -   $��TimespanBtn1Y��$��1Y��$��11(��� -   $��TimespanBtn2Y��$��2Y��$��11(��� -   $��TimespanBtn5Y��$��5Y��$��11(��� -   $��TimespanBtnMAX��$	��MAX��$��11(��� $ 
Horizontal=�$��
25,0,25,10q�$��25��$��Center=�.+��� $��	Line Type��
��������� -   $��LineTypeComboBox��$��65��$��Center=�.@��� $��Candle�� �� 
IsSelected . �� $
��Line���� $	��Bar���� $ 
Horizontal=�.+��� $��Moving Average Interval��
��������� $ 
Horizontal=�$��10,0,0,0q�.+��� $��0,0,10,0q�.+��� -   $	��MA5��$��5��
��������� -   $
��MA10��$��10��
��������� $��0,0,10,0q�.+��� -   $
��MA20��$��20��
��������� -   $
��MA50��$��50��
��������� .+��� -   $��MA100��$	��100��
��������� -   $��MA200��$	��200��
��������� $	��500������  �� Path$ 
StockImage� �� 	Converter# [ �� $��1��$��0��
�������$��0q�$��0,0,10,0q����� $	 0.5,1&�$
 RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q�� # �  $ 0.417Q����� $
��Bold"�$��14(�Strategy Configuration.��� $��5q� �� $	��100 ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��Config Name:��
��������� -   $��ConfigNameTextBox��$��0��$��1��$	��300��$
��Left=����� *SelectedStrategy.Name+ �� Mode$ OneWay=��� $��1��$��0��$��	Strategy:��
��������� -   $��StrategyComboBox��$��1��$��1��$
��Left=�$��Center=�$	��300��.@���  . $��Automatic Trading���� $��Two Moving Averages���� $��Bollinger Band���� $��2��$��0��$��Stock:��
��������� $��2��$��1��.+��� -   $��StockComboBox��$
��Left=�$��Center=�$	��300�� �� SelectedValue �� *SelectedStrategy.Stock+$ OneWay=��� $ 
Horizontal=�$��Right=�.+��� -   $��ExchangeVolLabel��$��Exchange Volume: -��
�������$��0q�$��0,0,20,0q��� -   $��AvailableSizeLabel��$��Available Size: -��
�������$��0q��� $��3��$��0��$��Trade Option:��
��������� $��3��$��1��$ 
Horizontal=�$��Center=�.+�	� -   $��BuyRadio��$ TradeOption��$	��Buy��
�������$��0,0,10,0q� �� *SelectedStrategy.Buy+$ OneWay=�	� -   $��	SellRadio��$ TradeOption��$
��Sell��
������� �� *SelectedStrategy.Sell+$ OneWay=��� $��4��$��0��$��	Position:��
��������� $��4��$��1��$ 
Horizontal=�$��Center=�.+��� -    $��ShortCheckBox��$��Short��
������� �� *SelectedStrategy.Short+$ OneWay=��� $��5��$��0��$��Size:��
��������� $��5��$��1��.+��� -!   $��SizeTextBox��$	��300��$
��Left=����� *SelectedStrategy.Size+$ OneWay=��� -"   $��
ErrorLabel��
���  ��$��0q��� $��6��$��1��$ 
Horizontal=�$��Center=�$��65,19,165,1q�.+��� -#   $��
SaveButton��$
��Save��{�.  $��70������ $	 0.5,1&�$
 RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF89D1E6��$ 1Q�� # �  $ 1Q�� $ 	#FF1B8DFF��$ 0.514Q�� $ 	#FF61AEF1��