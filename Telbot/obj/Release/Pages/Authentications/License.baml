   M S B A M L   `   `   `  ����   AutoMemberBot*    $Telbot.Pages.Authentications.License   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$   �� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value   InputBox 
 bounce	 �� TargetProperty
 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%�  '��       &	  �    &	 @    �� " ��.���  � ��$
 40px����  � ��$ 100px����  � ��$ 15(���  � ��$ Center=���  � ��$ Center=���  � ��
 �   ���  � ��
 �������  � ��$ 5q���  � ��
 �  ����  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �	#FF363535��  �@�Cursor$
 Hand���� " ��.���  � ��$ 20(���  � ��$ 200px����  � ��$
 50px����  � ��$ Center=���  � ��$ LeftToRight=���  � �� �	#FF060606�� .��U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$
 PulseBox��.\�  $ 0:0:0.25��$	 0.9Q�  $ 0:0:0.35��$ 1Q�U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$
 PulseBox��.\�  $ 0:0:0.25��$	 0.9Q�  $ 0:0:0.35��$ 1Q�.��� 
������� � RowDefinitions �� $��* ��� $��2* �.+�� $��0��$��	0,55,0,55q�.+��� -    ��Name$ PulseBox��$��../../Images/tick.png��z�V� .~��� �� �� T� � $��1��.+��� $	��top=�.+��� -   $��txt_message��$��RightToLeft=�$��Center=�$��
36,10,36,0q��� $(�"در حال بررسی سرور...���� -    ��Name$ btn_check_again��$��دوباره سعی کن��$��0,20,0,0q�$y�Visible=� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� #��[ 