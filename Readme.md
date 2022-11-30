# Word Counter
Counts up the number of times each word occurs (case insensitive) in a string of English text.

## Basic Usage

```
var wordCounter = new WordCounter();
var results = wordCounter.Count("Hello World!");
```

## Advanced Usage

Some of the logic for the word counter is customizable through the use of input parameters to the `WordCounter` constructor. The available options are:

`ICharacterIdentification`: Specifies what characters are considered to be part of a word.

`IWordCountMethod`: Used to figure out how the count should be incremented when a word is encountered. There are two available options. The default option just increments the count and the second option is `IsPresentWordCountMethod` which will cause the count to stop at one no matter how many times a word occurs.