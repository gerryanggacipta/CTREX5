   M S B A M L   `   `   `  ����  TREX    TREX.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��MainWin��5   0     � Title$
  TREX��6	   $	��718��6   $
��1300��6#   
���   � � 	Resources 5         RoundCorner �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius	 �� EndPoint
 �� 
StartPoint � Color � Offset �� Triggers P� Property P� Value �� 
TargetName � MappingMode  BlueGradient��%�  &	        �� 5   
   " ��6$   .�5      ��  � ��$ Center=�6;   �� 5	       � ��$ Center=�69   �� 5
       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � $
 grid��6   .+�5      �� $ border��6%   $ 8��65   
�������$��1q�6[   ��5   "   �� 5   &   $		 0,1&�6:   $	
 0,0&�6I   .��5   *   � $ 	#FFEEEEEE��67   $ 1Q�6I   � 5   *   $ 	LightGray��67   $ 0.659Q�6I   � 5   *   $ 	#FFCFCFCF��67   $ 0.357Q�6I   � 5   *   $ 
WhiteSmoke��67   5   '   ._�5   "   �� $��Center=�63   $��Center=�5      $
��Bold"�5      6:   5      5!       5"      P� 54       �@J�IsMouseOver . . �55   "   �� $ border��6@    � �� 56   &   �� 57   *   $	 0.5,1&�6>   $
 0.5,0&�6O   .��58   .   � $ Black��6;   $ 0Q�6I   � 59   .   $ White��6;   $ 1Q�6I   5:   +   5<   #   �� 5>   "   $ border��6D    � ��$ 1q�6X   5?      P� 5@       � {� .  . �5A   "   �� $
 grid��6<    �@J�Opacity$
 0.25Q�6N   5B      P� 5C       � �� . . �5D   "   �� $ border��6?    � �� 5E   &   �� 5F   *   $	 0.5,1&�6>   $ RelativeToBoundingBox=�6O   $
 0.5,0&�6s   .��5G   .   � $ 	#FF89D1E6��6;   $ 1Q�6M   � 5H   .   # �  6;   $ 1Q�6b   � 5I   .   $ 	#FF1B8DFF��6;   $ 0.514Q�6M   � 5J   .   $ 	#FF61AEF1��6;   5K   +   5M   #   5N      5Q      5S      5T      5U      .��5V      � $��
10,10,10,0q�6    � RowDefinitions 5W   
   �� 5X      $
��Auto �6   �� 5Y      $��* �6   .+�5[   
   �� $��0��6   $��0��6!   $��90��61   
������� �� RadiusX$ 15��6K    �� RadiusY$ 15��6X   ��5\      �� 5]      $	 0.5,1&�6&   $ RelativeToBoundingBox=�67   $
 0.5,0&�6[   .��5^      � $ 	#FF313336��6#   $ 1Q�65   � 5_      $ 	#FF7A7A7A��6#   � 5`      $ 	#FF3B3E44��6#   $ 0.712Q�65   � 5a        BlueGrayGradient# �  6#   $ 0.417Q�6N   5b      5d      � 5e   
   $��	30,0,30,0q�6    � ColumnDefinitions 5f      �� 5g      $��* �6#   �� 5h      $
��Auto �6#   �� 5i      $
��Auto �6#   �� 5j      $
��Auto �6#   �� 5k      $
��Auto �6#   .+�5m      �� $��0��6   $��Images/logo.png��6$   $	��220��6=   $
��Left=�6I   �� 5n      $��0��6   $��1��6&   $��0,0,10,0q�66    �� Orientation$ Vertical=�6H   $
��auto��6_   $
��auto��6m   $��Center=�6z   $��Center=�6�   .+�5o      �� -   5         ��Name$ MarketDataBtn��5o      $��60��60   
������ $��0q�6V   #���   6�   .��5p      �� $��Images/marketData.png��6   5q      �� 5r      $��Market Data��6   
�������6/   5s      �� 5t      $��2��6   $��0,0,10,0q�6)   $ Vertical=�6;   $
��auto��6R   $
��auto��6`   $��Center=�6m   $��Center=�6�   .+�5u      �� -   5        $ StrategyBtn��5u      $��60��6.   
������ $��0q�6T   #���   6�   ��5v      �� 5w      $		 0,1&�6.   $	
 0,0&�6=   .��5x      � $ 	#FFEEEEEE��6+   $ 1Q�6=   � 5y      $ 	LightGray��6+   $ 0.659Q�6=   � 5z      $ 	#FFCFCFCF��6+   $ 0.357Q�6=   � 5{      $ 
WhiteSmoke��6+   5|      .��5~      �� $��Images/strategy.png��6   5      �� 5�      $��Strategy��6   $��7,0,0,0q�6,   
�������6>   5�      �� 5�      $��3��6   $��0,0,10,0q�6)   $ Vertical=�6;   $
��auto��6R   $
��auto��6`   $��Center=�6m   $��Center=�6�   .+�5�      �� -   5        $ TradeBtn��5�      $��60��6+   
������ $��0q�6Q   #���   6|   .��5�      �� $��Images/trade.png��6   5�      �� 5�      $��Trades��6   $��10,0,0,0q�6*   
�������6<   5�      �� 5�      $��4��6   $��0,0,10,0q�6)   $ Vertical=�6;   $
��auto��6R   $
��auto��6`   $��Center=�6m   $��Center=�6�   .+�5�      �� -   5        $ PortfolioBtn��5�      $��60��6/   
������ $��0q�6U   #���   6�   .��5�      �� $��Images/portfolio.png��6   5�      �� 5�      $��	Portfolio��6   $��7,0,0,0q�6-   
�������6>   5�      5�       � 5�   
   -   5          �Name$ ContentFrame��5�      $��1��6&   $��0��63     � NavigationUIVisibility$ Hidden=�6C   $��0,0,0,10q�6c   $��0q�6u   5�      5�      