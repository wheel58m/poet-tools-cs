This is a C# program that helps users find rhyming words for a given input word. The program uses the CMU Pronouncing Dictionary to identify the rhyming syllable for the input word, and then finds all the words in the dictionary that share the same rhyming syllable.

## Issues

Currently, the `FindRhyme` method writes to the console too many times, erasing/hiding previous console messages.

## Instructions

1.  Clone the repository.
2.  Open the project in your C# IDE of choice.
3.  Build and run the program.
4.  When prompted, enter the word you want to find a rhyme for.
5.  The program will display the rhyming syllable and a list of all the words in the CMU Pronouncing Dictionary that share that same rhyming syllable.

## Future Improvements

-   Address the issue of the `FindRhyme` method writing to the console too many times.
-   Display the top 10 best rhyming words (words with the most rhyming syllables).
-   Create a 'Syllable Counter' method to display the number of syllables in a given word.
-   Create a 'Word Counter' method to count the number of words in a line.
-   Create a 'Get Poem Template' method to generate and display templates for different poem types (Sonnets, Haikus, Limerick, etc.).
-   Create a 'Define Word' method to return the definition of a given word.