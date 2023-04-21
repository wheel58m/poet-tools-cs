// Austin Wheeler
// CS 1400 Final Project
// 

// NOTES ----------------------------------------------------------------------/
/*
 * TODO: Break ContinueOrExit Into Two Methods.
 * ISSUE: FindRhyme Method does not display a message if the input word is not found in the dictionary.
 * ISSUE: FindRhyme Method does not display a message if there are no rhymes for the input word.
 * ISSUE: Rhymes are Soley Based on Ending Sounds and Do Not Include Complex Rhymes Like Unconventional or Internal Rhymes.
*/

// MAIN -----------------------------------------------------------------------/
Dictionary<string, Tuple<List<string>, int>> pronunciationDictionary = LoadPronunciationDictionary();
bool error = false; // Store Error Status
string msg = ""; // Store Any Error or Success Messages

// Test FindRhyme Method
while (true) {
    Console.Clear();
    Console.WriteLine("Please select a poetry tool below: ");
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
    Console.WriteLine(msg); // Display Any Error or Success Messages
    Console.WriteLine("1. Find Rhyming Words");
    Console.WriteLine("2. Lookup a Word's Pronunciation");
    Console.WriteLine("3. Lookup a Word's Definition");
    Console.WriteLine("4. Select a Poetry Template");
    Console.WriteLine("5. Count Syllables in a Word or Phrase");
    Console.WriteLine("6. Count Words in a Phrase");
    Console.WriteLine("7. Exit Program");
    Console.WriteLine();
    Console.Write("Enter a number to select a tool: ");
    char input = Console.ReadKey().KeyChar;

    switch (input) {
        case '1':
            while (true) {
                Console.Clear();
                Console.Write("Enter a word to find rhymes: ");
                string inputWord = Console.ReadLine();
                FindRhyme(inputWord);

                if (ContinueOrExit("\nWould you like to find another rhyme? (y/n) ", false)) {
                    continue;
                } else {
                    break;
                }
            }
            break;
        case '2':
            break;
        case '3':
            break;
        case '4':
            break;
        case '5':
            break;
        case '6':
            break;
        case '7':
            Console.Clear();
            return;
        default:
            error = true;
            msg = "Invalid Option. Please Try Again. ";
            break;
    }
}

// METHOD: CONTINUE OR EXIT ---------------------------------------------------/
static bool ContinueOrExit(string prompt, bool exit) {
    switch (exit) {
        // Exit Only ----------------------------------------------------------/
        case true:
            Console.Write(prompt);
            Console.ReadKey(); // Accept Any Key Press
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

// METHOD: LOAD PRONUNCIATION DICTIONARY --------------------------------------/
// Load Pronunciation Dictionary File into a Dictionary Variable that can be used throughout the entire program.
static Dictionary<string, Tuple<List<string>, int>> LoadPronunciationDictionary() {
    // Read in the CMU Pronouncing Dictionary ---------------------------------/
    string dictFile = "cmudict-0.7b"; // Store Filename of Phonetic Dictionary
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

// METHOD: Find Rhyming Word --------------------------------------------------/
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
            Console.WriteLine($"> {rhyme.Item1} rhymes with {inputWord} on {rhyme.Item2} sounds.");
        }

        // Ask User if They Want to See More Rhymes
        if (endIndex < rhymingWordCount - 1) {
            findingRhymes = ContinueOrExit("\nWould you like to see more rhymes? (y/n): ", false);

            // Update Indexes
            startIndex += 10;
            if (endIndex + 10 < rhymingWordCount - 1) {
                endIndex += 10;
            } else {
                endIndex = rhymingWordCount - 1;
            }
        } else {
            findingRhymes = ContinueOrExit("\nPress any key to exit: ", true);
        }
    }
}