   M S B A M L   `   `   `  ���� I  ERevitAddIn1, Version=2021.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RevitAddIn1.ViewModelsRevitAddIn1.ViewModels  '    !RevitAddIn1.Views.RevitAddIn1View   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :	viewModel$clr-namespace:RevitAddIn1.ViewModels       � 
ResizeMode$  	CanResize=� � WindowStartupLocation$ Manual=�
 � Top$ 20�� � Title$ RevitAddIn1��$	��450��$	��800��$	��700��$	��400��$��SegoeUI)� � 	Resources  �� MergedDictionaries �� Source��  �� $ Styles.xaml?�%    .���  � ColumnDefinitions �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��20 ��� $��40 ��� $��20 ��� $��20 ��� $��* ��� $��20 ��� $��40 ��� $��20 �.+��� -   	 ��Name$	 StartingTextBlock��
�������$��0q�$��1��$��4��$��1��$��5��
 K� ZIndex$

 9999��$y�Hidden=�._��� $0��*Select two elements to transfer parameters��$
��Wrap=�
�������$��40(� �� TextAlignment$ Center=�$��Center=�$��Center=�$y�Visible=��� $��ParameterTransfer��$��1��$��1��$��25(�$��Italic$�$��0,5,0,0q�
���/kU��� -    ��Name$ 
element1Id��   
IdBoxStyle#��[  $��3��$��1����   AnnotationForTextBox#��[ $��3��$��1��$	��ID:���� -    ��Name$ selectNewId1��$��3��$��1��  EditIdButton#��[ $��0,0,10,0q� � ToolTip$ Select another element���� #��[ $	��ID:��$��5��$��1���� -   $ 
element2Id��#��[  $��	45,0,45,0q�$��5��$��1���� -   $ selectNewId2��#��[ $��0,0,10,0q�$ Select another element��$��5��$��1���� -   $	 ParameterNameWrapper��$��1��$��4��$��0q�._��� -    ��Name$ ParameterNames��  ParameterListBoxStyle#��[ $��0q�����  �� Path$ ActualWidth� �� ElementName$ RightListWrapping������ $ ActualHeight�$ RightListWrapping������ .R���  �� Orientation$ 
Horizontal=�.+��� -   $
��Left=�$��Center=�$��14(�
�����p����� �� -	   $	 Element1Panel��$��3��$��1��$��4��  RoundCornerBorderStyle#��[ $
 -1���� -
   $	 LeftListWrapping��$��3��$��4��$��0q�._��� -   $ Element1Parameters��#��[ $��Hidden=����� $ ActualWidth�$ LeftListWrapping������ $ ActualHeight�$ LeftListWrapping������ .R��� $��14(�
���   ����� �� -   $	 Element2Panel��$��5��$��1��$��4��#��[ $
 -1���� -   $	 RightListWrapping��$��5��$��4��$��0q�._��� -   $ Element2Parameters��#��[ $��Hidden=����� $ ActualWidth�$ RightListWrapping������ $ ActualHeight�$ RightListWrapping������ .R��� $��14(�
���   ����� �� -   $ TransferParameters1to2��$��3��$��6��$��Transfer there ->���� -   $ TransferParameters2to1��$��5��$��6��$��<- Transfer there��