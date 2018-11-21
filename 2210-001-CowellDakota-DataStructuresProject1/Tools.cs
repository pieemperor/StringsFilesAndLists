///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  DataStructuresProject1/DataStructuresProject1
//	File Name:         Tools.cs
//	Description:       Perform common tasks and string manipulation tasks
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Dakota Cowell, cowelld@etsu.edu
//	Created:           Thursday, September 6, 2018
//	Copyright:         Dakota Cowell, 2018
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataStructuresProject1
{
    /// <summary>
    /// This class shows different functionalities of the string class as well as using messageBox to display information
    /// </summary>
    static class Tools
    {
        /// <summary>
        /// Displays a MessageBox with a specified message, caption, and author greeting the user
        /// </summary>
        /// <param name="message">Message to display in MessageBox</param>
        /// <param name="caption">Caption to display in MessageBox</param>
        /// <param name="author">Author to display in MessageBox</param>
        public static void WelcomeMessage(String message, String caption, String author)
        {

            System.Windows.Forms.MessageBox.Show(message,caption + " - " + author,
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays MessageBox with specified message
        /// </summary>
        /// <param name="message">Message to be displayed in MessageBox</param>
        public static void GoodbyeMessage(String message)
        {
            System.Windows.Forms.MessageBox.Show(message,"Goodbye!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Initializes basic console properties including background and foreground color and title
        /// </summary>
        public static void Setup()
        {
            //Clear Console
            Console.Clear();

            //Set background and foreground color of the console
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //Give the Console a caption / title
            Console.Title = "Data Structures Project 1";
        }

        /// <summary>
        /// Receives a string and removes all leading and trailing spaces and tabs
        /// </summary>
        /// <param name="work">String to be cleaned</param>
        /// <returns>Returns cleaned version of passed string</returns>
        public static string CleanString(ref string work)
        {
            string trimmedString = "";

            //Remove leading and trailing spaces and tabs
            char[] charsToTrim = { ' ', '\t'};
            trimmedString = work.Trim(charsToTrim);

            //Replace carriage return and new line with new line
            trimmedString = trimmedString.Replace("\r\n", "\n");

            //Return cleaned string
            return trimmedString;
        }

        /// <summary>
        /// Splits a string into a List of words based on the delimiters passed
        /// </summary>
        /// <param name="original">Original string to be parsed for words</param>
        /// <param name="delimiters">Delimiters to split the string on</param>
        /// <returns>Returns a list of words that were in the original string</returns>
        public static List<String> Parse(string original, string delimiters)
        {
            //Copy original into temp variable
            string temp = original;

            //Initialize List<string>
            List<string> words = new List<string>();

            //Split delimiters string into array of characters to be used in IndexOfAny
            var delims = delimiters.ToCharArray();

            //loop until the temp string is empty
            while (!String.IsNullOrEmpty(temp))
            {
                //Find index of first delimiter in temp
                var index = temp.IndexOfAny(delims);

                //If the position of the delimiter is greater than 0, add everything before the delimiter as a word. Then remove the word from temp
                if (index > 0)
                {
                    words.Add(temp.Substring(0, index));
                    temp = temp.Substring(index);
                }
                //If the position of the delimiter = 0, add the delimiter to the words list if it's not a space or new line
                else if (index == 0)
                {
                    if(temp.Substring(0, index + 1) != " " && temp.Substring(0, index + 1) != "\n")
                    {
                        words.Add(temp.Substring(0, index + 1));
                        temp = temp.Substring(index + 1);
                    } else
                    {
                        temp = temp.Substring(index + 1);
                    }
                }
                //If there is not another delimiter, add the rest of temp to the words list
                else if (index == -1)
                {
                    words.Add(temp);
                    temp = "";
                }

               //Trim leading and trailing spaces or new line characters
                temp.Trim(new char[] { ' ', '\t', '\r'});
            }

            return words;
        }

        /// <summary>
        /// Takes a List of words as input and returns a string that is formatted to fit between 2 margins
        /// </summary>
        /// <param name="list">List of words to be formatted</param>
        /// <param name="leftMargin">Number of spaces to displayed in the left margin</param>
        /// <param name="rightMargin">Number of characters allowed in each line</param>
        /// <returns>Returns a formatted string with margins</returns>
        public static String Format(List<string> list, int leftMargin, int rightMargin)
        {
            //Initialize a string 
            string spaceString = "";

            //set spaceString to the specified number of spaces on the left margin
            for (int i = 0; i < leftMargin;i++)
            {
                spaceString += " ";
            }

            //Initialize formattedString with the set number of spaces at the beginning
            string formattedString = spaceString;

            //Initialize empty List<string>
            List<string> lines = new List<string>();

            //Loop through each item in the list that was passed in
            foreach(string item in list)
            {
                //If the length of the current line + the length of next item is less than the right margin, add it to the current line. Otherwise, start a new line.
                if(formattedString.Length + item.Length + 1 <= rightMargin)
                {
                    formattedString += item + " ";
                }
                else
                {
                    lines.Add(formattedString);
                    formattedString = spaceString + item + " ";
                }
            }

            //Add the remaining line to the list of lines
            lines.Add(formattedString);
            formattedString = "";

            //Put all the lines from the list together
            foreach(string line in lines)
            {
                formattedString += line + "\n";
            }

            //Replace all \r characters with an empty string. Otherwise, it causes formatting issues
            return formattedString.Replace("\r","");
        }
    }
}
