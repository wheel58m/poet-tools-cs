// Austin Wheeler
// CS 1400 Final Project
// 

// NOTES ----------------------------------------------------------------------/
/*
 * ISSUE: FindRhyme Method does not display a message if the input word is not found in the dictionary.
 * ISSUE: FindRhyme Method does not display a message if there are no rhymes for the input word.
 * ISSUE: Rhymes are Soley Based on Ending Sounds and Do Not Include Complex Rhymes Like Unconventional or Internal Rhymes.
*/

// MAIN -----------------------------------------------------------------------/
Dictionary<string, Tuple<List<string>, int>> pronunciationDictionary = LoadPronunciationDictionary();
bool error = false; // Store Error Status
string msg = ""; // Store Any Error or Success Messages

// Display Menu ---------------------------------------------------------------/
while (true) {
    Console.Clear();
    Console.WriteLine("Please select a poetry tool below: ");

    // Display Any Error or Success Messages ----------------------------------/
    if (error) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
        error = false;
        msg = "";
    } else {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(msg);
        Console.ResetColor();
        msg = "";
    }

    // Display Menu Options ---------------------------------------------------/
    Console.WriteLine("1. Find Rhyming Words");
    Console.WriteLine("2. Lookup a Word's Pronunciation");
    Console.WriteLine("3. Lookup a Word's Definition");
    Console.WriteLine("4. Generate Poem from Template");
    Console.WriteLine("5. Count Syllables in a Word or Phrase");
    Console.WriteLine("6. Count Words in a Phrase");
    Console.WriteLine("7. Exit Program");
    Console.WriteLine();
    Console.Write("Enter a number to select a tool: ");
    char input = Console.ReadKey().KeyChar;

    switch (input) {
        // Find Rhyming Words -------------------------------------------------/
        case '1':
            while (true) {
                // Ask User for Input Word
                Console.Clear();
                Console.Write("Enter a word to find rhymes: ");
                string inputWord = Console.ReadLine();

                // Find & Display Rhymes
                FindRhyme(inputWord);

                // Prompt User to Find Another Rhyme or Exit
                if (ContinuePrompt("Would you like to find another rhyme? (y/n) ", true)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        // Lookup a Word's Pronunciation --------------------------------------/
        case '2':
            while (true) {
                // Ask User for Input Word
                Console.Clear();
                Console.Write("Enter a word to lookup pronunciation: ");
                string inputWord = Console.ReadLine();

                // Find Pronunciation
                string[] pronunciationArray = FindPronunciation(inputWord);
                
                // Display Pronunciation
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{inputWord.ToUpper()}:");
                foreach (string pronunciation in pronunciationArray) {
                    Console.Write($" {pronunciation}");
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();

                // Prompt User to Lookup Another Word or Exit
                if (ContinuePrompt("Would you like to lookup another word? (y/n) ", false)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        // Lookup a Word's Definition -----------------------------------------/
        case '3':
            while (true) {
                // Ask User for Input Word
                Console.Clear();
                Console.Write("Enter a word to lookup definition: ");
                string inputWord = Console.ReadLine();

                // Find Definition
                string definition = LookupDefinition(inputWord);

                // Display Definition
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{inputWord} - {definition}");
                Console.ResetColor();
                Console.WriteLine();

                // Prompt User to Lookup Another Word or Exit
                if (ContinuePrompt("Would you like to lookup another word? (y/n) ", false)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        // Generate Poem from Template ----------------------------------------/
        case '4':
            while (true) {
                // Ask User for Template and Generate Poem
                string[] poem = GeneratePoem();

                // Check for Blank Poem
                if (poem.Length == 0) {
                    break;
                } else {
                    // Display Poem
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    foreach (string line in poem) {
                        Console.WriteLine(line);
                    }
                    Console.ResetColor();
                    Console.WriteLine();

                    // Promt User to Save Poem or Exit
                    if (ContinuePrompt("Would you like to save your poem? (y/n) ", false)) {
                        SavePoem(poem);
                    } else {
                        break;
                    }
                }
            }
            break;
        // Count Syllables in a Word or Phrase --------------------------------/
        case '5':
            while (true) {
                // Ask User for Input Word or Phrase
                Console.Clear();
                Console.Write("Enter a word or phrase to count syllables: ");
                string inputPhrase = Console.ReadLine();

                // Count Syllables
                int syllableCount = SyllableCounter(inputPhrase);

                // Display Syllable Count
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"There are {syllableCount} syllables in the phrase \"{inputPhrase}\".");
                Console.ResetColor();
                Console.WriteLine();

                // Prompt User to Count Another Word/Phrase or Exit
                if (ContinuePrompt("Would you like to count syllables in another phrase? (y/n) ", false)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        // Count Words in a Phrase --------------------------------------------/
        case '6':
            while (true) {
                // Ask User for Input Phrase
                Console.Clear();
                Console.Write("Enter a phrase to count words: ");
                string inputPhrase = Console.ReadLine();

                // Count Words
                int wordCount = WordCounter(inputPhrase);

                // Display Word Count
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"There are {wordCount} words in the phrase \"{inputPhrase}\"");
                Console.ResetColor();
                Console.WriteLine();

                // Prompt User to Count Another Phrase or Exit
                if (ContinuePrompt("Would you like to count words in another phrase? (y/n) ", false)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        // Exit Program -------------------------------------------------------/
        case '7':
            Console.Clear();
            return;
        // Invalid Option -----------------------------------------------------/
        default:
            error = true;
            msg = "Invalid Option. Please Try Again. ";
            break;
    }
}

// METHOD: CONTINUE PROMPT ----------------------------------------------------/
static bool ContinuePrompt(string prompt, bool clear) {
    // Clear Console
    if (clear) {
        Console.Clear();
    }

    while (true) {
        // Display Prompt
        Console.Write(prompt);
        char input = Console.ReadKey().KeyChar;

        if (input == 'y' || input == 'Y') {
            return true;
        } else if (input == 'n' || input == 'N') {
            return false;
        } else {
            // Display Error Message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Option. Please Try Again. ");
            Console.ResetColor();
            continue;
        }
    }
}

// METHOD: EXIT PROMPT --------------------------------------------------------/
static bool ExitPrompt(string prompt, bool clear) {
    // Clear Console
    if (clear) {
        Console.Clear();
    }

    // Display Prompt
    Console.Write(prompt);
    Console.ReadKey(true); // Accept Any Key Press
    return true;
}

// METHOD: LOAD PRONUNCIATION DICTIONARY --------------------------------------/
// Load Pronunciation Dictionary File into a Dictionary Variable that can be used throughout the entire program.
static Dictionary<string, Tuple<List<string>, int>> LoadPronunciationDictionary() {
    // Read in the CMU Pronouncing Dictionary ---------------------------------/
    string dictFile = "pronunciations.txt"; // Store Filename of Phonetic Dictionary
    string[] pronunciations = File.ReadAllLines(dictFile);

    // Create Dictionary to Store Pronunciations ------------------------------/
    Dictionary<string, Tuple<List<string>, int>> pronunciationDictionary = new Dictionary<string, Tuple<List<string>, int>>();

    // Loop Through Each Pronunciation and Add to Dictionary ------------------/
    foreach (string pronunciation in pronunciations) {
        if (pronunciation.StartsWith(";;;")) continue; // Skip File Comments

        string[] terms = pronunciation.Split(" "); // Split Pronunciation into Terms
        string word = terms[0]; // Store Word
        List<string> sounds = new List<string>(); // Store Sounds
        int soundCount = terms.Length - 2; // Store Number of Sounds

        // Loop Through Each Sound and Add to List ----------------------------/
        for (int i = 2; i < terms.Length; i++) {
            sounds.Add(terms[i]);
        }

        // Add Word and Sounds to Dictionary ----------------------------------/
        pronunciationDictionary.Add(word, new Tuple<List<string>, int>(sounds, soundCount));
    }

    return pronunciationDictionary;
}

// METHOD: FIND RHYME ---------------------------------------------------------/
void FindRhyme(string inputWord) {
    inputWord = inputWord.ToUpper();

    // Find Input Word and Its Sounds in the Phonetic Dictionary --------------/
    List<string> inputSounds = new List<string>(); // List of Sounds in Given Word
    int inputSoundCount = 0; // Number of Sounds in Given Word

    if (pronunciationDictionary.ContainsKey(inputWord)) {
        inputSounds = pronunciationDictionary[inputWord].Item1;
        inputSoundCount = pronunciationDictionary[inputWord].Item2;
    }

    // Add Words that Rhyme with the Input Word to the Rhymes List ------------/
    List<(string RhymingWord, int RhymeCount)> rhymes = new List<(string, int)>(); // List Storing Rhyming Words in a Tuple with the Rhyming Word and Number of Matching Ending Sounds.

    foreach (KeyValuePair<string, Tuple<List<string>, int>> pronunciation in pronunciationDictionary) {
        string outputWord = pronunciation.Key; // Store Word
        List<string> outputSounds = pronunciation.Value.Item1; // Store Sounds
        int outputSoundCount = pronunciation.Value.Item2; // Store Number of Sounds
        int matchingSoundCount = 0;

        for (int i = 1; i <= inputSoundCount && i <= outputSoundCount; i++) {
            string inputSound = inputSounds[^i];
            string outputSound = outputSounds[^i];

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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"> {rhyme.Item1} rhymes with {inputWord} on {rhyme.Item2} sounds.");
            Console.ResetColor();
        }

        // Ask User if They Want to See More Rhymes
        if (endIndex < rhymingWordCount - 1) {
            findingRhymes = ContinuePrompt("\nWould you like to see more rhymes? (y/n): ", false);

            // Update Indexes
            startIndex += 10;
            if (endIndex + 10 < rhymingWordCount - 1) {
                endIndex += 10;
            } else {
                endIndex = rhymingWordCount - 1;
            }
        } else {
            findingRhymes = !ExitPrompt("\nPress any key to exit: ", false);
        }
    }
}

// METHOD: FIND PRONUNCIATION -------------------------------------------------/
string[] FindPronunciation(string inputWord) {
    inputWord = inputWord.ToUpper(); // Convert Word to Uppercase for Case Matching in Dictionary

    // Find Input Word and Its Sounds in the Phonetic Dictionary --------------/
    string[] pronunciation = new string[] { }; // Array to Store Pronunciation

    if (pronunciationDictionary.ContainsKey(inputWord)) {
        pronunciation = pronunciationDictionary[inputWord].Item1.ToArray();
    }

    return pronunciation;
}

// METHOD: LOOKUP DEFINITION --------------------------------------------------/
string LookupDefinition(string inputWord) {
    string dictFile = "dictionary.txt";
    string definition = "";

    // Search Dictionary for Word ---------------------------------------------/
    string[] dictionary = File.ReadAllLines(dictFile);

    foreach (string line in dictionary) {
        string[] terms = line.Split(" ");
        string word = terms[0];

        if (word == inputWord) {
            definition = line;
            break;
        }
    }

    return definition;
}

// METHOD: WORD COUNTER -------------------------------------------------------/
int WordCounter(string input) {
    int wordCount = 0;

    // Count Words in Input ---------------------------------------------------/
    string[] words = input.Split(" ");

    foreach (string word in words) {
        if (word != "") {
            wordCount++;
        }
    }

    return wordCount;
}

// METHOD: SYLLABLE COUNTER ---------------------------------------------------/
int SyllableCounter(string input) {
    int syllableCount = 0;
    string[] words = input.Split(" "); // Split Input into Words

    // Count Syllables in Each Word -------------------------------------------/
    foreach (string word in words) {
        int wordSyllables = 0;
        bool lastWasVowel = false;

        foreach (char c in word) {
            // Check if Character is a Vowel
            bool isVowel = "aeiouy".Contains(c);

            // If Character is a Vowel and the Last Character Was Not a Vowel, Add a Syllable
            if (isVowel && !lastWasVowel) {
                wordSyllables++;
            }

            lastWasVowel = isVowel;
        }

        // Check for Silent "e" at the End of the Word
        if (word.EndsWith("e")) {
            wordSyllables--;
        }

        // Check for One Syllable Words
        if (wordSyllables == 0) {
            wordSyllables = 1;
        }

        // Add Syllables in Word to Total Syllable Count
        syllableCount += wordSyllables;
    }

    return syllableCount;
}

// METHOD: GENERATE POEM ------------------------------------------------------/
string[] GeneratePoem() {
    bool error = false;
    string errorMessage = "";
    string[] poem = new string[] { }; // Initialize Array to Store Poem

    while (true) {
        Console.Clear();

        // Display Instructions -----------------------------------------------/
        Console.WriteLine("Please select a poem type:");

        // Display Any Error Messages -----------------------------------------/
        if (error) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
            error = false;
            errorMessage = "";
        }

        // Display Menu Options -----------------------------------------------/
        Console.WriteLine("1. Haiku");
        Console.WriteLine("2. Limerick");
        Console.WriteLine("3. Sonnet");
        Console.WriteLine("4. Return to Main Menu");
        Console.WriteLine();
        Console.Write("Please enter a number: ");
        ConsoleKeyInfo key = Console.ReadKey();

        // Generate Selected Poem ---------------------------------------------/
        switch (key.KeyChar) {
            // Generate Haiku
            case '1':
                return poem = GenerateHaiku();
            // Generate Limerick
            case '2':
                // poem = GenerateLimerick();
                break;
            // Generate Sonnet
            case '3':
                // poem = GenerateSonnet
                break;
            // Go Back to Main Menu
            case '4':
                return new string[0];
            // Invalid Input
            default:
                error = true;
                errorMessage = "Invalid input. Please try again.";
                break;
        }
    }
}

// METHOD: GENERATE HAIKU -----------------------------------------------------/
string[] GenerateHaiku() {
    string[] haiku = new string[3];
    haiku[0] = SyllablePhraseGenerator(5); // Generate 5 Syllable Phrase
    haiku[1] = SyllablePhraseGenerator(7); // Generate 7 Syllable Phrase
    haiku[2] = SyllablePhraseGenerator(5); // Generate 5 Syllable Phrase
    return haiku;
}

// METHOD: SYLLABLE PHRASE GENERATOR ------------------------------------------/
string SyllablePhraseGenerator(int maxSyllables) {
    string phrase = "";
    int syllableCount = 0;

    // Initialize Loop for Phrase Input ---------------------------------------/
    while (syllableCount < maxSyllables) {
        Console.Clear();
        // Print Instructions
        Console.WriteLine("Follow the prompts to generate a haiku.");
        Console.Write($"Please enter {maxSyllables} syllable phrase ");
        Console.Write($"{syllableCount}/{maxSyllables}: "); // Display Current Syllable Count

        // Get User Input
        bool completeWord = false;
        string word = "";

        // Initialize Loop for Word Input -------------------------------------/
        while (!completeWord) {
            ConsoleKeyInfo key = Console.ReadKey();

            // Check if Word is Complete (User Pressed Space or Enter)
            if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter) {
                completeWord = true;
            } else {
                word += key.KeyChar;
            }
        }
        syllableCount += SyllableCounter(word); // Add Syllables in Word to Total Syllable Count

        // Add Word to Phrase
        phrase += word + " ";
    }

    phrase = phrase.TrimEnd(); // Remove Trailing Space
    return phrase;
}

// METHOD: SAVE POEM ----------------------------------------------------------/
void SavePoem(string[] poem) {
    Console.Clear();
    Console.WriteLine("Please enter a file name: ");
    string fileName = Console.ReadLine();

    // Write Poem to File -----------------------------------------------------/
    string filePath = $"poems/{fileName}.txt";
    File.WriteAllLines(filePath, poem);
}