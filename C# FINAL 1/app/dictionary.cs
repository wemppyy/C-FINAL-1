using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace C__FINAL_1.app
{
    internal class dictionary
    {
        public string SourceLang { get; set; }
        public string TargetLang { get; set; }
        public List<word> Words  { get; set; }
        public string DictName   { get; set; }

        public dictionary(string sourceLang, string targetLang, string dictName)
        {
            SourceLang = sourceLang;
            TargetLang = targetLang;
            Words = new List<word>();
            DictName = dictName;
        }

        public void AddWord(string sourceWord, List<string> targetWords)
        {
            if (Words.Exists(x => x.SourceWord == sourceWord))
            {
                throw new Exception("Word already exists");
            }
            else
            {
                Words.Add(new word(sourceWord, targetWords));
            }
        }

        public void DeleteWord(string word)
        {
            if (Words.Exists(x => x.SourceWord == word))
            {
                Words.Remove(Words.Find(x => x.SourceWord == word));
            }
            else
            {
                throw new Exception("Word not found");
            }
        }

        public void DeleteTargetWord(string sourceWord, string targetWord)
        {
            if (Words.Exists(x => x.SourceWord == sourceWord))
            {
                word w = Words.FirstOrDefault(x => x.SourceWord == sourceWord);
                w.DeleteTargetWord(targetWord);
            }
            else
            {
                throw new Exception("Source word not found");
            }
        }

        public void ChangeSourceLang(string sourceLang)
        {
            SourceLang = sourceLang;
        }

        public void ChangeTargetLang(string targetLang)
        {
            TargetLang = targetLang;
        }

        public void ChangeSourceWord(string sourceWord, string newSourceWord)
        {
            if (Words.Exists(x => x.SourceWord == sourceWord))
            {
                word w = Words.Find(x => x.SourceWord == sourceWord);
                w.ChangeSourceWord(newSourceWord);
            }
            else
            {
                throw new Exception("Source word not found");
            }
        }

        public void ChangeTargetWord(string sourceWord, string targetWord, string newTargetWord)
        {
            if (Words.Find(x => x.SourceWord == sourceWord).TargetWords.Contains(targetWord))
            {
                word w = Words.Find(x => x.SourceWord == sourceWord);
                w.ChangeTargetWord(targetWord, newTargetWord);
            }
            else
            {
                throw new Exception("Target word not found");
            }
        }

        public void AddTargetWord(string sourceWord, string targetWord)
        {
            if (Words.Exists(x => x.SourceWord == sourceWord))
            {
                word w = Words.Find(x => x.SourceWord == sourceWord);
                w.AddTargetWord(targetWord);
            }
            else
            {
                throw new Exception("Source word not found");
            }
        }

        public string Translate(string sourceWord)
        {
            if (Words.Exists(x => x.SourceWord == sourceWord))
            {
                return Words.Find(x => x.SourceWord == sourceWord).TargetWords[0];
            }
            else
            {
                throw new Exception("Word not found");
            }
        }

        public void ChangeDictName(string dictName)
        {
            DictName = dictName;
        }

        public void Export(string path)
        {
            string fileName = DictName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
            string fullPath;

            if (path[0] == '/')
            {
                fullPath = path + fileName;
            }
            else
            {
                fullPath = path + "/" + fileName;
            }

            string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fullPath, jsonString);
        }

        public void Import(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The file path cannot be empty or null.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found: {path}", path);
            }

            try
            {
                string jsonString = File.ReadAllText(path);
                dictionary? dict = JsonSerializer.Deserialize<dictionary>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                });

                if (dict == null)
                {
                    throw new InvalidDataException("The dictionary file contains invalid or empty data.");
                }

                SourceLang = dict.SourceLang;
                TargetLang = dict.TargetLang;
                Words = dict.Words;
                DictName = dict.DictName;
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("Error reading the JSON file. The file may be corrupted or incorrectly formatted.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("File access error. The file may be in use by another program.", ex);
            }
        }

    }
}
