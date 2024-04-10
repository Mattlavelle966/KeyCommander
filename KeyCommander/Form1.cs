using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
//Matthew Lavelle
//Key Commander Game
//BUGS REPORT/TODOS
/*
 * 
 * Right arrow symbol is never shown //Fixed
 * points system needs fixing //NotFixed anymore
 * exe broken so far only on new user machine
 * RunningPath needs to be understood in more depth
 * ui astetic//probably wont bother with
 
 
 
 */
namespace KeyCommander
{
    public partial class KeyCommander : Form
    {
        string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
        string Dir;
        int numOfInputs = 0;
        string up = "up.png", down = "down.png", left = "left.png", right = "right.png";
        int timerNum = 0;
        KeyCodeGenerator CurrentCode;
        Stats stats = new Stats();
        bool GameHasStarted = false;
        int points = 0;
        string userSequence;
        public KeyCommander()
        {
            InitializeComponent();
        }

        private void Sqtimer(object sender, EventArgs e)
        {
            timer.Text = timerNum++ + "S";
            points_lbl.Text = points + "P";
            bool isChallangeComplete = numOfInputs >= CurrentCode.TotalKeys;//for more redability 
            if (!isChallangeComplete)
            {
                return;
            }//less nesting is more readable
            numOfInputs = 0;
            resetPics("user");
            stats.addData(name_box.Text, timer.Text, CurrentCode.CompareSequence(userSequence), CurrentCode.KeyCreationSequence);
            NewSequence();
        }
        public void resetPics(string choice)
        {
            if (choice == "user")
            {
                PictureBox[] userBoxes = { inputBox1, inputBox2, inputBox3, inputBox4, inputBox5, inputBox6,
                inputBox7, inputBox8, inputBox9, inputBox10, inputBox11, inputBox12};
                for (int index = 0; index < userBoxes.Length; index++)
                {
                    userBoxes[index].Image = null;
                }

            }
            else if (choice == "pc")
            {
                PictureBox[] boxes = { Box1, Box2, Box3, Box4, Box5, Box6, Box7, Box8, Box9, Box10, Box11, Box12 };
                for (int index = 0; index < boxes.Length; index++)
                {
                    boxes[index].Image = null;
                }
            }
        }
        private void startGame_Click(object sender, EventArgs e)
        {
            Dir = string.Format("{0}Resources\\", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            if (name_box.Text == "")
            {
                MessageBox.Show("you must put your name in before you can play");
            }
            else
            {
                NewSequence();
            }
        }

        private void Results_Click(object sender, EventArgs e)
        {
            MessageBox.Show(stats.displayDataAsStr());
            sqtime.Stop();
        }
        private void KeyCommander_KeyDown(object sender, KeyEventArgs e)
        {
            // this class generates a random key sequence the sequence constants are 1,2,3,4 each one representing up,down,left,right
            //below will be the users created sequence it will be a char array of ints  
            userSequence = "";

            PictureBox[] userBoxes = { inputBox1, inputBox2, inputBox3, inputBox4, inputBox5, inputBox6,
                inputBox7, inputBox8, inputBox9, inputBox10, inputBox12, inputBox11};
            //if (numOfInputs <= KeyCodeGenerator.TotalKeys)
            //{
            if (!GameHasStarted)
            {
                return;
            }
            if (e.KeyCode == Keys.W)
            {
                userSequence += "1";
                using (Bitmap bm = new Bitmap(Dir + up))
                {
                    userBoxes[numOfInputs].Image = (Bitmap)bm.Clone(); ;
                }
                numOfInputs++;

            }
            else if (e.KeyCode == Keys.S)
            {
                userSequence += "2";
                using (Bitmap bm = new Bitmap(Dir + down))
                {
                    userBoxes[numOfInputs].Image = (Bitmap)bm.Clone(); ;
                }
                numOfInputs++;
            }
            else if (e.KeyCode == Keys.D)
            {
                userSequence += "4";
                using (Bitmap bm = new Bitmap(Dir + right))
                {
                    userBoxes[numOfInputs].Image = (Bitmap)bm.Clone(); ;
                }
                numOfInputs++;
            }
            else if (e.KeyCode == Keys.A)
            {
                userSequence += "3";
                using (Bitmap bm = new Bitmap(Dir + left))                                                                                      
                {
                    userBoxes[numOfInputs].Image = (Bitmap)bm.Clone(); ;
                }
                numOfInputs++;

            }

        }
        private void NewSequence()
        {

            numOfInputs = 0;
            timerNum = 0;
            string sequence;
            CurrentCode = new KeyCodeGenerator();
            sequence = CurrentCode.CodeGenerator();
            char[] sequenceCode = CurrentCode.KeyCreationSequence.ToCharArray();
            PictureBox[] boxes = { Box1, Box2, Box3, Box4, Box5, Box6, Box7, Box8, Box9, Box10, Box11, Box12 };
            points += Int32.Parse(CurrentCode.CompareSequence(userSequence));
            resetPics("pc");
            resetPics("user");
            GameHasStarted = true;
            for (int index = 0; index < sequenceCode.Length; index++)
            {
                string direc = "";
                if (sequenceCode[index] == '1') { direc = Dir + "up.png"; }
                else if (sequenceCode[index] == '2') { direc = Dir + "down.png"; }
                else if (sequenceCode[index] == '3') { direc = Dir + "left.png"; }
                else if (sequenceCode[index] == '4') { direc = Dir + "right.png"; }
                using (Bitmap bm = new Bitmap(direc))
                {
                    boxes[index].Image = (Bitmap)bm.Clone();
                }


            }
            sqtime.Start();
        }
    }
}
