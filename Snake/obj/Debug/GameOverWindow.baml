   M S B A M L   `   `   `  ����@  <Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SnakeSnake  5   	       Snake.GameOverWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Snake     5   	   -   5          � Title$   ИГРА ОКОНЧЕНА!��5   	    � 
ResizeMode$ NoResize=�6     � Icon$ icon.png��66   $	��249��6F    � SizeToContent$ Height=�6R   .��5	      � .+�5
   
   �� .+�5      �� -   5         ��Name$ 
ScoreLabel��5      $	��Top=�6(   $��Center=�6@   .��6]   	Счет:5      �� 5      ���#F67280 �� TextAlignment$ Center=�6-   $��Center=�6D   $
��Wrap=�6a   $��36��6u   $��
10 0 10 10q�6�   $��13(�6�   qoИгра завершена! Запишите свое имя, чтобы сохранить результат5      �� 5      -   5         ��Name$ PlayerNameTextBox��5      $��	10 0 10 0q�61   $��2q�6D    J� IsHitTestVisible . .��6i   
Игрок5      ]� 5      .+�5      �� -   5         ��Name$ 
SaveButton��5      $
��Left=�6-   $��10q�6C   ���#54A593
�������$	��102��6w   .��6�   Сохранить6�   �� 5      -   5        $ CancelButton��5      $
��Left=�6/   $��10q�6E   ���#F67280
�������.��6�   Отмена6�   5      5      5      5      