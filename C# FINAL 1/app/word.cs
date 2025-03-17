using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__FINAL_1.app
{
    internal class word
    {
        public string SourceWord { get; set; }
        public List<string> TargetWords { get; set; }

        public word(string sourceWord, List<string> targetWords)
        {
            SourceWord = sourceWord;
            TargetWords = targetWords;
        }

        public void DeleteTargetWord(string targetWord)
        {
            if (TargetWords.Count() > 1)
            {
                TargetWords.Remove(targetWord);
            } else
            {
                throw new Exception("Cannot delete the last target word");
            }
        }

        public void ChangeSourceWord(string sourceWord)
        {
            SourceWord = sourceWord;
        }

        public void ChangeTargetWord(string targetWord, string newTargetWord)
        {
            DeleteTargetWord(targetWord);
            AddTargetWord(newTargetWord);
        }

        public void AddTargetWord(string targetWord)
        {
            TargetWords.Add(targetWord);
        }


    }
}
