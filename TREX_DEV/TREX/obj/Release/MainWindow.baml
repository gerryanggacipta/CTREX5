   M S B A M L   `   `   `  ����   TREX    TREX.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��MainWin��  � Title$
  TREX��$	��718��$
��1300��
���   � � 	Resources    RoundCorner �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius	 �� EndPoint
 �� 
StartPoint � Color � Offset �� Triggers P� Property P� Value �� 
TargetName � MappingMode  BlueGradient��%S  &	        �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
 grid��.+��� $ border��$ 8��
�������$��1q����� $		 0,1&�$	
 0,0&�.��� $ 	#FFEEEEEE��$ 1Q�� $ 	LightGray��$ 0.659Q�� $ 	#FFCFCFCF��$ 0.357Q�� $ 
WhiteSmoke��._��� $��Center=�$��Center=�$
��Bold"� P�  �@J�IsMouseOver . . ��� $ border�� � �� �� $	 0.5,1&�$
 0.5,0&�.��� $ Black��$ 0Q�� $ White��$ 1Q��� $ border�� � ��$ 1q�P�  � {� .  . ��� $
 grid�� �@J�Opacity$
 0.25Q�P�  � �� . . ��� $ border�� � �� �� $	 0.5,1&�$ RelativeToBoundingBox=�$
 0.5,0&�.��� $ 	#FF89D1E6��$ 1Q�� # �  $ 1Q�� $ 	#FF1B8DFF��$ 0.514Q�� $ 	#FF61AEF1��.��� $��
10,10,10,0q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$��90��
������� �� RadiusX$ 15�� �� RadiusY$ 15������ $	 0.5,1&�$ RelativeToBoundingBox=�$
 0.5,0&�.��� $ 	#FF313336��$ 1Q�� $ 	#FF7A7A7A��� $ 	#FF3B3E44��$ 0.712Q��   BlueGrayGradient# �  $ 0.417Q�� $��	30,0,30,0q� � ColumnDefinitions �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Images/logo.png��$	��220��$
��Left=��� $��0��$��1��$��0,0,10,0q� �� Orientation$ Vertical=�$
��auto��$
��auto��$��Center=�$��Center=�.+��� -    ��Name$ MarketDataBtn��$��60��
������ $��0q�#���   .���� $��Images/marketData.png���� $��Market Data��
��������� $��2��$��0,0,10,0q�$ Vertical=�$
��auto��$
��auto��$��Center=�$��Center=�.+��� -   $ StrategyBtn��$��60��
������ $��0q�#���   ���� $		 0,1&�$	
 0,0&�.��� $ 	#FFEEEEEE��$ 1Q�� $ 	LightGray��$ 0.659Q�� $ 	#FFCFCFCF��$ 0.357Q�� $ 
WhiteSmoke��.���� $��Images/strategy.png���� $��Strategy��$��7,0,0,0q�
��������� $��3��$��0,0,10,0q�$ Vertical=�$
��auto��$
��auto��$��Center=�$��Center=�.+��� -   $ TradeBtn��$��60��
������ $��0q�#���   .���� $��Images/trade.png���� $��Trades��$��10,0,0,0q�
��������� $��4��$��0,0,10,0q�$ Vertical=�$
��auto��$
��auto��$��Center=�$��Center=�.+��� -   $ PortfolioBtn��$��60��
������ $��0q�#���   .���� $��Images/portfolio.png���� $��	Portfolio��$��7,0,0,0q�
������� � -     �Name$ ContentFrame��$��1��$��0��  � NavigationUIVisibility$ Hidden=�$��0,0,0,10q�$��0q