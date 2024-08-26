using System.Collections.Generic;
using System.Linq;

namespace GTeck.DicePer12;

public static class RollUtil
{
  public static IEnumerable<RollSet> EnumRollSet( Roll roll )
  {
    for ( int index = 0; index < roll.Dices.Length; index++ )
    {
      Dice firstDice     = roll.Dices[index];
      Roll remainingRoll = roll.RemoveDice( index );

      yield return new( new[] { new DiceSet( firstDice ) }, remainingRoll );

      for ( int subIndex = index; subIndex < remainingRoll.Dices.Length; subIndex++ )
      {
        Dice secondDice = remainingRoll.Dices[subIndex];

        Roll remainingSubRoll = remainingRoll.RemoveDice( subIndex );

        DiceSet diceSet = new( firstDice, secondDice );
        if ( diceSet.Value > 6 )
        {
          yield return new( new[] { diceSet }, remainingSubRoll );
        }
      }
    }
  }

  public static IEnumerable<RollSet> AppendRemaingRoll( IEnumerable<RollSet> currentRollSet )
  {
    foreach ( RollSet current in currentRollSet )
    {
      List<DiceSet> newDiceSet  = new( current.DiceSet );
      Roll          remaingRoll = current.RemaingRoll;
      bool          hasMatching = false;
      do
      {
        IEnumerable<RollSet> rollsetFromRemaining = EnumRollSet( remaingRoll );
        RollSet              ?mathchingSet         = rollsetFromRemaining.FirstOrDefault( s => s.Value == current.Value );
        if ( mathchingSet != null )
        {
          remaingRoll = mathchingSet.RemaingRoll;
          newDiceSet.AddRange( mathchingSet.DiceSet );
          hasMatching = true;
        }
        else
        {
          hasMatching = false;
        }
      } while ( hasMatching );

      yield return new( newDiceSet.ToArray(), remaingRoll );
    }
  }
}