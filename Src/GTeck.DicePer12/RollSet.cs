using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GTeck.DicePer12;

[DebuggerDisplay( "{OutputDebug}")]
public sealed record RollSet( ImmutableArray<DiceSet> DiceSet, Roll RemaingRoll )
{
  public RollSet( DiceSet[] diceSet, Roll remainingDices ) : this( diceSet.ToImmutableArray(), remainingDices )
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

  public string OutputDebug => $"DiceSet.Value={string.Join( ",", DiceSet.Select( d => d.Value ) )} RemainingRoll={RemaingRoll.OutputDebug}";

}