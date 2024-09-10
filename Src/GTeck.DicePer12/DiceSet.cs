using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GTeck.DicePer12;

[DebuggerDisplay( "{OutputDebug}" )]
public sealed record DiceSet( ImmutableArray<Dice> Dices )
{
  public DiceSet( params Dice[] dices ) : this( dices.OrderBy( s => s.Value  ).ToImmutableArray() )
  {
  }

  public bool Equals( DiceSet? diceSet )
  {
    if ( diceSet is not null )
    {
      return Dices.SequenceEqual( diceSet.Dices );
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

  public int    Value       => Dices.Sum( d => d.Value );
  public string OutputDebug => $"Dices={string.Join( ",", Dices.Select( d => d.Value ) )} Value={Value}";
}
