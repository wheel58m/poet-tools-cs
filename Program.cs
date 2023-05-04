// Author: Austin Wheeler
// CS 1400 Final Project
// Submission Date: 5/4/2023

// DESCRIPTION ----------------------------------------------------------------/
/*
 * This program provides an editor and tools for writing poetry. It allows users
 * to create and open poems, find rhymes, lookup a words definition, count
 * syllables and words, and more.
*/

// MAIN METHOD ----------------------------------------------------------------/
Dictionary<string, Tuple<List<string>, int>> pronunciationDictionary = LoadPronunciationDictionary(); // Load Pronunciation Dictionary
bool error = false; // Store Error Status
string message = ""; // Store Error/Success Message

// Menu Loop ------------------------------------------------------------------/
while (true) {
    // Display Menu -----------------------------------------------------------/
    Console.Clear();
    Console.WriteLine("Welcome to the Poetry Editor!");
    ResponseMessage(message, error); // Display Error/Success Message
    Console.WriteLine("1. Generate Poem from Template");
    Console.WriteLine("2. Open Poem");
    Console.WriteLine("3. View Tools");
    Console.WriteLine("4. Exit");

    // Get User Input ---------------------------------------------------------/
    Console.WriteLine();
    Console.Write("Enter a number: ");
    char input = Console.ReadKey().KeyChar;

    // Load Menu Option -------------------------------------------------------/
    switch (input) {
        // Generate Poem from Template
        case '1':
            TemplateMenu();
            break;
        // Open Poem
        case '2':
            OpenPoem();
            break;
        // View Tools
        case '3':
            ToolsMenu();
            break;
        // Exit
        case '4':
            Console.Clear();
            return;
        // Invalid Input
        default:
            error = true;
            message = "Invalid Input. Please try again.";
            break;
    }
}

// RESPONSE MESSAGE METHOD ----------------------------------------------------/
void ResponseMessage(string message, bool error) {
    if (error) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        // Reset Error Status and Message
        error = false;
        message = "";
    } else {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        // Reset Success Message
        message = "";
    }
}

// USER PROMPT METHOD ---------------------------------------------------------/
bool UserPrompt(string prompt) {
    bool promptError = false; // Store Error Status
    string promptMessage = ""; // Store Error Message

    while (true) {
        Console.WriteLine();
        ResponseMessage(promptMessage, promptError); // Display Error Message
        Console.Write(prompt);
        char input = Console.ReadKey().KeyChar;

        if (input == 'Y' || input == 'y') {
            return true;
        } else if (input == 'N' || input == 'n') {
            return false;
        } else {
            Console.Clear();
            promptError = true;
            promptMessage = "Invalid Input. Please try again.";
        }
    }
}

// TEXT PROMPT METHOD ---------------------------------------------------------/
string TextPrompt(string prompt) {
    Console.Clear();
    Console.Write(prompt);
    Console.ForegroundColor = ConsoleColor.DarkGray;
    string? userInput = Console.ReadLine();
    Console.ResetColor();

    return userInput;
}

// EXIT PROMPT METHOD ---------------------------------------------------------/
bool ExitPrompt(string prompt) {
    Console.WriteLine();
    Console.Write(prompt);
    Console.ReadKey(true); // Wait for User Input
    return true;
}

// TEMPLATE MENU METHOD -------------------------------------------------------/
void TemplateMenu() {
    bool templateError = false; // Store Error Status
    string templateMessage = ""; // Store Error/Success Message

    // Menu Loop --------------------------------------------------------------/
    while (true) {
        // Display Menu -------------------------------------------------------/
        Console.Clear();
        Console.WriteLine("Generate Poem from Template");
        ResponseMessage(templateMessage, templateError); // Display Error/Success Message
        Console.WriteLine("1. Haiku");
        Console.WriteLine("2. Limerick");
        Console.WriteLine("3. Return to Main Menu");

        // Get User Input -----------------------------------------------------/
        Console.WriteLine();
        Console.Write("Enter a number: ");
        char templateInput = Console.ReadKey().KeyChar;

        // Load Menu Option ---------------------------------------------------/
        switch (templateInput) {
            // Generate Haiku
            case '1':
                GenerateHaiku();
                break;
            // Generate Limerick
            case '2':
                GenerateLimerick();
                break;
            // Return to Main Menu
            case '3':
                Console.Clear();
                return;
            // Invalid Input
            default:
                templateError = true;
                templateMessage = "Invalid Input. Please try again.";
                break;
        }
    }
}

