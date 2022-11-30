using NUnit.Framework;
using WordCounting.CharacterIdentification;
using WordCounting.Counting;

namespace WordCounting.Tests
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

        [Test]
        public void Counts_Words_When_Multiple_Strings_Are_Passed_In()
        {
            string text = "Hello World";

            var wordCounter = CreateWordCounter();
            var results = wordCounter.Count(text, text);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(2, results["Hello"]);
            Assert.AreEqual(2, results["World"]);
        }

        [Test]
        public void IsPresentWordCountMethod_Stops_At_One_When_There_Are_Multiple_Matches()
        {
            string text = "Hello World Hello World";

            var wordCounter = CreateWordCounter(new IsPresentWordCountMethod());
            var results = wordCounter.Count(text, text);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(1, results["Hello"]);
            Assert.AreEqual(1, results["World"]);
        }

        [Test]
        public void IsPresentWordCountMethod_Gives_Total_Presence_Count_Across_All_Input_Strings_When_MergeResults_Is_True()
        {
            string text = "Hello World Hello World";

            var wordCounter = CreateWordCounter(new IsPresentWordCountMethod(), true);
            var results = wordCounter.Count(text, text);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(2, results["Hello"]);
            Assert.AreEqual(2, results["World"]);
        }

        private WordCounter CreateWordCounter(IWordCountMethod wordCountMethod = null, bool mergeResults = false)
        {
            return new WordCounter(new CharacterIdentifier(), wordCountMethod, mergeResults);
        }
    }
}