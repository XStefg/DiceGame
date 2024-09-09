using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTeck.DiceGame
{
  /// <summary>
  /// Interaction logic for DiceControl.xaml
  /// </summary>
  public partial class DiceControl : UserControl
  {
    public DiceControl()
    {
      InitializeComponent();
    }

    public static readonly DependencyProperty ValueProperty =
      DependencyProperty.Register( nameof(Value), typeof( int ), typeof( DiceControl ),
                                   new FrameworkPropertyMetadata( defaultValue: 0, propertyChangedCallback: ValueChangedCallback ) );

    private static void ValueChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
      if ( d is DiceControl diceControl )
      {
        diceControl.ValueChanged();
      }
    }

    private void ValueChanged()
    {
      Icon1.Visibility = Visibility.Collapsed;
      Icon2.Visibility = Visibility.Collapsed;
      Icon3.Visibility = Visibility.Collapsed;
      Icon4.Visibility = Visibility.Collapsed;
      Icon5.Visibility = Visibility.Collapsed;
      Icon6.Visibility = Visibility.Collapsed;

      switch ( Value )
      {
        case 1:
          Icon1.Visibility = Visibility.Visible;
          break;
        case 2:
          Icon2.Visibility = Visibility.Visible;
          break;
        case 3:
          Icon3.Visibility = Visibility.Visible;
          break;
        case 4:
          Icon4.Visibility = Visibility.Visible;
          break;
        case 5:
          Icon5.Visibility = Visibility.Visible;
          break;
        case 6:
          Icon6.Visibility = Visibility.Visible;
          break;
      }
    }

    public int Value
    {
      get => (int)GetValue( ValueProperty );
      set => SetValue( ValueProperty, value );
    }
  }
}