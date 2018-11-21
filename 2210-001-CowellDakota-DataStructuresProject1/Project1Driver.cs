///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1/DataStructuresProject1
//	File Name:         Project1Driver.cs
//	Description:       Demonstrate functionality of Tools class using a menu
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Dakota Cowell, cowelld@etsu.edu, Dept. of Computing, East Tennessee State University
//	Created:           Thursday, September 6, 2018
//	Copyright:         Dakota Cowell, 2018
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresProject1;
using System.Windows.Forms;
using System.IO;

namespace DataStructuresProject1
{
    /// <summary>
    /// Driver class to demonstrate the functionality of the Tools class. 
    /// This class interacts with the user and uses the Menu class.
    /// </summary>
    class Project1Driver
    {
        /// <summary>
        /// Main method of the program
        /// Displays the menu and handles user input as well as displaying the correct information
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            //Setup Console
            Tools.Setup();

            //Create new Menu
            Menu menu = new Menu("Project 1 Menu");
            menu = menu + "Display Welcome Message" + "Input a string to be cleaned" + "Display a list of all words" + "Format text for output" + "Quit";

            //Get input from menu and loop until the quit choice is chosen
            Choices choice = (Choices)menu.GetChoice();
            while (choice != Choices.QUIT)
            {
                switch (choice)
                {
                    case Choices.DISPLAYWELCOME:
                        DisplayWelcomeMessage();
                        break;

                    case Choices.CLEANSTRING:
                        CleanInputString();
                        break;

                    case Choices.PARSE:
                        OpenFileAndParse();
                        break;

                    case Choices.FORMAT:
                        OpenFileAndFormat();
                        break;
                }  // end of switch

                choice = (Choices)menu.GetChoice();
            }  // end of while
            DisplayGoodbyeMessage();
        }

        /// <summary>
        /// Method to display message box welcoming user to the program. 
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            //Ask the user for what course and author this is. 
            Console.WriteLine("What course is this for?");
            var course = Console.ReadLine();
            Console.WriteLine("Who is the author?");
            var author = Console.ReadLine();
            Tools.WelcomeMessage("Welcome to Project 1 in Data Structures. This is the first project of the semester.", course, author);
        }
        
        /// <summary>
        /// Method to display messagebox telling the user goodbye
        /// </summary>
        private static void DisplayGoodbyeMessage()
        {
            Tools.GoodbyeMessage("Goodbye! Thanks for using this program");
        }

        /// <summary>
        /// Helper method to open a file and format it correctly according to user input margins
        /// </summary>
        private static void OpenFileAndFormat()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"..\..\TestData";
            string fileName = "";
            StreamReader rdr = null;
            string stringFromFile = "";

            if (DialogResult.Cancel != dlg.ShowDialog())
            {
                fileName = dlg.FileName;
            }

            try
            {
                rdr = new StreamReader(fileName);
                stringFromFile = rdr.ReadToEnd();

                List<string> list = Tools.Parse(stringFromFile, " ,\n");

                Console.WriteLine("What would you like the left margin to be?");
                string leftMarginString = Console.ReadLine();
                int leftMargin = int.Parse(leftMarginString);

                Console.WriteLine("What would you like the right margin to be?");
                string rightMarginString = Console.ReadLine();
                int rightMargin = int.Parse(rightMarginString);

                if(leftMargin >= rightMargin)
                {
                    Console.WriteLine("Left margin must be less than right margin.");
                } else
                {
                    string stringReturn = Tools.Format(list, leftMargin, rightMargin);
                    Console.WriteLine("Your Formatted String:");
                    Console.WriteLine(stringReturn);
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        /// <summary>
        /// Method to ask user for input and then clean string - shows functionality of the CleanString method in the Tools class
        /// </summary>
        private static void CleanInputString()
        {
            //Ask for a string to clean and then clean it and display it
            Console.WriteLine("Enter a string to clean.");
            string stringToClean = Console.ReadLine();
            string cleanedString = Tools.CleanString(ref stringToClean);

            Console.WriteLine("Your cleaned string is " + '"' + cleanedString + '"');
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        /// <summary>
        /// Helper method that gets called in the OpenFileAndParse Method 
        /// Asks user which delimiter set they would like to use
        /// </summary>
        /// <returns>Returns a string of the delimiters the user wants to use</returns>
        private static string ChooseDelimiters()
        {
            string delimString = "\r \n";

            //Ask for user's delimiters
            Console.WriteLine("Which set of delimiters would you like to use?");
            Console.WriteLine("1. All symbols");
            Console.WriteLine("2. All letters");
            Console.WriteLine("3. \"\\r .;\\n\"");
            Console.WriteLine("4. All numbers");
            Console.WriteLine("5. Space character");
            Console.WriteLine("6. \"\\n \\r\"");
            Console.Write("Enter a number from 1-4: ");

            //Read user's selection
            int userChoice = int.Parse(Console.ReadLine());

            //Check User choice and set the delmString to the selected number
            if(userChoice == 1)
            {
                delimString = "~`!@#$%^&*()_-=+[{]}\\|'\";:/?.<>,";
            } else  if (userChoice == 2)
            {
                delimString = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            } else if (userChoice == 3)
            {
                delimString = "\r .;\n";
            } else if (userChoice == 4)
            {
                delimString = "1234567890";
            } else if (userChoice == 5)
            {
                delimString = " ";
            } else if(userChoice == 6)
            {
                delimString = "\r \n";
            }
            return delimString;
        }

        /// <summary>
        /// Opens a file and parses it for words. Then, displays the words in a list to the user
        /// Shows the funcionality of the Tools class Parse method
        /// </summary>
        private static void OpenFileAndParse()
        {
            //Create new OpenFileDialog with initial directory
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"..\..\TestData";
            string fileName = "";
            StreamReader rdr = null;
            string stringFromFile = "";

            //Get filename from dialog
            if (DialogResult.Cancel != dlg.ShowDialog())
            {
                fileName = dlg.FileName;
            }

            try
            {
                //Create new streamReader and read to end of file with specified delimiters
                rdr = new StreamReader(fileName);

                stringFromFile = rdr.ReadToEnd();

                string delims = ChooseDelimiters();

                //Use parse method to break string into words
                List<string> stringList = Tools.Parse(stringFromFile, delims);
                int count = 0;

                //Print Words to console in a list
                Console.WriteLine("Count".PadLeft(7) + "   Word");
                foreach (string word in stringList)
                {
                    string countAsString = count.ToString() + ".";
                    Console.WriteLine(countAsString.PadLeft(7) + "   " + word);
                    count++;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            } finally
            {
                if(rdr != null)
                {
                    //Close the reader
                    rdr.Close();
                }
            }
        }
    }
}