// GENERATE HAIKU METHOD ------------------------------------------------------/
void GenerateHaiku() {
    Console.Clear();

    // Name Haiku -------------------------------------------------------------/
    Console.Write("Name your Haiku: ");
    string? haikuName = Console.ReadLine();

    // Generate Haiku Lines ---------------------------------------------------/
    string[] haiku = new string[3]; // Store Haiku Lines
    haiku[0] = SyllablePhraseGenerator(5); // Generate 5 Syllable Line
    haiku[1] = SyllablePhraseGenerator(7); // Generate 7 Syllable Line
    haiku[2] = SyllablePhraseGenerator(5); // Generate 5 Syllable Line

    // Display Haiku ----------------------------------------------------------/
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"[{haikuName}]");

    // Write Haiku Lines
    foreach (string line in haiku) {
        Console.WriteLine(line);
    }
    Console.ResetColor();

    // Prompt User to Save or Exit --------------------------------------------/
    if (UserPrompt("Would you like to save your Haiku? (Y/N): ")) {
        // Save & Exit --------------------------------------------------------/
        File.WriteAllText($"poems/{haikuName}.txt", string.Join("\n", haiku));
        Console.Clear();
        return;
    } else {
        // Exit Without Saving ------------------------------------------------/
        Console.Clear();
        return;
    }
}

// GENERATE LIMERICK METHOD ---------------------------------------------------/
void GenerateLimerick() {
    Console.Clear();

    // Name Limerick ----------------------------------------------------------/
    Console.Write("Name your Limerick: ");
    string? limerickName = Console.ReadLine();

    // Generate Limerick Lines ------------------------------------------------/
    string[] limerick = new string[5]; // Store limerick Lines
    
    // Get First Line (Place/Person)
    limerick[0] = TextPrompt("Enter a line than end's with a place or person: "); 

    // Get Second Line (Rhymes with First Line)
    limerick[1] = TextPrompt("Enter a line that rhymes with the first line: ");

    // Get Third Line (Custom)
    limerick[2] = TextPrompt("Enter a line of your choice: ");

    // Get Fourth Line (Rhymes with Third Line)
    limerick[3] = TextPrompt("Enter a line that rhymes with the third line: ");

    // Get Fifth Line (Rhymes with First Line)
    limerick[4] = TextPrompt("Enter a line that rhymes with the first line: ");

    // Display Limerick -------------------------------------------------------/
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"[{limerickName}]");

    // Write Limerick Lines
    foreach (string line in limerick) {
        Console.WriteLine(line);
    }
    Console.ResetColor();

    // Prompt User to Save or Exit --------------------------------------------/
    if (UserPrompt("Would you like to save your Limerick? (Y/N): ")) {
        // Save & Exit --------------------------------------------------------/
        File.WriteAllText($"poems/{limerickName}.txt", string.Join("\n", limerick));
        Console.Clear();
        return;
    } else {
        // Exit Without Saving ------------------------------------------------/
        Console.Clear();
        return;
    }
}

// OPEN POEM METHOD -----------------------------------------------------------/
void OpenPoem() {
    bool openError = false; // Store Error Status
    string openMessage = ""; // Store Error/Success Message

    // Menu Loop --------------------------------------------------------------/
    while (true) {
        // Display Menu -------------------------------------------------------/
        Console.Clear();
        Console.WriteLine("Select a Poem to Open");
        ResponseMessage(openMessage, openError); // Display Error/Success Message

        // Display List of Poem Files
        string[] poemFiles = Directory.GetFiles("poems");
        if (poemFiles.Length == 0) {
            Console.WriteLine("No Poems Found.");
            ExitPrompt("Press any key to exit: "); // Prompt User to Exit
        } else {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < poemFiles.Length; i++) {
                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(poemFiles[i])}");
            }
            Console.WriteLine($"{poemFiles.Length + 1}. Return to Main Menu"); // Display Exit Option
            Console.ResetColor();

            // Get User Input -------------------------------------------------/
            Console.WriteLine();
            Console.Write("Enter a number: ");
            string poemInput = Console.ReadLine();

            // Validate User Input --------------------------------------------/
            if (int.TryParse(poemInput, out int optionNumber) && optionNumber > 0 && optionNumber <= poemFiles.Length) {
                // Display Poem
                DisplayPoem(poemFiles[optionNumber - 1]);
            } else if (optionNumber == poemFiles.Length + 1) {
                // Return to Main Menu
                Console.Clear();
                return;
            } else {
                // Invalid Input
                openError = true;
                openMessage = "Invalid Input. Please try again.";
            }
        }
    }
}

