   M S B A M L   `   `   `  ����@  <Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SnakeSnake  5   	       Snake.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Snake     5   	   -   5          �Name$  
GameWindow��5   	    � Title$ Змейка��5   	   $	��459��6    � Icon$ icon.png��6%    � 
ResizeMode$ NoResize=�65    � SizeToContent$ WidthAndHeight=�6f   .��5	      ]� -   5         ]�Name$ MainGrid��5	      $��Center=�6"   $��5q�6?   .+�5
   
   �� -   5         ��Name$ 
ScoreStack��5
      $��5q�6)   $	��192��64   .+�5      �� ���#F67280$��Center=�6)   $��21(�6F   .��6T   !Таблица рекордов6f   �� 5      -   5         ��Name$ 	ScoreList��5      $��Stretch=�6)   $��Disabled=�6G   $	��389��6}   5      5      �� 5   
   -   5         ��Name$ 
GameCanvas��5      $	��400��6%   $	��400��61   ���#ACFADF$��5q�6S   5      �� 5   
   $	��129��6   $��5q�6!   .+�5      �� -   5        	 ��Name$	 StartButton��5      $��
Старт��6*   $��20(�6:   $��60��6H   $��5 14 5 5q�6T   6f   �� 5      -   5        
 ��Name$
 
LevelCombo��5      $��5q�6+   .@�5      ��  �� 
IsSelected . .��61   1 уровень6<   �� 5      .��6   2 уровень6*   �� 5      .��6   3 уровень6*   �� 5      .��6   4 уровень6*   5      �� 5      -   5         ��Name$ 
ScoreLabel��5      $��5q�6(   ���#F67280$��20(�6H   .��6V   Счет: 05      �� 5       -	   5        $	 
HelpButton��5       $��20(�6)   $��46��67   $��Об игре��6C   $��	5,210,5,5q�6U   6h   5#      5%      5&      