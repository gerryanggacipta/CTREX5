   M S B A M L   `   `   `  ����   TREX    TREX.HighTouchTradeView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  High Touch Trade��$	��210��$	��300��
�������.��� .+�� $	��250��$
��Auto��$��0,15,0,0q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* � � 	Resources  �� 
TargetType �� Property �� Value��%2   '��       �� " ��.���  � ��$ 0,0,0,3q�.+��� $��0��$��0��$��2�� �� Orientation$ 
Horizontal=�$��Center=�$��0,0,0,10q�.+�	� -   $��BuyRadio�� 	� 	GroupName$ TradeOption��$	��Buy��
�������$��0,0,10,0q�	 d� 	IsChecked	 �� *Buy+	� -   $��	SellRadio��$ TradeOption��$
��Sell��
�������$��0,0,40,0q�	 �� *Sell+	� -   $��	LongRadio��$ PositionOption��$
��Long��$
	 TrueY�
�������$��0,0,10,0q�	� -   $��
ShortRadio��$ PositionOption��$��Short��
�������$��0,0,10,0q��� $��1��$��0��$��Stock��
��������� -   $��
StockLabel��$��1��$��1��
�������$
��Bold"����� *SelectedStock.NameSymbol+�� $��2��$��0��$��Price��
��������� -   $��PriceTextBox��$��2��$��1������ *SelectedStock.Bid+�� $��3��$��0��$
��Size��
��������� -   $��SizeTextBox��$��3��$��1���� -   $��
ErrorLabel��$��4��$��1��
���  ��$��0q��� -	   $��
ConfirmBtn��$��Confirm��{�.  $
��Left=�$��207,145,0,0q�$	��Top=�$��65��