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
                //Display Menu (which handles game play) until the user chooses to exit
                while (true)
                {
                    HomeNavigation(path);
                }
            }
            //If any exception occurs with system I/O, exit game
            catch
            {
                Console.WriteLine("Error occurred during file access. Exiting Game.");
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
            string menuOptions = "\n\n1: PLAY\n2: SEE WORDS\n3: ADD WORD\n4: DELETE WORD\n5: EXIT";

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
                        GamePlay(path);
                        break;
                    //Show words
                    case 2:
                        string[] words = GetWordsFromFile(path);
                        DisplayAllWords(words);
                        break;
                    //Add word
                    case 3:
                        Console.WriteLine("\nAdd chosen.");
                        word = GetAndValidateWordForAddDelete();
                        AppendWordToFile(path, word);
                        Console.WriteLine($"{word} successfully added.");
                        break;
                    //Delete word
                    case 4:
                        Console.WriteLine("\nDelete chosen.");
                        word = GetAndValidateWordForAddDelete();
                        DeleteWordFromFile(path, word);
                        Console.WriteLine($"{word} successfully deleted.");
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
            Console.WriteLine();
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
        public static string GetAndValidateLetterGuess()
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
            return guess;
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

        /// <summary>
        /// Controls the game play: present user with word and allows the user
        /// to guess letters until the full word has been guessed
        /// </summary>
        /// <param name="path">Path to the file of possible mystery words</param>
        public static void GamePlay(string path)
        {
            string pastGuesses = ""; //Tracks user letter guesses
            string[] words = GetWordsFromFile(path);
            string word = GetRandomWord(words);

            //Create mystery string
            char[] mysteryWordArray = CreateMysteryString(word);
            bool wordGuessed = FullWordGuessed(mysteryWordArray);

            while(!wordGuessed)
            {
                //Display results
                DisplayCurrentGameState(mysteryWordArray, pastGuesses);
                string guess = GetAndValidateLetterGuess();
                //Process letter guess and update pastGuesses
                //this bool "correctGuess" is not currently used, but could be useful if the game is refactored
                bool correctGuess = LetterInWord(guess, word, mysteryWordArray);
                pastGuesses += guess;
                //Check if full word has been guessed
                wordGuessed = FullWordGuessed(mysteryWordArray);
            }

            //Display full word and present options
            DisplayCurrentGameState(mysteryWordArray, pastGuesses);
            Console.WriteLine("\n\n\n*** Congratulations! You've guessed the whole word! ***");
            Console.WriteLine();
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

        /// <summary>
        /// Returns a string of underscores the same length as the mystery word
        /// </summary>
        /// <param name="word">The word the user will guess</param>
        /// <returns>A char array of underscores the same length as the mystery word</returns>
        public static char[] CreateMysteryString(string word)
        {
            string mysteryString = "";
            for(int i=0; i < word.Length; i++)
            {
                mysteryString += "_";
            }
            return mysteryString.ToCharArray();
        }

        /// <summary>
        /// Processes the user's guess, adding that letter to the mystery
        /// string for each occurrence in the original word. Also adds the user's guess
        /// to the string of past guesses
        /// </summary>
        /// <param name="guess">The letter entered by the user</param>
        /// <param name="word">The mystery word</param>
        /// <param name="mysteryWord">The mystery string of underscores</param>
        /// <return>True if the letter was in the word, otherwise false</return>
        public static bool LetterInWord(string guess, string word, char[] mysteryWord)
        {
            bool flag = false; //For tracking whether the guess is in the mystery word
            //Replace occurrences of the guess in the mysteryWord array
            for(int i=0; i < word.Length; i++)
            {
                if(char.ToUpper(word[i]) == char.ToUpper(guess[0]))
                {
                    mysteryWord[i] = char.ToLower(guess[0]);
                    flag = true;
                }
            }
            if (flag) return true;
            return false;
        }

        /// <summary>
        /// Checks whether the entire mystery string has been guessed
        /// </summary>
        /// <param name="mysteryString">The "mystery" string that represents the word to be guessed</param>
        /// <returns>True if there are no more underscores, else returns false</returns>
        public static bool FullWordGuessed(char[] mysteryString)
        {
            for(int i=0; i < mysteryString.Length; i++)
            {
                if (mysteryString[i] == '_') return false;
            }
            return true;
        }

        /// <summary>
        /// Displays the mystery string and the user's past guesses
        /// </summary>
        /// <param name="mysteryWord">The array used to track the mystery word</param>
        /// <param name="pastGuesses">The user's past letter guesses</param>
        public static void DisplayCurrentGameState(char[] mysteryWord, string pastGuesses)
        {
            //Display the mystery string
            Console.Write("\n\n");
            Console.Write("--> Mystery Word: ");
            for(int i=0; i < mysteryWord.Length; i++)
            {
                Console.Write($"{mysteryWord[i]} ");
            }
            Console.WriteLine();

            //Display guessses so far
            Console.Write("\n--> Letters guessed: ");
            for(int i = 0; i < pastGuesses.Length; i++)
            {
                Console.Write($"{pastGuesses[i]} ");
            }
        }

    }
}
