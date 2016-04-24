using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Advanced_Gold_Lotto_Checker {
    /*
     * Randomly generates a list of 12 lotto games
     * and check them against a randomly generated list of
     * drawn numbers, and displays the number of winning numbers
     * and supplementaries.
     * 
     * Author: Megan Hunter
     * Date: March 2015
     *  
     */
    class Program {

        //Minimum and maximum values possible when creating numbers for each game
        const int MIN_VALUE = 1, MAX_VALUE = 46;

        //Number of supplementaries in each draw
        const int SUPP = 2;


        static Random randomGen = new Random();

        /* Calls the methods to execute the program
         * 
         * precondition: none
         * postcondition: returns a series of strings and integers
         */
        static void Main() {

            const int NUMBER_OF_ROWS = 12;

            int[][] lottoNumbers ={ 
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6],
                             new int [6] 
                              };

            int[] drawNumbers = new int[8];

            InsertYourNums(lottoNumbers);
            InsertDrawNums(drawNumbers);

            TitleMessage();
            PrintYourNumbers(lottoNumbers);
            PrintDrawNumbers(drawNumbers);
            PrintChecks(lottoNumbers, drawNumbers, lottoNumbers.Length);
            GoodbyeMessage();

            ExitProgram();
        }//end Main


        /* Insert random integers into a list which are sorted with no duplicates.
         * 
         * precondition: yourNums must be a two-dimentional integer array
         * postcondition: returns a series of lists of random positive integers with no duplicates in ascending order
         */
        static void InsertYourNums(int[][] yourNums) {
            int temp = 0;
            
            for (int i = 0; i < yourNums.Length; i++) {
                for (int j = 0; j < yourNums[i].Length; j++) {
                    temp = randomGen.Next(MIN_VALUE, MAX_VALUE);
                    for (int k = 0; k < yourNums[i].Length; k++) {
                        while (yourNums[i][k] == temp) {
                            temp = randomGen.Next(MIN_VALUE, MAX_VALUE);
                        }
                    }
                    yourNums[i][j] = temp;
                }
                Array.Sort<int>(yourNums[i]);
            }
        } //end InsertYourNums


        /* Insert random integers into a list with no duplicates.
         * 
         * precondition: drawNums must be a single integer array
         * postcondition: returns a single lists of random positive integers with no duplicates
         */
        static void InsertDrawNums(int[] drawNums) {
            int temp = 0;

            for (int i = 0; i < drawNums.Length; i++) {
                temp = randomGen.Next(MIN_VALUE, MAX_VALUE);
                for (int j = 0; j < drawNums.Length; j++) {
                    while (drawNums[j] == temp) {
                        temp = randomGen.Next(MIN_VALUE, MAX_VALUE);
                    }
                }
                drawNums[i] = temp;
            }
        } //end InsertDrawNums


        /* Print welcome message
         * 
         * precondition: none
         * postcondition: returns the string
         */
        static void TitleMessage() {
            Console.WriteLine("\n\t Welcome to Lotto Checker \n\n\n");
        } //end TitleMessage


        /* Display the list of games created
         * 
         * precondition: list must be a two-dimentional array containing integers
         * postcondition: returns the list of games on separate lines (as strings)
         */
        static void PrintYourNumbers(int[][] list) {
            Console.WriteLine("Your Lotto numbers are:\n");
            for (int i = 0; i < list.Length; i++) {
                Console.Write("Game{0,3}:", i + 1);
                for (int j = 0; j < list[i].Length; j++) {
                    Console.Write("  {0,2}", list[i][j]);
                }
                Console.WriteLine("\n\n");
            }
        } //end PrintYourNumbers


        /* Display the single draw list created
        * 
        * precondition: list must be a two-dimentional array containing integers
        * postcondition: returns the draw list
        */
        static void PrintDrawNumbers(int[] list) {
            Console.WriteLine("\n\n Lotto Draw Numbers are: \n");
            for (int i = 0; i < list.Length; i++) {
                Console.Write("  {0,2}", list[i]);
            }
            Console.WriteLine("\n");
        } //end PrintDrawNumbers


        /* Display the string of winning numbers and sups
        * 
        * precondition: all parameters must be integers, yourNums must be 2D array,
        *                  drawNums must be single array, 
        *                  gameNum > 0 and < length of yourNums  
        * postcondition: returns the winning values on separate lines (as a string)
        */
        static void PrintWinners(int[][] yourNum, int[] drawNum, int gameNum) {
            int winningNum = 0, winningSup = 0;

            winningNum = CheckNumbers(yourNum, drawNum, gameNum);
            winningSup = CheckSups(yourNum, drawNum, gameNum);

            Console.WriteLine("\n\n\t found {0} winning numbers and {1} supplementary numbers in Game {2}\n\n", winningNum, winningSup, gameNum + 1);
        } //end PrintWinners


        /* Compare game numbers against draw numbers
        * 
        * precondition: all parameters must be integers, yourNums must be 2D array,
        *                  drawNums must be single array, 
        *                  gameNum > 0 and < length of yourNums  
        * postcondition: returns the number of winning numbers
        */
        static int CheckNumbers(int[][] yourNum, int[] drawNum, int gameNum) {
            int winningNumber = 0;
            for (int i = 0; i < yourNum[gameNum].Length; i++) {
                for (int j = 0; j < drawNum.Length - SUPP; j++) {
                    if (yourNum[gameNum][i] == drawNum[j]) {
                        winningNumber++;
                    }
                }
            }
            return winningNumber;
        } //end CheckNumbers


        /* Compare game sup numbers against draw numbers
        * 
        * precondition: all parameters must be integers, yourNums must be 2D array,
        *                  drawNums must be single array, 
        *                  gameNum > 0 and < length of yourNums  
        * postcondition: returns the number of winning sups
        */
        static int CheckSups(int[][] yourNum, int[] drawNum, int gameNum) {
            int winningSup = 0;

            for (int j = 0; j < yourNum[gameNum].Length; j++) {
                for (int k = drawNum.Length - SUPP; k < drawNum.Length; k++) {
                    if (yourNum[gameNum][j] == drawNum[k]) {
                        winningSup++;
                    }
                }
            }
            return winningSup;
        } //end CheckSups


        /* Prints each game's winning numbers and sups
         * 
         * precondition: all parameters must be integers, yourNums must be 2D array,
         *                  drawNums must be single array, 
         *                  gameNum > 0 and < length of yourNums  
         * postcondition: prints each game's winning number & sup count
         */
        static void PrintChecks(int[][] yourNums, int[] drawNums, int gameNum) {
            for (int i = 0; i < gameNum; i++) {
                PrintWinners(yourNums, drawNums, i);
            }
        } //end PrintChecks



        /* Print goodbye message
         * 
         * precondition: none
         * postcondition: returns the string
         */
        static void GoodbyeMessage() {
            Console.WriteLine("\n\n\t Thanks for using Lotto Checker!\n");
        } //end GoodbyeMessage


        /* Print exit message
         * 
         * precondition: none
         * postcondition: returns the string
         */
        static void ExitProgram() {
            Console.Write("\n\nPress any key to exit program: ");
            Console.ReadKey();
        }//end ExitProgram

    } //end class Program
}
