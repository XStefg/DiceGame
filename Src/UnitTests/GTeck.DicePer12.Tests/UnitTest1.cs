using System.Linq;

namespace GTeck.DicePer12.Tests;

[TestClass]
public class UnitTest1
{
  [TestMethod]
  public void TestMethod1()
  {
    Dice[] dices = [ 2,4,5,6,7 ];

    Roll roll = new( dices );

    var v = RollUtil.EnumDiceSet( roll ).ToArray();

  }
}