// DISPLAY POEM METHOD --------------------------------------------------------/
void DisplayPoem(string poemFile) {
    Console.Clear();

    // Display Poem Name ------------------------------------------------------/
    Console.WriteLine($"[{Path.GetFileNameWithoutExtension(poemFile)}]");

    // Display Poem -----------------------------------------------------------/
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(File.ReadAllText(poemFile));
    Console.ResetColor();

    // Prompt User to Exit ----------------------------------------------------/
    ExitPrompt("Press any key to exit: ");
}

// TOOL MENU METHODS ----------------------------------------------------------/
void ToolsMenu() {
    bool toolsError = false; // Store Error Status
    string toolsMessage = ""; // Store Error/Success Message

    // Menu Loop --------------------------------------------------------------/
    while (true) {
        // Display Menu -------------------------------------------------------/
        Console.Clear();
        Console.WriteLine("Select a Tool");
        ResponseMessage(toolsMessage, toolsError); // Display Error/Success Message
        Console.WriteLine("1. Find Rhyming Words");
        Console.WriteLine("2. Lookup a Word's Pronunciation");
        Console.WriteLine("3. Lookup a Word's Definition");
        Console.WriteLine("4. Count Syllables in a Word or Phrase");
        Console.WriteLine("5. Count Words in a Phrase");
        Console.WriteLine("6. Return to Main Menu");

        // Get User Input -----------------------------------------------------/
        Console.WriteLine();
        Console.Write("Enter a number: ");
        char templateInput = Console.ReadKey().KeyChar;

        // Load Menu Option ---------------------------------------------------/
        switch (templateInput) {
            // Find Rhyming Words
            case '1':
                while (true) {
                    // Ask User for Word
                    string? inputWord = TextPrompt("Enter a word to find rhymes: ");

                    // Find & Display Rhyming Words
                    FindRhyme(inputWord);

                    // Prompt User to Continue or Exit
                    if (UserPrompt("Would you like to find another rhyme? (Y/N): ")) {
                        continue;
                    } else {
                        break;
                    }
                }
                break;
            // Lookup a Word's Pronunciation
            case '2':
                while (true) {
                    // Ask User for Word
                    string? inputWord = TextPrompt("Enter a word to find pronunciation: ");

                    // Find Pronunciation
                    string[] pronunciationArray = FindPronunciation(inputWord);

                    // Display Pronunciation
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{inputWord.ToUpper()}:");
                    foreach (string pronunciation in pronunciationArray) {
                        Console.Write($" {pronunciation}");
                    }
                    Console.ResetColor();

                    // Prompt User to Continue or Exit
                    if (UserPrompt("Would you like to lookup another word? (Y/N): ")) {
                        continue;
                    } else {
                        break;
                    }
                }
                break;
            // Lookup a Word's Definition
            case '3':
                while (true) {
                    // Ask User for Word
                    string? inputWord = TextPrompt("Enter a word to lookup its definition: ");

                    // Find Definition
                    string definition = LookupDefinition(inputWord);

                    // Display Definition
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{inputWord} - {definition}");
                    Console.ResetColor();

                    // Prompt User to Lookup Another Word or Exit
                    if (UserPrompt("Would you like to lookup another word? (Y/N) ")) {
                        continue;
                    } else {
                        break;
                    }
                }
                break;
            // Count Syllables in a Word or Phrase
            case '4':
                while (true) {
                    // Ask User for Word
                    string? inputPhrase = TextPrompt("Enter a word/phrase to count its syllables: ");

                    // Count Syllables
                    int syllableCount = SyllableCounter(inputPhrase);

                    // Display Syllable Count
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"There are {syllableCount} syllables in the phrase \"{inputPhrase}\".");
                    Console.ResetColor();

                    // Prompt User to Count Another Word/Phrase or Exit
                    if (UserPrompt("Would you like to count syllables in another phrase? (Y/N) ")) {
                        continue;
                    } else {
                        break;
                    }
                }
                break;
            // Count Words in a Phrase
            case '5':
                while (true) {
                    // Ask User for Word
                    string? inputPhrase = TextPrompt("Enter a phrase to count its words: ");

                    // Count Syllables
                    int wordCount = WordCounter(inputPhrase);

                    // Display Word Count
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"There are {wordCount} syllables in the phrase \"{inputPhrase}\".");
                    Console.ResetColor();

                    // Prompt User to Count Another Phrase or Exit
                    if (UserPrompt("Would you like to count words in another phrase? (Y/N) ")) {
                        continue;
                    } else {
                        break;
                    }
                }
                break;
            // Return to Main Menu
            case '6':
                Console.Clear();
                return;
            // Invalid Input
            default:
                toolsError = true;
                toolsMessage = "Invalid Input. Please try again.";
                break;
        }
    }
}

