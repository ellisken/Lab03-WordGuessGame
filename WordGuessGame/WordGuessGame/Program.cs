using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        private const int MenuChoiceCt = 5;

        static void Main(string[] args)
        {
            string path = "../../../mysteryWords.txt";
            string[] words = {
                "catnip",
                "salmon",
                "yarn",
                "feathers",
                "meow",
            };

            try
            {
                //Create mystery words file
                OverwriteOrCreateFile(path, words);
                //Welcome User
                Console.WriteLine("Welcome to Josie Cat's Word Guessing Game!");
                //Display Menu (which handles game play)
                //HomeNavigation(path);

            }
            catch
            {
                ExitGame(1);
            }
        }

        /// <summary>
        /// Displays the menu and processes the user's menu choice, executing
        /// the next step.
        /// </summary>
        /// <param name="path">File path for mystery words file</param>
        public static void HomeNavigation(string path)
        {
            string menuOptions = "1: PLAY\n2: SEE WORDS\n3: ADD WORD\n4: DELETE WORD\n5: EXIT";


            //Display menu
            Console.WriteLine(menuOptions);
            //Get user choice
            int userMenuChoice = GetAndValidateMenuChoice();
            //Execute choice
            try
            {
                string word = ""; //stores word for add or delete

                switch (userMenuChoice)
                {
                    //Play game
                    case 1:
                        Console.WriteLine("Play game!");
                        break;
                    //Show words
                    case 2:
                        string[] words = GetWordsFromFile(path);
                        DisplayAllWords(words);
                        break;
                    //Add word
                    case 3:
                        Console.WriteLine("\n\nAdd chosen.");
                        word = GetAndValidateWordForAddDelete();
                        AppendWordToFile(path, word);
                        break;
                    //Delete word
                    case 4:
                        Console.WriteLine("\n\nDelete chosen.");
                        word = GetAndValidateWordForAddDelete();
                        DeleteWordFromFile(path, word);
                        break;
                    //Exit game
                    case 5:
                        ExitGame(0);
                        break;
                }
            }
            catch
            {
                ExitGame(1);
            }
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
            foreach (string word in words)
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
                    for (int i = 0; i < words.Length; i++)
                    {
                        //Only write if word is not empty string
                        if (words[i] != "") sw.WriteLine(words[i]);
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
            for (int i = 0; i < words.Length; i++)
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
            while (!validInput)
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
                else if (userChoiceVal < 1 || userChoiceVal > MenuChoiceCt)
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
            while (!validInput)
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

        /// <summary>
        /// Gets and validates a user's letter guess
        /// </summary>
        /// <returns>The letter the user chose</returns>
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

        /// <summary>
        /// Exits the game with a goodbye message
        /// </summary>
        /// <param name="status">Game status. 0 is good, nonzero means an error occurred</param>
        public static void ExitGame(int status)
        {
            if (status == 0)
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Error occurred. Exiting game.");
            }
            Environment.Exit(status);
        }

        public static void GamePlay()
        {

        }

        /// <summary>
        /// Returns a word from the input array chosen at random
        /// </summary>
        /// <param name="words">An array of words to choose from</param>
        /// <returns>Randomly chosen word from input array</returns>
        public static string GetRandomWord(string[] words)
        {
            //Generate random index within range
            Random rand = new Random();
            int randomIndex = rand.Next(0, words.Length);
            //Return string at index
            return words[randomIndex];
        }

        public static string CreateMysteryString()
        {
            return "string";
        }

        public static void ProcessLetterGuess()
        {

        }

        public static bool FullWordGuessed(string mysterString)
        {
            return true;
        }

        public static void DisplayCurrentGameState()
        {

        }

    }
}
