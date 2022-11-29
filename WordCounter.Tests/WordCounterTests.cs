using NUnit.Framework;

namespace WordCounter.Tests
{
    public class Tests
    {
        [Test]
        public void Counts_Words_In_A_Simple_Sentence_That_Has_Only_Letters()
        {
            string text = "Hello World";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(1, results["Hello"]);
            Assert.AreEqual(1, results["World"]);
        }

        private WordCounter CreateWordCounter()
        {
            return new WordCounter(new CharacterIdentifier());
        }
    }
}