// LOAD PRONUNCIATION DICTIONARY METHOD ---------------------------------------/
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

// FIND RHYME METHOD ----------------------------------------------------------/
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
            findingRhymes = UserPrompt("Would you like to see more rhymes? (y/n): ");

            // Update Indexes
            startIndex += 10;
            if (endIndex + 10 < rhymingWordCount - 1) {
                endIndex += 10;
            } else {
                endIndex = rhymingWordCount - 1;
            }
        } else {
            findingRhymes = !ExitPrompt("Press any key to exit: ");
        }
    }
}

// FIND PRONUNCIATION METHOD --------------------------------------------------/
string[] FindPronunciation(string inputWord) {
    inputWord = inputWord.ToUpper(); // Convert Word to Uppercase for Case Matching in Dictionary

    // Find Input Word and Its Sounds in the Phonetic Dictionary --------------/
    string[] pronunciation = new string[] { }; // Array to Store Pronunciation

    if (pronunciationDictionary.ContainsKey(inputWord)) {
        pronunciation = pronunciationDictionary[inputWord].Item1.ToArray();
    }

    return pronunciation;
}

// LOOKUP DEFINITION METHOD ---------------------------------------------------/
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

// SYLLABLE PHRASE GENERATOR METHOD -------------------------------------------/
string SyllablePhraseGenerator(int maxSyllables) {
    string phrase = ""; // Store Phrase
    int syllableCount = 0; // Store Syllable Count

    // Initialize Loop for Phrase Input ---------------------------------------/
    while (syllableCount < maxSyllables) {
        Console.Clear();
        // Print Instructions
        Console.WriteLine("Follow the prompts to generate a Haiku.");
        Console.Write($"Please enter a/an {maxSyllables} syllable phrase ");
        Console.WriteLine($"({maxSyllables - syllableCount} syllables remaining): "); // Display Syllables Remaining

        // Initialize Loop for Word Input -------------------------------------/
        bool completeWord = false; // Store Word Completion Status
        string word = "";

        while (!completeWord) {
            ConsoleKeyInfo key = Console.ReadKey(); // Get User Input

            // Check for Backspace --------------------------------------------/
            if (key.Key == ConsoleKey.Backspace) {
                if (word.Length > 0) {
                    word = word.Remove(word.Length - 1); // Remove Last Character
                }
            }

            // Check for Word Completion (Space or Enter) ---------------------/
            if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter) {
                completeWord = true;
            } else {
                word += key.KeyChar; // Add Character to Word
            }
        }
        phrase += word + " "; // Add Word to Phrase
        syllableCount += SyllableCounter(word); // Add Syllables to Count
    }

    // Return Phrase ----------------------------------------------------------/
    phrase = phrase.Trim(); // Remove Leading/Trailing Whitespace
    return phrase;
}

// SYLLABLE COUNTER METHOD ----------------------------------------------------/
int SyllableCounter(string input) {
    int syllableCount = 0; // Store Syllable Count
    string[] words = input.Split(" "); // Split Input into Words

    // Count Syllables in Each Word -------------------------------------------/
    foreach (string word in words) {
        int wordSyllables = 0; // Store Syllable Count of Each Word
        bool lastLetterVowel = false;

        foreach (char c in word) {
            // Check if Letter is a Vowel
            bool isVowel = "aeiouy".Contains(c.ToString().ToLower());

            // If Letter is a Vowel and Last Letter was not a Vowel, Add Syllable
            if (isVowel && !lastLetterVowel) {
                wordSyllables++;
            }

            lastLetterVowel = isVowel; // Update Last Letter Vowel Status
        }

        // If Word Ends in Silent 'e', Remove Syllable
        if (word.EndsWith("e")) {
            wordSyllables--;
        }

        // If Word Ends in 'le', Add Syllable
        if (word.EndsWith("le")) {
            wordSyllables++;
        }

        // Check for One Syllable Words
        if (wordSyllables == 0) {
            wordSyllables = 1;
        }

        // Add Word Syllables to Total Syllable Count
        syllableCount += wordSyllables;
    }

    return syllableCount;
}

// WORD COUNTER METHOD --------------------------------------------------------/
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