using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Josie Cat's Word Guessing Game!");
            char result = GetAndValidateLetterGuess();
            Console.WriteLine("You chose {0}", result);

            
        }

        /// <summary>
        /// Loads all lines from specified file to an array
        /// of strings
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>An array of words from the file</returns>
        public static string[] GetWordsFromFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        string[] mysteryWords = File.ReadAllLines(path);
                        return mysteryWords;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else throw new Exception("File does not exist.");
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Displays all words from an array of words
        /// </summary>
        /// <param name="words">The array of words to be displayed</param>
        public static void DisplayAllWords(string[] words)
        {
            foreach(string word in words)
            {
                Console.WriteLine(word);
            }
        }

        /// <summary>
        /// Creates a file with the given path and writes each word from
        /// the array of words to the file on its own line
        /// </summary>
        /// <param name="path">The path where the file exists or should be created</param>
        /// <param name="words">An array of words that should be added to the file</param>
        public static void OverwriteOrCreateFile(string path, string[] words)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    //Add words to file
                    for(int i=0; i < words.Length; i++)
                    {
                        //Only write if word is not empty string
                        if(words[i] != "") sw.WriteLine(words[i]);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Appends the specified word to the end of the specified file, otherwise
        /// returns an exception
        /// </summary>
        /// <param name="path">Path of file to append to</param>
        /// <param name="word">Word to append</param>
        public static void AppendWordToFile(string path, string word)
        {
            try
            {   //If file exists, append word
                if (File.Exists(path))
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        try
                        {
                            sw.WriteLine(word);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                else throw new Exception("File does not exist.");
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Deletes all occurrences of a specified word from the specified file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="wordToDelete">Word to delete</param>
        public static void DeleteWordFromFile(string path, string wordToDelete)
        {
            //Get words
            string[] words = GetWordsFromFile(path);

            //New array of words with wordToDelete omitted
            string[] wordsAfterDeletion = new string[words.Length];

            //Fill wordsAfterDeletion
            for(int i=0; i < words.Length; i++)
            {
                //If line matches wordToDelete, insert empty string
                if (words[i].Contains(wordToDelete))
                {
                    wordsAfterDeletion[i] = "";
                }

                else wordsAfterDeletion[i] = words[i];
            }

            //Overwrite original file with wordsAfterDeletion
            OverwriteOrCreateFile(path, wordsAfterDeletion);
        }

        /// <summary>
        /// Prompts the user for their menu selection and validates the selection,
        /// otherwise reprompts the user until a valid selection is entered
        /// </summary>
        /// <returns>The user's menu selection</returns>
        public static int GetAndValidateMenuChoice()
        {
            bool validInput = false;
            int userChoiceVal = -1;

            //While user input is invalid, reprompt
            while(!validInput)
            {
                //Prompt user for menu choice
                Console.WriteLine("\n\nPlease enter your choice:");
                string userChoice = Console.ReadLine();
                //Reprompt if number not entered
                if (!Int32.TryParse(userChoice, out userChoiceVal))
                {
                    Console.WriteLine("\nChoice unrecognized.");
                    continue;
                }
                //Reprompt if number is not in menu
                else if (userChoiceVal < 1 || userChoiceVal > 4)
                {
                    Console.WriteLine("\nMenu selection not recognized.");
                    continue;
                }
                else validInput = true;
            }

            return userChoiceVal;
        }

        /// <summary>
        /// Gets and validates a user's word to add or delete
        /// </summary>
        /// <returns>The word the user wants to delete from the word file</returns>
        public static string GetAndValidateWordForAddDelete()
        {
            bool validInput = false;
            string word = "";
            while(!validInput)
            {
                Console.WriteLine("\n\nPlease enter word: ");
                word = Console.ReadLine();
                bool wordConditionsNotMet = word.Contains(" ") || word.Length < 1;
                //Check word is alpha only
                bool isAlphaOnly = true;
                foreach (char character in word)
                {
                    if (!char.IsLetter(character))
                    {
                        isAlphaOnly = false;
                        break;
                    }
                }
                //Check that word has no spaces, is a word
                if (wordConditionsNotMet || !isAlphaOnly)
                {
                    Console.WriteLine("\n\nInvalid word. Words must not contain spaces or non-alpha characters");
                    continue;
                }
                else validInput = true;
            }
            
            return word;
        }

        public static char GetAndValidateLetterGuess()
        {
            bool validInput = false;
            string guess = "";
            while (!validInput)
            {
                Console.WriteLine("\n\nGuess a letter: ");
                guess = Console.ReadLine();
                bool letterConditionsNotMet = guess.Length > 1 || !char.IsLetter(guess[0]);
                //Check that word has no spaces, is a word
                if (letterConditionsNotMet)
                {
                    Console.WriteLine("\n\nCharacter not recognized.");
                    continue;
                }
                else validInput = true;
            }
            return guess[0];
        }
    }


}
