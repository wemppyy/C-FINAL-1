using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__FINAL_1.app
{
    internal class ui
    {
        public List<dictionary> Dictionaries { get; set; }

        public ui(dictionary firstDict)
        {
            Dictionaries = new List<dictionary>();
            Dictionaries.Add(firstDict);
        }
        public void PrintDict(int index)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(index + ": " + Dictionaries[index].DictName + " (" + Dictionaries[index].SourceLang + " to " + Dictionaries[index].TargetLang + ")");

                Console.WriteLine("--------------------");
                PrintWordsLimited(index, 5);
                Console.WriteLine("--------------------");

                Console.WriteLine("1. Word operations");
                Console.WriteLine("2. Dictionary operations");
                Console.WriteLine("3. Print all words");

                Console.WriteLine("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());
                MainMenu(index, option);
            }


        }

        public void PrintWordsLimited(int index, int count)
        {
            if (Dictionaries[index].Words.Count() == 0)
            {
                Console.WriteLine("No words in dictionary");
            }
            else if (Dictionaries[index].Words.Count() <= count)
            {
                foreach (word w in Dictionaries[index].Words)
                {
                    Console.WriteLine(w.SourceWord + " -> " + string.Join(", ", w.TargetWords));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(Dictionaries[index].Words[i].SourceWord + " -> " + string.Join(", ", Dictionaries[index].Words[i].TargetWords));
                }
                Console.WriteLine("...");
            }
        }

        public void PrintWords(int index)
        {
            Console.Clear();
            if (Dictionaries[index].Words.Count() == 0)
            {
                Console.WriteLine("No words in dictionary");
            }
            else
            {
                foreach (word w in Dictionaries[index].Words)
                {
                    Console.WriteLine(w.SourceWord + " -> " + string.Join(", ", w.TargetWords));
                }
            }
        }

        public void MainMenu(int index, int option)
        {
            Console.Clear();
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter word: ");
                    string word = Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine("1. Add target words");
                    Console.WriteLine("2. Delete");
                    Console.WriteLine("3. Change");
                    Console.WriteLine("4. Add target");
                    Console.WriteLine("5. Delete target");
                    Console.WriteLine("6. Change target");
                    Console.WriteLine("7. Back");
                    Console.WriteLine("Choose an option: ");
                    int option1 = Convert.ToInt32(Console.ReadLine());
                    WordsMenu(index, option1, word);
                    break;
                case 2:
                    Console.WriteLine("1. Add");
                    Console.WriteLine("2. Delete");
                    Console.WriteLine("3. Change name");
                    Console.WriteLine("4. Switch");
                    Console.WriteLine("5. Import");
                    Console.WriteLine("6. Export");
                    Console.WriteLine("7. Back");
                    Console.WriteLine("Choose an option: ");
                    int option2 = Convert.ToInt32(Console.ReadLine());
                    DictMenu(index, option2);
                    break;
                case 3:
                    PrintWords(index);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

        }

        public void WordsMenu(int index, int option, string word)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    addWord(index, word);
                    break;
                case 2:
                    Console.Clear();
                    deleteWord(index, word);

                    break;
                case 3:
                    Console.Clear();
                    changeWord(index, word);
                    break;
                case 4:
                    Console.Clear();
                    addTargetWord(index, word);
                    break;
                case 5:
                    Console.Clear();
                    deleteTargetWord(index, word);
                    break;
                case 6:
                    Console.Clear();
                    changeTargetWord(index, word);
                    break;
                case 7:
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public void DictMenu(int index, int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    addDict();
                    break;
                case 2:
                    Console.Clear();
                    deleteDict(index);

                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Enter dictionary name: ");
                    string dictName1 = Console.ReadLine();
                    changeDictName(index, dictName1);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Enter dictionary index: ");
                    int dictIndex = Convert.ToInt32(Console.ReadLine());

                    try
                    {
                        switchDict(dictIndex);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Dictionary not found");
                    }

                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Enter file path: ");
                    string path = Console.ReadLine();
                    try
                    {
                        Dictionaries[index].Import(path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Enter file path: ");
                    string path1 = Console.ReadLine();
                    try
                    {
                        Dictionaries[index].Export(path1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    break;
                case 7:
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public void addWord(int index, string sourceWord)
        {
            Console.WriteLine("Enter target words separated by comma: ");
            string targetWords = Console.ReadLine();
            List<string> targetWordsList = targetWords.Split(',').ToList();

            try
            {
                Dictionaries[index].AddWord(sourceWord, targetWordsList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void deleteWord(int index, string sourceWord)
        {
            try
            {
                Dictionaries[index].DeleteWord(sourceWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void changeWord(int index, string sourceWord)
        {
            Console.WriteLine("Enter new word: ");
            string newWord = Console.ReadLine();

            try
            {
                Dictionaries[index].ChangeSourceWord(sourceWord, newWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void addTargetWord(int index, string sourceWord)
        {
            Console.WriteLine("Enter target word: ");
            string targetWord = Console.ReadLine();

            try
            {
                Dictionaries[index].AddTargetWord(sourceWord, targetWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void deleteTargetWord(int index, string sourceWord)
        {
            Console.WriteLine("Enter target word: ");
            string targetWord = Console.ReadLine();

            try
            {
                Dictionaries[index].DeleteTargetWord(sourceWord, targetWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void changeTargetWord(int index, string sourceWord)
        {
            Console.WriteLine("Enter target word: ");
            string targetWord = Console.ReadLine();
            Console.WriteLine("Enter new target word: ");
            string newTargetWord = Console.ReadLine();
            Dictionaries[index].ChangeTargetWord(sourceWord, targetWord, newTargetWord);
        }

        public void addDict()
        {
            Console.WriteLine("Enter source language: ");
            string sourceLang = Console.ReadLine();
            Console.WriteLine("Enter target language: ");
            string targetLang = Console.ReadLine();
            Console.WriteLine("Enter dictionary name: ");
            string dictName = Console.ReadLine();
            int index = Dictionaries.Count();
            Dictionaries.Add(new dictionary(sourceLang, targetLang, dictName));
            switchDict(index);
        }

        public void deleteDict(int index)
        {
            if (Dictionaries.Count() > 1)
            {
                Dictionaries.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Cannot delete the last dictionary");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void changeDictName(int index, string dictName)
        {
            Dictionaries[index].DictName = dictName;
        }

        public void switchDict(int index)
        {
            PrintDict(index);
        }
    }
}
