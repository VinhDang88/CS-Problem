using ChipSecuritySystem;

static bool UnlockPanel(List<ColorChip> chipList)
{

    Color colorToMatch = Color.Blue;


    List<ColorChip> solution = new List<ColorChip>(); // empty for now


    List<List<ColorChip>> possibleSolutions = new List<List<ColorChip>>();

    //I want multiple solutions, so the best way to start is by separating all chips that start with blue 
    List<ColorChip> possibleStarters = chipList.Where(c => c.StartColor == colorToMatch).ToList();


    bool startingChipFound = possibleStarters.Any();

    while (startingChipFound)
    {

        foreach (ColorChip c in possibleStarters)
        {
            solution.Add(c);
            chipList.Remove(c);
            colorToMatch = c.EndColor;

            if (colorToMatch == Color.Green) //one-chip solution 
            {
                possibleSolutions.Add(solution); //adding solution as-is, but still seeing if we can go further below (for large collections of chips) 
            }

            while (chipList.Count > 0) // we will rarely reach a point where there's zero chips left. setting it like this allows us to keep searching for matches for longer.
                                       //if we'd have set a for each, each chip would've been checked once 
            {
                ColorChip match = chipList.FirstOrDefault(chp => chp.StartColor == colorToMatch);

                if (match != null)
                {
                    colorToMatch = match.EndColor;
                    solution.Add(match);
                    chipList.Remove(match);

                    if (colorToMatch == Color.Green)
                    {
                        possibleSolutions.Add(solution);
                    }
                }
                else
                    break;  // we get out of this while loop and move on to the next blue chip (if it exists) 
            }
            solution.ForEach(x => chipList.Add(x)); //add all chips we're using back to chip list 
            solution.Clear();  //clear out solution 
        }
        break; // we need to be able to get out of the first loop 
    }

    try
    {
        List<ColorChip> finalSolution = possibleSolutions.OrderByDescending(c => c.Count()).First();

        foreach (ColorChip c in finalSolution)
        {
            Console.WriteLine(c.ToString());
        }
    }
    catch
    {
        Console.WriteLine("No solutions found.");
    }

    if (possibleSolutions.Count > 0)
    {
        return true;
    }
    else
    {
        Console.WriteLine(Constants.ErrorMessage);
        return false;
    }
}

List<ColorChip> chipList = new List<ColorChip>()
{
    new ColorChip (Color.Blue, Color.Yellow ),
    new ColorChip (Color.Red, Color.Green ),
    new ColorChip (Color.Yellow, Color.Red),
    new ColorChip (Color.Orange, Color.Purple),
    new ColorChip (Color.Blue, Color.Green),
    new ColorChip (Color.Red, Color.Purple),
    new ColorChip (Color.Purple, Color.Green),
};


Console.WriteLine(UnlockPanel(chipList));
/*  NOTES - please read. 
 When planning this out, I initially had the goal of perhaps finding all permutations with a given chip combination, then using LINQ to select whatever
 combinations fit the criteria. I don't know how to write an algorithm of this type yet, and couldn't find anything to do it with objects, only numbers. */

/* This code doesn't even find ALL solutions still, but I did separate all chips that have a start color BLUE in order to guarantee more combinations. 
I was not able to write the code to "backtrack" or go back to any chips who may have had multiple options to test  without restarting the loop. It got too 
complex, and not in an efficient way. */

/* 
 Finally, I've tried 5-6 different methods, but I just can't get my final combination to print. I promise you that I know how to iterate through a list 
and print the members. I really can't figure out why the WriteLine statements are not executing properly, when I know the loop detects different solutions. 

  */