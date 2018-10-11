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
        /// Creates a file with the given path and writes each word from
        /// the array of words to the file on its own line
        /// </summary>
        /// <param name="path">The path where the file exists or should be created</param>
        /// <param name="words">An array of words that should be added to the file</param>
        public static void WriteToOrCreateFile(string path, string[] words)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
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
    }
}
