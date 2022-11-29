using NUnit.Framework;
using WordCounter.CharacterIdentification;

namespace WordCounter.Tests
{
    public class Tests
    {
        [Test]
        public void Counts_Words_When_There_Are_Only_Letters()
        {
            string text = "Hello World";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(1, results["Hello"]);
            Assert.AreEqual(1, results["World"]);
        }

        [Test]
        public void Ignores_Case_When_Counting_Words()
        {
            string text = "Hello heLlO";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(2, results["Hello"]);
        }

        [Test]
        public void Counts_Words_When_There_Is_External_Punctuation()
        {
            string text = "hello.hello?hello!hello;hello: hello";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(6, results["hello"]);
        }

        private WordCounter CreateWordCounter()
        {
            return new WordCounter(new CharacterIdentifier());
        }
    }
}