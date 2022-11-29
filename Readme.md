# Word Counter
Counts up the number of times each word occurs (case insensitive) in a string of English text.

## Basic Usage

```
var wordCounter = new WordCounter();
var results = wordCounter.Count("Hello World!");
```

## Advanced Usage

Some of the logic for the word counter is customizable. The `WordCounter` class can take an input parameter of type `ICharacterIdentification` to specify what characters are considered to be part of a word. 