   M S B A M L   `   `   `  ����  AutoMemberBot'    !Telbot.Dialogs.EnterConfiguration   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$   ��5   	   $	��300��5   	   $	��400��6    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   $7��1/AddMemberBot;component/Fonts/vazir/#Vazir FD-WOL)�5   	    � Icon$" /Telbot;component/ic_app.ico��6H    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value   InputBox 	 Pulse g� RepeatBehavior �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%�  '��       &	  �    &	 �    �� 5
   
   " ��6   .�5      ��  � ��$
 40px��6'   �� 5       � ��$ 100px��6&   �� 5       � ��$ 12(�6)   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��
 �   �6+   �� 5       � ��
 �����6+   �� 5       � ��$ 5q�60   �� 5       � ��
 �  ��6,   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � �� �	#FF36353563   �� 5        �@�Cursor$
 Hand��6/   5!      5$      �� 5%   
   " ��6!   .�5&      ��  � ��$ 10(�6)   �� 5'       � ��$ 200px��6&   �� 5(       � ��$
 25px��6'   �� 5)       � ��$ Center=�69   �� 5*       � ��$ LeftToRight=�6.   �� 5+       � �� �	#FF0606066,   5,      �� 5.   
   $ Forever��6#   .��5/      U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6,   $ PulseBox��6�   .\�50        $ 0:0:1��6'   $
 1.15Q�67     51      $ 0:0:2��6'   $ 1Q�67   52      U� 53      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6,   $ PulseBox��6�   .\�54        $ 0:0:1��6'   $
 1.15Q�67     55      $ 0:0:2��6'   $ 1Q�67   56      57      59      .��5:      � .+�5;   
   �� .+�5<      �� $��RightToLeft=�6   A?توجه: باید api_id و api_hash حساب وارد شود.5>      �� 5?      $��RightToLeft=�6   $"برای دریافت به این �� 5B      -   5        $��https://my.telegram.org?�5B      .�6Z   
لینک6`    مراجعه کنید5D      �� 5E      $��api id:��6   $��47,20,46.6,0q�6&   �� 5F      -   5         ��Name$ 
txt_api_id��5F      #��[  6*   $	��300��6N   6\   �� 5G      $��	api hash:��6   $��47,0,46.6,0q�6(   �� 5H      -   5        $ txt_api_hash��5H      #��[  6,   $	��300��6P   6^   �� 5I      -   5        $��اعمال تغییرات��5I      $��	0,30,15,0q�6-   6`   5J      5K      5L      