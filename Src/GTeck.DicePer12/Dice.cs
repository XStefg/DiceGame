using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTeck.DicePer12;

public record Dice( int Value )
{
  public static implicit operator Dice( int dice ) => new Dice( dice );
}