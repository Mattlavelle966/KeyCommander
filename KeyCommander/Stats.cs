using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyCommander
{
    internal class Stats
    {
        public string[] Score { get; set; }
        public string[] Sequence { get; set; }
        public string[] Time { get; set; }
        public string Name { get; set; }

        public int Redundant { get; set; }
        public int NumInputs { get; set; }




        public Stats()
        {
            Score = new string[30]; // Assuming a maximum of 100 inputs, adjust as needed
            Sequence = new string[30];
            Time = new string[30];
            NumInputs = 0;
        }
        public Stats(int re)
        {
            Redundant = re;
        }
        public void addData(string name, string time, string score, string sequence)
        {
            Score[NumInputs] = score;
            Sequence[NumInputs] = sequence;
            Time[NumInputs] = time;
            Name = name;
            NumInputs++;
        }
        public string displayDataAsStr()
        {
            string Data = $"Name:{Name}\n";
            int totalScore = 0;
            int AvgTime = 0;
            //$"SEQ:{string.Join("", stats.Sequence[index])}\n  TimePassed:{string.Join("", stats.Time[index])}\n  Score:{string.Join("", stats.Score[index])}\n"
            for (int index = 0; index < NumInputs; index++)
            {
                totalScore += Int32.Parse(Time[index].Replace("S", string.Empty));
                AvgTime += Int32.Parse(Score[index]);

            }
            AvgTime = AvgTime / NumInputs;

            Data += $"Time elapsed:{AvgTime}s\n Points:{totalScore}\n";
            return Data;
        }
    }
}
