using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GTeck.DicePer12;

[DebuggerDisplay( "{OutputDebug}")]
public record DiceSet( ImmutableArray<Dice> Dices )
{
  public DiceSet( params Dice[] dices ) : this( dices.ToImmutableArray() )
  {
  }

  public int Value => Dices.Sum( d => d.Value );

  public string OutputDebug => $"Dices={string.Join( ",", Dices.Select( d => d.Value ) )} Value={Value}";

}
