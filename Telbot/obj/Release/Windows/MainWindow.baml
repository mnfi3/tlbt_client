   M S B A M L   `   `   `  ���� H  DAutoMemberBot, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Telbot.ItemsTelbot.Items      Telbot.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     .controlclr-namespace:Telbot.Items     -     � Icon$  ../ic_app.ico�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � Title$ AutoMemberBot��$	��600��$
��1800��$8��2/AutoMemberBot;component/Fonts/vazir/#Vazir FD-WOL)�.��]� .+�o� $	��Top=�$��RightToLeft=�$��22��$��Center=�.@�m� -   $��
txt_mobile��$��
0000000000��$
��Bold"�$y�Visible=�$��0,0,0,0q�m� $��حساب کاربری��.@�m� -   $��txt_app_logout��$)��#خروچ از حساب کاربری��m� -   $��txt_telegram_logout��$7��1 خروچ از حساب کاربری تلگرام��$y�	Collapsed=�m� -   $��txt_add_account��$-��'+ افزودن اکانت  تلگرام��m� -   $��	txt_about��$��درباره ما��m� -   $��txt_exit��$��خروج��m� $��خرید اشتراک��$y�	Collapsed=�� $��RightToLeft=� � ColumnDefinitions �� $��1* ��� $��2* ��� $��1* �.+�� $��0��.+��� -   $��btn_refresh_contacts��$��Right=�$	��Top=�   BtnLarge#��[  $!��دریافت مخاطبین��$��25��$��90��$��0,5,5,0q��� $��لیست مخاطبین��$
��Left=�$
��Left=�$	��Top=�$��0q��� -	    ��Name$ lst_contacts��$��0,40,0,0q�$��Stretch=�$��Stretch=�$��0q����� .R�A� $	��Top=�$��Stretch=� A� Columns$ 1��!   Telbot.Items.CircleProgress  -
     Name$ prg_contact��$��0 0 0 0q�$��Center=�$��Center=�$y�	Collapsed=�$	��100��$	��100���� $��0��$��5��$��Stretch=�� $��1�� � RowDefinitions �� $	��100 ��� $��* ��� $��40 �.+�� $��0�� �� $��* ��� $��* �.+�� $��0��.+��� 	 �� Orientation$	 Vertical=�.+��� $	 
Horizontal=�$	��Top=�.+��� $	 Vertical=�.+��� -   
 ��Name$
 
btn_browse��$
��Left=�#��[  $��120px��$
��30px��$��0,5,5,0q�.��'%انتخاب لیست شماره ها�� -   $
 btn_add_to_database��$
��Left=�$	��Top=�#��[  $��120px��$
��30px��$��5,5,0,0q�.��)'افزودن به پایگاه داده�� -    ��Name$ lbl_filepath��$
��Left=��� $��0,0,0,0q�$	 
Horizontal=�.+��� $��لیست  شماره ها��$
��Left=�$
��Left=�$��0q�$��0,5,0,0q��� $��10(�$
��Left=� �� TextAlignment$ Right=��� -   $��
btn_remove�����	#FFD83838#��[  $��حذف��$��25��$��90��$��70,0,0,0q�� $��1��.+��� $	 Vertical=�.+��� $	 
Horizontal=�$��0,5,0,0q�.+��� $��
فیلتر���� .+��� $��10(�$
��Left=�$ Right=�	نام:�� -    ��Name$ inp_name��  InputBox#��[ $��25��$	��Top=�$��90��$��12(�$��Center=��� $��3,0,0,0q�.+��� $��10(�$
��Left=�$ Right=�نام خانوادگی:�� -   $ inp_lastname��#��[ $��25��$	��Top=�$��90��$��12(�$��Center=��� $��3,0,0,0q�.+��� $��10(�$
��Left=�$ Right=�شماره تلفن :�� -   $ 
inp_number��#��[ $��25��$	��Top=�$��90��$��12(��� $	 
Horizontal=�$��30,5,0,0q�.+��� $��3,0,0,0q�.+��� $��10(�$
��Left=�$ Right=�از ردیف :�� -   $ inp_number_from��#��[ $��25��$	��Top=�$��90��$��12(��� $��3,0,0,0q�.+��� $��10(�$
��Left=�$ Right=�تا ردیف :�� -   $ inp_number_to��#��[ $��25��$	��Top=�$��90��$��12(��� $��3,0,0,0q�.+��� $��10(�$
��Left=�$ Right=��� -   $��
btn_search��#��[  $��
جستجو��$��25��$��90��$��0,0,0,0q�� $��1�� �� $
��15px ��� $��* �.+�� $��0��$��Bottom=����#e8e8e8 �� $��40 ��� $��* ��� $��* ��� $��* ��� $��60 �.+�� $��0��.+��� $��Center=�$��Center=�
ردیف� $��1��.+��� $��Center=�$��Center=�نام� $��2��.+��� $��Center=�$��Center=�نام خانوادگی� $��3��.+��� $��Center=�$��Center=�شماره تلفن� $��4��.+��� $	 
Horizontal=�.+��� $��	-13,0,0,0q�همه�� -    ��Name$ chk_all��$��Center=�$��Center=�$��LeftToRight=�$��2,0,0,0q�� $��1��.+��� -   $ lst_numbers��$��0,0,0,0q�$��Stretch=�$��Stretch=�$��0q����� .R�A� $	��Top=�$��Stretch=�$ 1��  -   $ prg_numbers��$��0 0 0 0q�$��Center=�$��Center=�$y�	Collapsed=�$	��100��$	��100��� $��2��.+��� -   $
 btn_add_member_to_group��#��[  $��Stretch=�$��Center=�$��6,3,6,3q�$4��.افزودن لیست موجود به گروه��$
��Auto���� $��1��$��5��$��Stretch=�� $��2��.+��� -   $��btn_refresh_channels��$��Right=�$	��Top=�#��[  $��دریافت چت ها��$��25��$��90��$��0,5,5,0q��� $/��)لیست گروه ها و کانال ها��$
��Left=�$
��Left=�$	��Top=�$��0q��� -   $ 
lst_groups��$��0,40,0,0q�$��Stretch=�$��Stretch=�$��0q����� .R�A� $	��Top=�$��Stretch=�$ 1��  -   $ prg_channel��$��0 0 0 0q�$��Center=�$��Center=�$y�	Collapsed=�$	��100��$	��100��