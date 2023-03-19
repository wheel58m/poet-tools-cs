// Austin Wheeler
// CS 1400 Final Project
// 

// NOTES ----------------------------------------------------------------------/
/*

*/

FindRhyme("Yellow");

// METHOD: Find Rhyming Word --------------------------------------------------/
static void FindRhyme(string word) {
    word = word.ToUpper();
    string rhymeSyllable = "";

    string dictFile = "cmudict-0.7b"; // Save Filename of Phonetic Dictionary
    string[] pronunciations = File.ReadAllLines(dictFile);

    // Find Requested Word and Its Rhyming Syllable in the Phonetic Dictionary
    foreach (string pronunciation in pronunciations) {
        string[] terms = pronunciation.Split(" ");

        foreach (string term in terms) {
            if (term == word) {
                rhymeSyllable = terms[^1];
                Console.WriteLine(rhymeSyllable);
            }
        }
    }

    // Print Every Word with the Same Rhyming Syllable
    Console.WriteLine("Here is a list of every possible rhyming word:");
    
    foreach (string pronunciation in pronunciations) {
        string[] terms = pronunciation.Split(" ");

        if (terms[^1] == rhymeSyllable) {
            Console.WriteLine(pronunciation);
        }
    }
}