using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GTeck.DicePer12;

[DebuggerDisplay("{OutputDebug}")]
public sealed record Roll( ImmutableArray<Dice> Dices )
{
  public Roll( params Dice[] dices ) : this( dices.ToImmutableArray() )
  {
  }

  public bool Equals( Roll? roll )
  {
    if ( roll is not null )
    {
      return Dices.SequenceEqual( roll.Dices);
    }

    return false;
  }

  public override int GetHashCode()
  {
    int hash = 17;
    foreach ( Dice current in Dices )
    {
      hash = HashCode.Combine( hash, current );
    }

    return hash;
  }

  public static Roll RollDices( int numberOfDices, int nbOfSide = 6 )
  {
    Random random = new();
    Dice[] dices  = Enumerable.Range( 0, numberOfDices ).Select( _ => new Dice( random.Next( 1, nbOfSide + 1 ) ) ).ToArray();
    return new Roll( dices );
  }

  public Roll RemoveDice( int index )
  {
    return new Roll( Dices.RemoveAt( index ) );
  }

  public string OutputDebug => string.Join( ",", Dices.Select( d => d.Value ) );
}