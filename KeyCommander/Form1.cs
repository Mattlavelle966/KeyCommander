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
 * points system needs fixing //Fixed
 * exe broken so far only on new user machine//Fixed
 * bitmap replaced should be //Fixed
 * Fix seconds timer//Fixed
 * ui astetic
 
 
 
 */
namespace KeyCommander
{
    public partial class KeyCommander : Form
    {
        int numOfInputs = 0;
        int timerNum = 0;
        int points = 0;
        bool GameHasStarted = false;
        string userSequence;
        KeyCodeGenerator CurrentCode;
        Stats stats = new Stats();

        public KeyCommander()
        {
            InitializeComponent();
        }

        private void Sqtimer(object sender, EventArgs e)
        {
            
            bool isChallangeComplete = numOfInputs >= CurrentCode.TotalKeys;//for more redability 
            if (!isChallangeComplete)
            {
                return;
            }//less nesting is more readable


            numOfInputs = 0;
            resetPics("user");
            points += Int32.Parse(CurrentCode.CompareSequence(userSequence));
            stats.addData(name_box.Text, timer.Text, CurrentCode.CompareSequence(userSequence), CurrentCode.KeyCreationSequence);
            NewSequence();
            userSequence = "";
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
            sqtime.Stop();
            SecondsTimer.Stop();
            MessageBox.Show(stats.displayDataAsStr());
        }
        private void KeyCommander_KeyDown(object sender, KeyEventArgs e)
        {
            // this class generates a random key sequence the sequence constants are 1,2,3,4 each one representing up,down,left,right
            //below will be the users created sequence it will be a char array of ints  

            PictureBox[] userBoxes = { inputBox1, inputBox2, inputBox3, inputBox4, inputBox5, inputBox6,
                inputBox7, inputBox8, inputBox9, inputBox10, inputBox11,inputBox12};
            //if (numOfInputs <= KeyCodeGenerator.TotalKeys)
            //{
            if (!GameHasStarted)
            {
                return;
            }
            if (e.KeyCode == Keys.W)
            {
                userSequence += "1";
                userBoxes[numOfInputs].Image = Properties.Resources.up; 
                numOfInputs++;

            }
            else if (e.KeyCode == Keys.S)
            {
                userSequence += "2";
                userBoxes[numOfInputs].Image = Properties.Resources.down;
                numOfInputs++;
            }
            else if (e.KeyCode == Keys.D)
            {
                userSequence += "4";
                userBoxes[numOfInputs].Image = Properties.Resources.right; 
                numOfInputs++;
            }
            else if (e.KeyCode == Keys.A)
            {
                userSequence += "3";
                userBoxes[numOfInputs].Image = Properties.Resources.left;
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
            resetPics("pc");
            resetPics("user");
            GameHasStarted = true;
            for (int index = 0; index < sequenceCode.Length; index++)
            {
                if (sequenceCode[index] == '1') { boxes[index].Image = Properties.Resources.up; }
                else if (sequenceCode[index] == '2') { boxes[index].Image = Properties.Resources.down; }
                else if (sequenceCode[index] == '3') { boxes[index].Image = Properties.Resources.left; }
                else if (sequenceCode[index] == '4') { boxes[index].Image = Properties.Resources.right; }


            }
            sqtime.Start();
            SecondsTimer.Start();
        }


       
        private void KeyCommander_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"Welcome To Key Commander\nGoal: Recreate arrow sequence\nControls\n    W = up\n    S = down\n    D = right\n    A = left\n-input your name\n-Press Start to begin\n-press results for your score");
        }

        private void SecondsTimer_Tick(object sender, EventArgs e)
        {
            timer.Text = timerNum++ + "S";
            points_lbl.Text = points + "P";
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
