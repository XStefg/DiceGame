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
  #region CTOR

  public MainWindowViewModel()
  {
    RollRandomCommand   = Command.CreateFrom( RollRandomCommandHandler );
    PlayThisRollCommand = Command.CreateFrom<RollSet>( PlayThisRollCommandHandler );
    RollThisHandCommand = Command.CreateFrom<RollSet>( RollThisHandCommandHandler );

    _subscriber = this.GetSubscriberBuilder()
                      .AddSubscription( p => p.SelectedDice1, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice2, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice3, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice4, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice5, UpdateRoll )
                      .AddSubscription( p => p.SelectedDice6, UpdateRoll )
                      .SubscribeAndInvokeAll();
  }

  #endregion

  #region Public Properties

  public SimpleCommand<RollSet> RollThisHandCommand { get; set; }

  public SimpleCommand<RollSet> PlayThisRollCommand { get; set; }

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

  public Roll InitialRoll
  {
    get => _initialRoll;
    set => SetProperty( ref _initialRoll, value );
  }

  public Roll HandRoll
  {
    get => _handRoll;
    set => SetProperty( ref _handRoll, value );
  }

  public ViewableCollection<RollSet> RollSets
  {
    get => _rollSets;
    set => SetProperty( ref _rollSets, value );
  }

  public bool PlayingHand
  {
    get => _playingHand;
    set => SetProperty( ref _playingHand, value );
  }

  #endregion

  #region Disposable Overrides

  protected override void Dispose( bool disposing )
  {
    _subscriber.Dispose();
    base.Dispose( disposing );
  }

  #endregion

  #region Private Methods

  private void UpdateRoll()
  {
    if ( _suspendUpdateRoll )
    {
      return;
    }

    InitialRoll = new Roll( SelectedDice1, SelectedDice2, SelectedDice3, SelectedDice4, SelectedDice5, SelectedDice6 );

    RollSets = new ViewableCollection<RollSet>( InitialRoll.EnumRollSet().AppendRemaingRoll().OrderBy( s => s.Value ).RemoveDuplicate() );
  }

  #endregion

  #region Command Handler

  private void RollRandomCommandHandler()
  {
    _suspendUpdateRoll = true;

    Roll newRoll = Roll.RollDices( numberOfDices: 6 );
    SelectedDice1 = newRoll.Dices[0].Value;
    SelectedDice2 = newRoll.Dices[1].Value;
    SelectedDice3 = newRoll.Dices[2].Value;
    SelectedDice4 = newRoll.Dices[3].Value;
    SelectedDice5 = newRoll.Dices[4].Value;
    SelectedDice6 = newRoll.Dices[5].Value;

    _suspendUpdateRoll = false;

    HandRoll = new Roll();

    UpdateRoll();

    PlayingHand = false;
  }

  private void PlayThisRollCommandHandler( RollSet obj )
  {
    HandRoll = obj.RemaingRoll;
    RollSets.Clear();
    RollSets.Add( obj );
    PlayingHand = true;
  }

  private void RollThisHandCommandHandler( RollSet obj )
  {
    Roll newRoll = Roll.RollDices( HandRoll.Dices.Length != 0 ? HandRoll.Dices.Length : 6 );

    RollSet updated = RollSets.First().AddMatchingFrom( newRoll, out _ );

    RollSets.Clear();
    RollSets.Add( updated );

    HandRoll = updated.RemaingRoll;
  }

  #endregion

  #region Private Variables

  private int _selectedDice1;
  private int _selectedDice2;
  private int _selectedDice3;
  private int _selectedDice4;
  private int _selectedDice5;
  private int _selectedDice6;

  private bool _playingHand;
  private Roll _initialRoll = DicePer12.Roll.RollDices( 6 );
  private Roll _handRoll    = new Roll();

  private ViewableCollection<RollSet> _rollSets = new();

  private IDisposable _subscriber;

  private bool _suspendUpdateRoll;

  #endregion
}