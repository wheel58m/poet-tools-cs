﻿// Austin Wheeler
// CS 1400 Final Project
// 

// NOTES ----------------------------------------------------------------------/
/*
 * ISSUE: Rhymes are Soley Based on Ending Sounds and Do Not Include Complex Rhymes Like Unconventional or Internal Rhymes.
*/

// RUN FINDRHYME METHOD: Ask User for a Word ----------------------------------/
while (true) {
    Console.Write("Please type the word you want a rhyme for: ");
    string? inputWord = Console.ReadLine();

    if (inputWord != null) {
        Console.Clear();
        FindRhyme(inputWord);
    }
}

// METHOD: Find Rhyming Word --------------------------------------------------/
static void FindRhyme(string inputWord) {
    inputWord = inputWord.ToUpper();
    List<string> inputSounds = new List<string>(); // List of Sounds in Given Word
    int inSoundCount = 0; // Number of Sounds in Given Word
    List<(string RhymingWord, int RhymeCount)> rhymes = new List<(string, int)>(); // List Storing Rhyming Words in a Tuple with the Rhyming Word and Number of Matching Ending Sounds.

    string dictFile = "cmudict-0.7b"; // Save Filename of Phonetic Dictionary
    string[] pronunciations = File.ReadAllLines(dictFile);

    // Find Input Word and Its Sounds in the Phonetic Dictionary
    foreach (string pronunciation in pronunciations) {
        if (pronunciation.StartsWith(";;;")) continue; // Skip File Comments
        string[] terms = pronunciation.Split(" ");

        foreach (string term in terms) {
            if (term == inputWord) {
                // Add Each Sound After the Matching Word and Whitespace to the Sounds List.
                for (int i = 2; i < terms.Length; i++) {
                    inputSounds.Add(terms[i]);
                    inSoundCount++;
                }
            }
        }
    }

    // Add Words that Rhyme with the Input Word to the Rhymes List
    foreach (string pronunciation in pronunciations) {
        if (pronunciation.StartsWith(";;;")) continue; // Skip File Comments

        string[] terms = pronunciation.Split(" ");
        int outSoundCount = terms.Length - 2; // Exclude Word & Whitespace
        string outputWord = terms[0];
        int matchingSoundCount = 0;

        for (int i = 1; i <= inSoundCount && i <= outSoundCount; i++) {
            string inputSound = inputSounds[^i];
            string outputSound = terms[^i];

            if (inputSound == outputSound) {
                matchingSoundCount++;
            } else {
                break;
            }
        }

        if (matchingSoundCount > 0) {
            rhymes.Add((outputWord, matchingSoundCount));
        }
    }

    // Sort Rhyme List by the Number of Matching Sounds
    rhymes.Sort((x, y) => y.RhymeCount.CompareTo(x.RhymeCount));

    // Display List of Rhyming Words
    int rhymingWordCount = rhymes.Count();
    int startIndex = 0; // Index of First Rhyming Word to Display
    int endIndex = 9; // Index of Last Rhyming Word to Display
    bool findingRhymes = true;

    // Print List of Rhymes
    while (findingRhymes) {
        Console.WriteLine($"Here is a list of {rhymingWordCount} possible rhyming word (showing {startIndex + 1} through {endIndex + 1}):");

        for (int i = startIndex; i < endIndex; i++) {
            (string, int) rhyme = rhymes[i];
            Console.WriteLine($"> {rhyme.Item1} rhymes with {inputWord} on {rhyme.Item2} sounds.");
        }
        findingRhymes = false;
    }
}