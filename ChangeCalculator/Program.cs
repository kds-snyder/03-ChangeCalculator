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
        static decimal itemCost, amountGiven, change, numQuarters, numDimes, numNickels, numPennies;
        static decimal quarterVal = (decimal).25;
        static decimal dimeVal = (decimal).10;
        static decimal nickelVal = (decimal).05;
        static decimal pennyVal = (decimal).01;

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

            // Calculate nnumber of quarters needed 
            calcCoin(quarterVal, out numQuarters, ref changeLeft);

            // Calculate nnumber of dimes needed 
            calcCoin(dimeVal, out numDimes, ref changeLeft);

            // Calculate nnumber of nickels needed 
            calcCoin(nickelVal, out numNickels, ref changeLeft);

            // Number of pennies needed is the change that is left
            //  after giving the quarters, dimes, and nickels
            numPennies = changeLeft / pennyVal;

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
            Console.WriteLine("Quarters: {0}", numQuarters);
            Console.WriteLine("Dimes: {0}", numDimes);
            Console.WriteLine("Nickels: {0}", numNickels);
            Console.WriteLine("Pennies: {0}", numPennies);
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
