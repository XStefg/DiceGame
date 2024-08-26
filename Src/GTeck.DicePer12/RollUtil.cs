using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GTeck.DicePer12;

public record RollSet( ImmutableArray<DiceSet> DiceSet, Roll RemaingDices )
{
  public RollSet( DiceSet[] diceSet, Roll remainingDices ) : this( diceSet.ToImmutableArray(), remainingDices )
  {
  }

  public int Value => DiceSet.First().Dices.Sum( s => s.Value );
}

public static class RollUtil
{
  public static IEnumerable<RollSet> EnumDiceSet( Roll roll )
  {
    for ( int index = 0; index < roll.Dices.Length; index++ )
    {
      Dice firstDice     = roll.Dices[index];
      Roll remainingRoll = roll.SubRoll( index );

      yield return new( new[] { new DiceSet( firstDice ) }, remainingRoll );

      for ( int subIndex = index + 1; subIndex < remainingRoll.Dices.Length; subIndex++ )
      {
        Dice secondDice = roll.Dices[subIndex];

        DiceSet diceSet = new( firstDice, secondDice );
        if ( diceSet.Value > 6 )
        {
          yield return diceSet;
        }
      }
    }
  }
}