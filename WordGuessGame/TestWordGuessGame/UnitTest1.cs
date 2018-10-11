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

            Program.WriteToOrCreateFile(path, words);

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

            Exception exception = Record.Exception(() => Program.WriteToOrCreateFile(path, words));
            Assert.NotNull(exception);
            Assert.IsType<System.IO.IOException>(exception);
        }
    }
}
