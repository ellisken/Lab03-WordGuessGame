using System;
using System.IO;
using WordGuessGame;
using Xunit;

namespace TestWordGuessGame
{
    public class UnitTest1
    {
        /* REQUIRED:
         * Test letter exists method valid and invalid
         */

        //Test successful file creation in same dir as Program.cs
        [Fact]
        public void TestFileCreation()
        {
            string path = "../../../testFile.txt";
            string[] words = {
                "Hello",
                "World",
                "I",
                "am",
                "file",
            };

            Program.OverwriteOrCreateFile(path, words);

            Assert.True(File.Exists(path));
        }

        //Test incorrect path results in exception during file creation/writing
        [Fact]
        public void TestFileCreationInvalidPath()
        {
            string path = "../../../testFile***";
            string[] words = {
                "Hello",
                "World",
                "I",
                "am",
                "not",
                "here"
            };

            Exception exception = Record.Exception(() => Program.OverwriteOrCreateFile(path, words));
            Assert.NotNull(exception);
            Assert.IsType<System.IO.IOException>(exception);
        }

        //Test append to file success
        [Fact]
        public void TestAppendToFileSuccess()
        {
            string path = "../../../testFile.txt";
            string word = "newWord";

            try
            {
                Program.AppendWordToFile(path, word);
            }
            catch
            {
                throw;
            }
        }

        //Test append to file success
        [Fact]
        public void TestAppendToFileFailure()
        {
            string path = "../../../testFile";
            string word = "newWord";

            Exception exception = Record.Exception(() => Program.AppendWordToFile(path, word));
            Assert.NotNull(exception);
            Assert.IsType<System.Exception>(exception);
        }

        //Test can retrieve all words from file
        [Fact]
        public void TestGetWordsFromFileValidPath()
        {
            string path = "../../../testFile2.txt";
            string[] words = {
                "I",
                "am",
                "another",
                "test",
                "file",
            };

            //Call method to create file with expected contents
            Program.OverwriteOrCreateFile(path, words);
            //Get contents with GetWordsFromFile
            string[] retrievedWords = Program.GetWordsFromFile(path);
            //Check size is same
            Assert.Equal(words.Length, retrievedWords.Length);
        }

        //Test new word added to file
        [Fact]
        public void TestAppendToFileWordAdded()
        {
            string path = "../../../testFile.txt";
            string word = "newWord";

            string[] wordsBeforeAppend = Program.GetWordsFromFile(path);
            Program.AppendWordToFile(path, word);
            string[] retrievedWords = Program.GetWordsFromFile(path);
            Assert.Equal(wordsBeforeAppend.Length + 1, retrievedWords.Length);

        }

        //Test delete from file success
        [Fact]
        public void TestDeleteFromFileSuccess()
        {
            string path = "../../../testFile2.txt";
            string[] words = {
                "I",
                "am",
                "another",
                "test",
                "file",
            };

            string wordToDelete = "another";

            //Call method to create file with expected contents
            Program.OverwriteOrCreateFile(path, words);

            //Run Delete to remove 1 line from words
            Program.DeleteWordFromFile(path, wordToDelete);

            //Get all words from file
            string[] wordsWithoutDeletedWord = Program.GetWordsFromFile(path);

            //Check old and new words.Length
            Assert.Equal(words.Length - 1, wordsWithoutDeletedWord.Length);
        }

        //Test letter exists method valid
        [Fact]
        public void TestLetterInWord()
        {
            string letter = "l";
            string word = "hello";
            char[] mysteryString = { 'h', '_', '_', '_', 'o' };

            Assert.True(Program.LetterInWord(letter, word, mysteryString));
        }

        //Test letter exists invalid
        [Fact]
        public void TestLetterNotInWord()
        {
            string letter = "y";
            string word = "hello";
            char[] mysteryString = { 'h', '_', '_', '_', 'o' };

            Assert.False(Program.LetterInWord(letter, word, mysteryString));
        }
    }
}
