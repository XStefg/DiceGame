using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace GTeck.DicePer12.Tests;

[TestClass]
public class RollUnitTests
{
  [TestMethod]
  public void EnumRollSet_TestCase1()
  {
    RollSet[] rollSets = new Roll( 2, 4, 4, 6, 6 ).EnumRollSet().OrderBy( s => s.Value ).ToArray();

    rollSets[0].Value.Should().Be( 2 );
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
    rollSets[10].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[10].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[11].Value.Should().Be( 10 );
    rollSets[11].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[11].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 6 } );

    rollSets[12].Value.Should().Be( 12 );
    rollSets[12].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ), new Dice( 6 ) ) } );
    rollSets[12].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );
  }

  [TestMethod]
  public void RemoveDuplicate_TestCase2()
  {
    RollSet[] rollSetsRaw = new Roll( 4, 4, 4, 4, 4 ).EnumRollSet().AppendRemaingRoll().OrderBy( s => s.Value ).ToArray();
    RollSet[] rollSets    = rollSetsRaw.RemoveDuplicate().ToArray();

    rollSets[0].Value.Should().Be( 4 );
    rollSets[0].DiceSet.Should().BeEquivalentTo( new[]
                                                 {
                                                   new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) ),
                                                   new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) )
                                                 } );
    rollSets[0].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { } );

    rollSets[1].Value.Should().Be( 8 );
    rollSets[1].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 4 ) ), new DiceSet( new Dice( 4 ), new Dice( 4 ) ) } );
    rollSets[1].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { new( 4 ) } );
  }

  [TestMethod]
  public void AppendRemaingRoll_TestCase1()
  {
    RollSet[] rollSets = new Roll( 2, 4, 4, 6, 6 ).EnumRollSet().AppendRemaingRoll().OrderBy( s => s.Value ).ToArray();

    rollSets[0].Value.Should().Be( 2 );
    rollSets[0].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ) ) } );
    rollSets[0].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6, 6 } );

    rollSets[1].Value.Should().Be( 4 );
    rollSets[1].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) ) } );
    rollSets[1].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 6, 6 } );

    rollSets[2].Value.Should().Be( 4 );
    rollSets[2].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) ) } );
    rollSets[2].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 6, 6 } );

    rollSets[3].Value.Should().Be( 6 );
    rollSets[3].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ) ), new DiceSet( new Dice( 6 ) ) } );
    rollSets[3].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );

    rollSets[4].Value.Should().Be( 6 );
    rollSets[4].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ) ), new DiceSet( new Dice( 6 ) ) } );
    rollSets[4].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );

    rollSets[5].Value.Should().Be( 8 );
    rollSets[5].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 4 ) ) } );
    rollSets[5].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 6 } );

    rollSets[6].Value.Should().Be( 8 );
    rollSets[6].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 4 ) ) } );
    rollSets[6].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 6 } );

    rollSets[7].Value.Should().Be( 8 );
    rollSets[7].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 4 ) ), new DiceSet( new Dice( 2 ), new Dice( 6 ) ) } );
    rollSets[7].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 6 } );

    rollSets[8].Value.Should().Be( 10 );
    rollSets[8].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[8].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2 } );

    rollSets[9].Value.Should().Be( 10 );
    rollSets[9].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[9].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2 } );

    rollSets[10].Value.Should().Be( 10 );
    rollSets[10].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[10].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2 } );

    rollSets[11].Value.Should().Be( 10 );
    rollSets[11].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[11].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2 } );

    rollSets[12].Value.Should().Be( 12 );
    rollSets[12].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ), new Dice( 6 ) ) } );
    rollSets[12].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );
  }

  [TestMethod]
  public void RemoveDuplicate_TestCase1()
  {
    RollSet[] rollSetsRaw = new Roll( 2, 4, 4, 6, 6 ).EnumRollSet().AppendRemaingRoll().OrderBy( s => s.Value ).ToArray();
    RollSet[] rollSets    = rollSetsRaw.RemoveDuplicate().ToArray();

    rollSets[0].Value.Should().Be( 2 );
    rollSets[0].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ) ) } );
    rollSets[0].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 4, 4, 6, 6 } );

    rollSets[1].Value.Should().Be( 4 );
    rollSets[1].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ) ), new DiceSet( new Dice( 4 ) ) } );
    rollSets[1].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 6, 6 } );

    rollSets[2].Value.Should().Be( 6 );
    rollSets[2].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ) ), new DiceSet( new Dice( 6 ) ) } );
    rollSets[2].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );

    rollSets[3].Value.Should().Be( 8 );
    rollSets[3].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 2 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 4 ) ) } );
    rollSets[3].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 6 } );

    rollSets[4].Value.Should().Be( 10 );
    rollSets[4].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 4 ), new Dice( 6 ) ), new DiceSet( new Dice( 4 ), new Dice( 6 ) ) } );
    rollSets[4].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2 } );

    rollSets[5].Value.Should().Be( 12 );
    rollSets[5].DiceSet.Should().BeEquivalentTo( new[] { new DiceSet( new Dice( 6 ), new Dice( 6 ) ) } );
    rollSets[5].RemaingRoll.Dices.Should().BeEquivalentTo( new Dice[] { 2, 4, 4 } );
  }

  [TestMethod]
  public void TestEquals()
  {
    new Dice( 5 ).Equals( new Dice( 5 ) ).Should().BeTrue();
    new Dice( 5 ).Equals( new Dice( 1 ) ).Should().BeFalse();

    object diceSet = new DiceSet( new Dice( 6 ), new Dice( 6 ) );
    new DiceSet( new Dice( 6 ), new Dice( 6 ) ).Equals( diceSet ).Should().BeTrue();

    new Roll( 1, 2, 3 ).Equals( new Roll( 1, 2, 3 ) ).Should().BeTrue();

    object rollSet = new RollSet( new[] { new DiceSet( new Dice( 3 ), new Dice( 4 ) ) }, new Roll( 5, 6 ) );
    new RollSet( new[] { new DiceSet( new Dice( 3 ), new Dice( 4 ) ) }, new Roll( 5, 6 ) )
      .Equals( rollSet )
      .Should().BeTrue();
  }
}