using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GTeck.DicePer12;

[DebuggerDisplay( "{OutputDebug}")]
public record RollSet( ImmutableArray<DiceSet> DiceSet, Roll RemaingRoll )
{
  public RollSet( DiceSet[] diceSet, Roll remainingDices ) : this( diceSet.ToImmutableArray(), remainingDices )
  {
  }

  public int Value => DiceSet.First().Dices.Sum( s => s.Value );

  public string OutputDebug => $"DiceSet={string.Join( ",", DiceSet.Select( d => d.Value ) )} RemainingRoll={RemaingRoll.OutputDebug}";

}