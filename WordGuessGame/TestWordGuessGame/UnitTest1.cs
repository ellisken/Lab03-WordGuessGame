using System;
using System.IO;
using WordGuessGame;
using Xunit;

namespace TestWordGuessGame
{
    public class UnitTest1
    {
        /* REQUIRED:
         * Test file can be created
         * Test file can be updated
         * Test file can be deleted
         * Test word can be added to file
         * Test retrieve all words from file
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

        //Test delete from file failure
    }
}
