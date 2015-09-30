using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChangeCalculator
{
    class Program
    {
        static decimal itemCost, amountGiven, change, numQuarters, 
                                    numDimes, numNickels, numPennies;
        // coinInfo array corresponds to quarters, dimes, nickels, pennies
        //  first set holds the number of that coin type
        //  second set holds the coin value
        static decimal[,] coinInfo =
            { {0, 0, 0, 0, 0, 0, 0, 0 } , 
              {20.0M, 10.0M, 5.0M, 1.0M, .25M, .10M, .05M, .01M } };
        static int coinInfoIndexNum = 0, coinInfoIndexVal = 1;
        // array of coin type labels
        static string[] coinTypeLabels =
            {"Dollars ($20): ", "Dollars ($10): ", "Dollars ($5): ", "Dollars ($1): ",
             "Quarters: ", "Dimes: ", "Nickels: ", "Pennies: "};
 
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to ChangeCalc Version 1.0!");

                // Get the input amounts
                getInputAmounts();

                // Calculate the number of each type of coin for the change
                calcCoinAmounts();

                // Write the results
                displayResults();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        // Get the user input: item cost and amount customer has given
        // Make sure that the amount given is not smaller than the cost
        static void getInputAmounts ()
        {
            itemCost = inputMoney("How much does the item cost? $");

            while (true)
            {
                amountGiven = inputMoney("How much has the customer given you? $");
                if (amountGiven >= itemCost)
                {
                    return;
                }
                else
                {
                    Console.WriteLine
                        ("The amount given by the customer must be greater " +
                               "than or equal to the cost (${0})", itemCost);
                }
            }
       }

        // Calculate the number needed of each type of coin in the change
        //  (quarters, dimes, nickels, pennies)
        static void calcCoinAmounts()
        {
           // Calculate the change amount
            change =  amountGiven - itemCost;
            decimal changeLeft = change;

            // For each coin type, calculate the number of coins needed:
            //  it is the change left divided by coin value, rounded down to nearest integer
            for (int i = 0; i < coinInfo.GetLength(1); i++)
            {
                coinInfo[coinInfoIndexNum, i] =
                   Decimal.Floor(changeLeft / coinInfo[coinInfoIndexVal, i]);
                // Update the change left to give
                changeLeft = changeLeft % coinInfo[coinInfoIndexVal, i];
            }


        }
        // Calculate the number of the specified coin in the change,
        // and update the change left after giving the coin
        static void calcCoin(decimal coinVal, out decimal numCoins, ref decimal changeAmount)
        {
            // Calculate nnumber of coins needed:  
            //   it is the change divided by coin value, rounded down to nearest integer  
            numCoins = Decimal.Floor(changeAmount / coinVal);

            // Calculate the change left to give after giving the coins
            changeAmount = changeAmount % coinVal;
        }

        // Display the change needed, and the amoumt of each coin type
        static void displayResults()
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("The customer's change is: ${0}, given as follows:", change);

            for (int i = 0; i < coinTypeLabels.Length; i++)
            {
                Console.WriteLine(coinTypeLabels[i] + 
                    coinInfo[coinInfoIndexNum, i]);
            }

       }

        // Read in currency amount, allowing up to 2 decimal places
        static decimal inputMoney (string message)
        {
            while (true)
            {
                decimal inputAmount = inputPosDecNum(message);
 
                // Check if the number of decimal places is correct
                //if (Regex.IsMatch(inputAmount.ToString(), @"^\d+(\.\d{1,2})?$"))
                if ( inputAmount == Math.Round(inputAmount, 2))
                {
                    return inputAmount;
                }
                else
                {
                    Console.WriteLine("Please enter a number with no more than 2 decimal places.");
                }                    
            }

        }

        // Output message, then read in a decimal number that must be greater than 0
        // If input is invalid, keep prompting
        // When input is valid, return the decimal number
        static decimal inputPosDecNum(string message)
        {
            decimal resultNum = 0;
            while (true)
            {
                // Output message, and read user input
                Console.Write(message);
                string input = Console.ReadLine();

                // Attempt to convert input to decimal
                // If result is positive decimal number, return the result,
                //    else output appropriate message
                if (Decimal.TryParse(input, out resultNum))
                {
                    if (resultNum > 0)
                    {
                        return resultNum;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a positive number.");
                    }
                }
                else
                {
                    Console.WriteLine("That number is too large.");
                }
            }
        }

    }
}
