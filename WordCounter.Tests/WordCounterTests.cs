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
            string text = "hello.hello?hello!hello;hello:hello hello\thello\r\nhello\"hello\"";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(10, results["hello"]);
        }

        [Test]
        public void Counts_Words_When_There_Is_Internal_Punctuation()
        {
            string text = "cant can't can-t";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text);

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(1, results["cant"]);
            Assert.AreEqual(1, results["can't"]);
            Assert.AreEqual(1, results["can-t"]);
        }

        private WordCounter CreateWordCounter()
        {
            return new WordCounter(new CharacterIdentifier());
        }
    }
}