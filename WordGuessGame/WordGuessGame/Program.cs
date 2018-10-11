using System;
using System.IO;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Josie Cat's Word Guessing Game!");
            
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
                string[] mysteryWords = File.ReadAllLines(path);
                return mysteryWords;
            }
            catch (Exception)
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
                        sw.WriteLine(words[i]);
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
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        try
                        {
                            Console.WriteLine("appending to file");
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


    }


}
