using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GTeck.DicePer12;

[DebuggerDisplay( "{OutputDebug}" )]
public sealed record RollSet( ImmutableArray<DiceSet> DiceSet, Roll RemaingRoll )
{
  public RollSet( DiceSet[] diceSet, Roll remainingDices ) : this( Sort( diceSet ).ToImmutableArray(), remainingDices )
  {
  }

  public bool Equals( RollSet? rollSet )
  {
    if ( rollSet is not null )
    {
      return DiceSet.SequenceEqual( rollSet.DiceSet ) && RemaingRoll.Equals( rollSet.RemaingRoll );
    }

    return false;
  }

  public override int GetHashCode()
  {
    int hash = RemaingRoll.GetHashCode();
    foreach ( DiceSet current in DiceSet )
    {
      HashCode.Combine( hash, current );
    }

    return hash;
  }

  public int Value => DiceSet.First().Dices.Sum( s => s.Value );

  public string OutputDebug =>
    $"Value = {Value} DiceSet={string.Join( ",", DiceSet.Select( d => string.Join( "-", d.Dices.Select( s => s.Value ) ) ) )} RemainingRoll={RemaingRoll.OutputDebug}";

  private static List<DiceSet> Sort( DiceSet[] source )
  {
    List<DiceSet> list = new( source );
    list.Sort( CompareDiceSet );
    return list;
  }

  private static int CompareDiceSet( DiceSet x, DiceSet y )
  {
    int maxIndex = Math.Min( x.Dices.Length, y.Dices.Length );
    for ( int i = 0; i < maxIndex; i++ )
    {
      if ( x.Dices[i].Value < y.Dices[i].Value )
      {
        return -1;
      }

      if ( x.Dices[i].Value > y.Dices[i].Value )
      {
        return 1;
      }
    }

    return 0;
  }
}