using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GTeck.DicePer12;

public record Roll( ImmutableArray<Dice> Dices )
{
  public Roll( params Dice[] dices ) : this( dices.ToImmutableArray() )
  {
  }

  public static Roll RollDices( int numberOfDices, int nbOfSide = 6 )
  {
    Random random = new();
    Dice[] dices  = Enumerable.Range( 0, numberOfDices ).Select( _ => new Dice( random.Next( 1, nbOfSide + 1 ) ) ).ToArray();
    return new Roll( dices );
  }

  public Roll SubRoll( int index )
  {
    return new Roll( Dices.RemoveAt( index ) );
  }
}