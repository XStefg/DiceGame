using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddyfi.Core;
using GTeck.DicePer12;

namespace GTeck.DiceGame;

internal class MainWindowViewModel : BaseViewModel
{
  public MainWindowViewModel()
  {
    RollRandomCommand = Command.CreateFrom( RollRandomCommandHandler );

    _subscriber = this.GetSubscriberBuilder()
                      .AddSubscription( p => p.SelectedDice1, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice2, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice3, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice4, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice5, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice6, UpdateRoll )
                      .SubscribeAndInvokeAll();
  }

  public SimpleCommand RollRandomCommand { get; set; }

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

  public int SelectedDice6
  {
    get => _selectedDice6;
    set => SetProperty( ref _selectedDice6, value );
  }

  public Roll? Roll
  {
    get => _roll;
    set => SetProperty( ref _roll, value );
  }

  protected override void Dispose( bool disposing )
  {
    _subscriber.Dispose();
    base.Dispose( disposing );
  }

  private void UpdateRoll()
  {
    //if ( SelectedDice1 == 0 || SelectedDice2 == 0 || SelectedDice3 == 0 || SelectedDice4 == 0 || SelectedDice5 == 0 || SelectedDice6 == 0 )
    //{
    //  Roll = null;
    //  return;
    //}

    Roll = new Roll( SelectedDice1 + 1, SelectedDice2 + 1, SelectedDice3 + 1, SelectedDice4 + 1, SelectedDice5 + 1, SelectedDice6 + 1 );

    RollSet[] rollSetsRaw = RollUtil.AppendRemaingRoll( RollUtil.EnumRollSet( Roll ) ).OrderBy( s => s.Value ).ToArray();
    RollSet[] rollSets    = RollUtil.RemoveDuplicate( rollSetsRaw ).ToArray();
  }

  private void RollRandomCommandHandler()
  {
    Roll newRoll = Roll.RollDices( 6 );
    SelectedDice1 = newRoll.Dices[0].Value - 1;
    SelectedDice2 = newRoll.Dices[1].Value - 1;
    SelectedDice3 = newRoll.Dices[2].Value - 1;
    SelectedDice4 = newRoll.Dices[3].Value - 1;
    SelectedDice5 = newRoll.Dices[4].Value - 1;
    SelectedDice6 = newRoll.Dices[5].Value - 1;
  }

  private int _selectedDice1;
  private int _selectedDice2;
  private int _selectedDice3;
  private int _selectedDice4;
  private int _selectedDice5;
  private int _selectedDice6;

  private Roll?       _roll;
  private IDisposable _subscriber;
}