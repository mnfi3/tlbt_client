   M S B A M L   `   `   `  ����   AutoMemberBot.    (Telbot.Pages.Authentications.EnterNumber   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� 
FontFamily$1  +/Telbot;component/Fonts/vazir/#Vazir FD-WOL)� N� Title$  �� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property	 P� Value   InputBox 	 Pulse
 g� RepeatBehavior �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value 
 bounce��%  '��       &	  �    &	 @    &	 �    �� " ��.���  � ��$
 40px����  � ��$ 100px����  � ��$ 15(���  � ��$ Center=���  � ��$ Center=���  � ��
 �   ���  � ��
 �������  � ��$ 5q���  � ��
 �  ����  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver	 . . ���  � �� �	#FF363535��  �@�Cursor$
 Hand���� " ��.���  � ��$ 20(���  � ��$ 200px����  � ��$
 50px����  � ��$ Center=���  � ��$ LeftToRight=���  � �� �	#FF060606�� $
 Forever��.��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$ PulseBox��.\�  $ 0:0:1��$
 1.15Q�  $ 0:0:2��$ 1Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$ PulseBox��.\�  $ 0:0:1��$
 1.15Q�  $ 0:0:2��$ 1Q��� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$ PulseBox��.\�  $ 0:0:0.25��$	 0.9Q�  $ 0:0:0.35��$ 1Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$ PulseBox��.\�  $ 0:0:0.25��$	 0.9Q�  $ 0:0:0.35��$ 1Q�.��� $��Center=�
������� � RowDefinitions �� $��4* ��� $��5* �.+�� $��0��$
��0,80q�.+��� -    ��Name$ PulseBox��$!��../../images/smartphone.png�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T� � $��1��.+���  �� Orientation$ Vertical=�.+��� -   $��txt_accounts_count��$��RightToLeft=����#ff0000$��12(�$��0,0,0,10q��� $��RightToLeft=�$��12(�$��0,0,0,10q�$
��Wrap=���اگر میخواهید حساب تلگرام اضافه کنید شماره تلفن تلگرام را برای دریافت کد وارد کنید و دکمه ارسال کد را کلیک کنید�� $ 
Horizontal=�.+��� -    ��Name$ country_code��
���   �$
��Left=�$
��Left=�$��Center=�
���  ��$��20(��� -   $��
txt_mobile��#��[  $	��300�� �� 	MaxLength$ 10���� $ 
Horizontal=�.+��� -   $��ارسال کد��$��	0,30,15,0q��� -   $#��تنظیمات apikey,apihash��$��	15,30,0,0q�$	��200���� $ Vertical=�$��Center=�.+��� -   $��btn_open_main_window��$'��!برو به داخل برنامه��$	��300��$��Center=�$��	0,40,0,10q� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� #��[ 