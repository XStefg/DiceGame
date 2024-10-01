using System.Collections.Generic;
using System.Linq;

namespace GTeck.DicePer12;

public static class RollUtil
{
  public static IEnumerable<RollSet> EnumRollSet( this Roll roll )
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

  public static IEnumerable<RollSet> AppendRemaingRoll( this IEnumerable<RollSet> currentRollSet )
  {
    foreach ( RollSet current in currentRollSet )
    {
      yield return current.AddMatchingFrom( current.RemaingRoll, out _ );
    }
  }

  public static RollSet AddMatchingFrom( this RollSet current, Roll remaingRoll, out bool hasMatching )
  {
    List<DiceSet> newDiceSet  = new( current.DiceSet );
    hasMatching = false;
    do
    {
      IEnumerable<RollSet> rollsetFromRemaining = EnumRollSet( remaingRoll );

      RollSet? mathchingSet = rollsetFromRemaining.FirstOrDefault( s => s.Value == current.Value );
      if ( mathchingSet != null )
      {
        remaingRoll = mathchingSet.RemaingRoll;
        newDiceSet.AddRange( mathchingSet.DiceSet );
        hasMatching = true;
      }
      else
      {
        break;
      }
    } while ( true );

    return new RollSet( newDiceSet.ToArray(), remaingRoll );
  }

  public static IEnumerable<RollSet> RemoveDuplicate( this IEnumerable<RollSet> currentRollSet )
  {
    return currentRollSet.Distinct();
  }
}