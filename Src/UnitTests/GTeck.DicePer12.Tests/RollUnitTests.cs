using System.Linq;
using FluentAssertions;

namespace GTeck.DicePer12.Tests;

[TestClass]
public class RollUnitTests
{
  [TestMethod]
  public void EnumRollSet_TestCase1()
  {
    RollSet[] rollSets = RollUtil.EnumRollSet( new Roll( 2, 4, 4, 6, 6 ) ).OrderBy( s => s.Value ).ToArray();

    rollSets[0].Value.Should().Be( 2 );
    rollSets[0].DiceSet.Should().HaveCount( 1 );
    rollSets[0].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ) ) } );
    rollSets[0].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6, 6 } );

    rollSets[1].Value.Should().Be( 4 );
    rollSets[1].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ) ) } );
    rollSets[1].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6, 6 } );

    rollSets[2].Value.Should().Be( 4 );
    rollSets[2].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ) ) } );
    rollSets[2].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6, 6 } );

    rollSets[3].Value.Should().Be( 6 );
    rollSets[3].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ) ) } );
    rollSets[3].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4, 6 } );

    rollSets[4].Value.Should().Be( 6 );
    rollSets[4].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ) ) } );
    rollSets[4].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4, 6 } );

    rollSets[5].Value.Should().Be( 8 );
    rollSets[5].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ), new Dice( 6 ) ) } );
    rollSets[5].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6 } );

    rollSets[6].Value.Should().Be( 8 );
    rollSets[6].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ), new Dice( 6 ) ) } );
    rollSets[6].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6 } );

    rollSets[7].Value.Should().Be( 8 );
    rollSets[7].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 4 ) ) } );
    rollSets[7].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 6, 6 } );

    rollSets[8].Value.Should().Be( 10 );
    rollSets[8].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[8].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[9].Value.Should().Be( 10 );
    rollSets[9].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[9].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[10].Value.Should().Be( 10 );
    rollSets[10].DiceSet.Should().HaveCount( 1 );
    rollSets[10].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[10].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[11].Value.Should().Be( 10 );
    rollSets[11].DiceSet.Should().HaveCount( 1 );
    rollSets[11].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[11].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[12].Value.Should().Be( 12 );
    rollSets[12].DiceSet.Should().HaveCount( 1 );
    rollSets[12].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ), new Dice( 6 ) ) } );
    rollSets[12].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );
  }

  [TestMethod]
  public void AppendRemaingRoll_TestCase1()
  {
    RollSet[] rollSets = RollUtil.AppendRemaingRoll( RollUtil.EnumRollSet( new Roll( 2, 4, 4, 6, 6 ) ) ).OrderBy( s => s.Value ).ToArray();

    rollSets[0].DiceSet.Should().HaveCount( 1 );
    rollSets[0].DiceSet[0].Value.Should().Be( 2 );
    rollSets[0].DiceSet[0].Dices.Should().BeEquivalentTo( new Dice[] { 2 } );
    rollSets[0].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6, 6 } );

    rollSets[1].DiceSet.Should().HaveCount( 2 );
    rollSets[1].DiceSet[0].Value.Should().Be( 4 );
    rollSets[1].DiceSet[0].Dices.Should().BeEquivalentTo( new Dice[] { 4 } );
    rollSets[1].DiceSet[1].Dices.Should().BeEquivalentTo( new Dice[] { 4 } );
    rollSets[1].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 6, 6 } );
  }
}