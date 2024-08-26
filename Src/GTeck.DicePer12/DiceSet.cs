using System.Collections.Immutable;
using System.Linq;

namespace GTeck.DicePer12;

public record DiceSet( ImmutableArray<Dice> Dices )
{
  public DiceSet( params Dice[] dices ) : this( dices.ToImmutableArray() )
  {
  }

  public int Value => Dices.Sum( d => d.Value );
}
