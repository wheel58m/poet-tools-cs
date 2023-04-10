// Austin Wheeler
// CS 1400 Final Project
// 

// NOTES ----------------------------------------------------------------------/
/*
 * ISSUE: FindRhyme Method does not display a message if the input word is not found in the dictionary.
 * ISSUE: FindRhyme Method does not display a message if there are no rhymes for the input word.
 * ISSUE: Rhymes are Soley Based on Ending Sounds and Do Not Include Complex Rhymes Like Unconventional or Internal Rhymes.
*/

// RUN FINDRHYME METHOD: Ask User for a Word ----------------------------------/
while (true) {
    Console.Clear();
    Console.Write("Please type the word you want a rhyme for: ");
    string? inputWord = Console.ReadLine();

    if (inputWord != null) {
        Console.Clear();
        FindRhyme(inputWord);
    }
}

// METHOD: Continue/Repeat Method ---------------------------------------------/
static bool Continue(string prompt, bool exit) {
    switch (exit) {
        // Exit Only ----------------------------------------------------------/
        case true:
            Console.Write(prompt);
            Console.ReadKey(true); // Accept Any Key Press
            return false;
        // Continue or Exit ---------------------------------------------------/
        case false:
            while (true) {
                Console.Write(prompt);
                char input = Console.ReadKey().KeyChar;

                if (input == 'y' || input == 'Y') {
                    return true;
                } else if (input == 'n' || input == 'N') {
                    return false;
                } else {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nInvalid Option. Please Try Again. ");
                    Console.ResetColor();
                }
            }
    }
}

// METHOD: Find Rhyming Word --------------------------------------------------/
static void FindRhyme(string inputWord) {
    inputWord = inputWord.ToUpper();

    // Read in the CMU Pronouncing Dictionary ---------------------------------/
    string dictFile = "cmudict-0.7b"; // Save Filename of Phonetic Dictionary
    string[] pronunciations = File.ReadAllLines(dictFile);

    // Find Input Word and Its Sounds in the Phonetic Dictionary --------------/
    List<string> inputSounds = new List<string>(); // List of Sounds in Given Word
    int inputSoundCount = 0; // Number of Sounds in Given Word

    foreach (string pronunciation in pronunciations) {
        if (pronunciation.StartsWith(";;;")) continue; // Skip File Comments
        string[] terms = pronunciation.Split(" ");

        if (terms[0] == inputWord) {
            inputSoundCount = terms.Length - 2; // Exclude Word & Whitespace

            for (int i = 2; i < terms.Length; i++) {
                inputSounds.Add(terms[i]); // Add Input Word Sounds to List
            }

            break; // Stop Searching for Input Word
        }
    }

    // Add Words that Rhyme with the Input Word to the Rhymes List ------------/
    List<(string RhymingWord, int RhymeCount)> rhymes = new List<(string, int)>(); // List Storing Rhyming Words in a Tuple with the Rhyming Word and Number of Matching Ending Sounds.

    foreach (string pronunciation in pronunciations) {
        if (pronunciation.StartsWith(";;;")) continue; // Skip File Comments

        string[] terms = pronunciation.Split(" ");
        int outputSoundCount = terms.Length - 2; // Exclude Word & Whitespace
        string outputWord = terms[0];
        int matchingSoundCount = 0;

        for (int i = 1; i <= inputSoundCount && i <= outputSoundCount; i++) {
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

    // Sort Rhyme List by the Number of Matching Sounds -----------------------/
    rhymes.Sort((x, y) => y.RhymeCount.CompareTo(x.RhymeCount));

    // Display List of Rhyming Words ------------------------------------------/
    int rhymingWordCount = rhymes.Count();

    // Set Indexes for Displaying Rhymes
    int startIndex = 0;
    int endIndex = 0;
    // Check if There are Less Than 10 Rhymes
    if (rhymingWordCount < 10) {
        endIndex = rhymingWordCount - 1;
    } else {
        endIndex = 9;
    }

    bool findingRhymes = true; // Intialize Loop Condition

    // Print List of Rhymes
    while (findingRhymes) {
        Console.Clear();
        Console.WriteLine($"Here is a list of {rhymingWordCount} possible rhyming word (showing {startIndex + 1} through {endIndex + 1}):");

        for (int i = startIndex; i <= endIndex; i++) {
            (string, int) rhyme = rhymes[i];
            Console.WriteLine($"> {rhyme.Item1} rhymes with {inputWord} on {rhyme.Item2} sounds.");
        }

        // Ask User if They Want to See More Rhymes
        if (endIndex < rhymingWordCount - 1) {
            findingRhymes = Continue("\nWould you like to see more rhymes? (y/n): ", false);

            // Update Indexes
            startIndex += 10;
            if (endIndex + 10 < rhymingWordCount - 1) {
                endIndex += 10;
            } else {
                endIndex = rhymingWordCount - 1;
            }
        } else {
            findingRhymes = Continue("\nPress any key to exit: ", true);
        }
    }
}