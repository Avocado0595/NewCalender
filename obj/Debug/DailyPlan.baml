   M S B A M L   `   `   `  ����E  AMyCalender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyCalender
MyCalender  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	          MyCalender.DailyPlan   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyCalender     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   3System&clr-namespace:System;assembly=mscorlib  5	        � Title$  	DailyPlan��5   	   $��562.856��6   $	��800��6,      MaterialDesignBody#���   5      $��Regular"�5      $��14(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      6   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6   .+�5      o�  o� 
IsMainMenu . $��30��6%   $��0q�61   .@�5      m� -   5        $��Thêm việc��5   9   $��2q�6L   $��Center=�6X   $��3 5 10 0q�6s   5       m� 5!      -   5        $��Hôm nay��5!   7   $��2q�6I   $��Center=�6U   $��3 5 10 0q�6p   5"      5#      �� 5'      $��Right=�6    �� Orientation$ 
Horizontal=�65   $��	0 0 150 0q�6N   .+�5(      �� -   5        $��	ckxFilter��5(   3   .��6D   Lọc6I   �� 5)      -   5        $��	cbxFilter��5)   L    �� Text$	 adf��6   $��5 0 5 3q�6]   $��4q�6n   $	��100��6z   {�5*      �� *	IsChecked+ �� ElementName$ 	ckxFilter��5+      5,      �� 5/      $��Right=�6   $ 
Horizontal=�65   $��0 0 10 0q�6N   .+�50      �� -   5        $��ckxSort��50      .��6*   Sắp xếp63   '  !MaterialDesignThemes.Wpf.PopupBox  51      $��5,0,5,0q�6*      	StaysOpen . {�52      �� *	IsChecked+$ ckxSort��	   PopupContent.	 53      R� $	��220��6   .@�54      Q� $��Thời gian bắt đầu��6'   .@�55      Q� -   5        $��Tăng dần��58   7   59      Q� 5:      -   5        $��Giảm dần��5=   7   5>      5?      Q� 5A      $��Thời gian kết thúc��6'   .@�5B      Q� -   5        $��Tăng dần��5E   7   5F      Q� 5G      -	   5        $��Giảm dần��5J   7   5K      5L      5M      5N      5O      5P      � 5R   
   $��1��6   .+�5S      �� 6   5T      � 5V   
   $��2��6   
 � ColumnDefinitions
 5W      �� 5X      6%   �� 5Y      $��2* �6#   60   �� 5Z      6%   .+�5]      � $��0��6   .+�5^      �� -
   5        $��
btnPrevday��5^   8   $��Hôm trước��6   $��5q�6-   6e   5_      � 5`      $��1��6   .+�5a      (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpSelectionDate��5a   X   $��30��6r   $��0q�6~   $��0q�6�   $	��200��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Chọn ngày��6�   5b      � 5c      $��2��6   .+�5d      �� -   5        $��
btnNextday��5d   6   $��Hôm sau��6   $��5q�6+   6c   5e      5f      �� 5h   
   $��3��6   $
��Auto=�6$   .��5i      �� -   5         ��Name$ 	spnlPlans��5i      5j      5k      � 5m   
   $��4��6   
 5n      �� 5o      $
��166* �6#   62   �� 5p      $
��627* �6#   62   .+�5r      �� $��0��6   .+�5s      �� $��5q�6   $��Center=�6'   Tiến độ6M   �� 5t      -   5        $��tbxProgress��5t   D   $��5q�6   $��Center=�6'   6Y   5u      � 5v      -   5        $��prbProgress��5v      $��1��6-   $	��6px��6=   $��5q�6J   6W   5w      5x      5y      