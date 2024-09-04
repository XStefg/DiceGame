using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddyfi.Core;

namespace GTeck.DiceGame;
internal class MainWindowViewModel : BaseViewModel
{
  public int SelectedDice1
  {
    get => _selectedDice1;
    set => SetProperty( ref _selectedDice1, value );
  }

  public int SelectedDice2
  {
    get => _selectedDice2;
    set => SetProperty( ref _selectedDice2, value );
  }

  public int SelectedDice3
  {
    get => _selectedDice3;
    set => SetProperty( ref _selectedDice3, value );
  }

  public int SelectedDice4
  {
    get => _selectedDice4;
    set => SetProperty( ref _selectedDice4, value );
  }

  public int SelectedDice5
  {
    get => _selectedDice5;
    set => SetProperty( ref _selectedDice5, value );
  }

  private int _selectedDice1;
  private int _selectedDice2;
  private int _selectedDice3;
  private int _selectedDice4;
  private int _selectedDice5;

}
