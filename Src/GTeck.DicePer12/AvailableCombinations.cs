namespace GTeck.DicePer12;

public class AvailableCombinations
{
  public static Combination[] Combinations = 
  [
    new Combination( 1,  false ),
    new Combination( 2,  false ),
    new Combination( 3,  false ),
    new Combination( 4,  false ),
    new Combination( 5,  false ),
    new Combination( 6,  false ),
    new Combination( 7,  true ),
    new Combination( 8,  true ),
    new Combination( 9,  true ),
    new Combination( 10, true ),
    new Combination( 11, true ),
    new Combination( 12, true )
  ];

  public Combination this[ int value ] => Combinations[value + 1];
}