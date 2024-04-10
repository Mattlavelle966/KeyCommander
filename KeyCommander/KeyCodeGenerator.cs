using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCommander
{
    internal class KeyCodeGenerator
    {
        public int TotalKeys { get; set; }
        public string KeyCreationSequence { get; set; }

        public string CodeGenerator()
        {
            TotalKeys = 0;
            Random rand = new Random();
            TotalKeys = rand.Next(1, 12);
            for (int index = 0; index < TotalKeys; index++)
            {
                KeyCreationSequence += rand.Next(1, 5) + "";
                
            }
            return KeyCreationSequence;
        }
        public string CompareSequence(string sequence)
        {
            int score = 0;
            if (sequence == KeyCreationSequence)
            {
                score = 130;
            }
            else
            {
                char[] userSequence = sequence.ToCharArray();
                char[] generatedSequence = KeyCreationSequence.ToCharArray();
                for (int index = 0; index < userSequence.Length; index++)
                {
                    if (userSequence[index] == generatedSequence[index])
                    {
                        score += 10;
                    }

                }
            }


            return score + "";
        }
    }
}
