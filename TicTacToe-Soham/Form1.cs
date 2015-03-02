using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe_Soham
{
    public partial class Form1 : Form
    {

        public Button[] button;
        public char player, comp;
        public int[] play;
        public int starter;
        public int gameon;
        public int gamewon;
        public int turn;
        public int counter, nodes;


        public int validmoves(int[] aux)
        {
            int j = 0;
            for (int i = 1; i <= 9; i++)
            {
                if (play[i] == 1)
                {
                    aux[i] = 1;
                    j++;
                }
            }
            return j;
        }


        public void arrcopy(int[] source, int[] dest)
        {
            for (int i = 1; i <= 9; i++)
                dest[i] = source[i];
        }

        public int findmin(int[] arr)
        {
            int min = 1000;
            int j = 1;
            for (int i = 1; i < 10; i++)
            {
                if ((arr[i] < min) && (play[i] == 1))
                {
                    min = arr[i];
                    j = i;
                }
            }
            return j;
        }

        public int findmax(int[] arr)
        {
            int max = -1000;
            int j = 1;
            for (int i = 1; i < 10; i++)
            {
                if ((arr[i] > max) && (play[i] == 1))
                {
                    max = arr[i];
                    j = i;
                }
            }
            return j;
        }

        public int heuristic(int total)
        {

            int[] maxcost = new int[10];
            int[] temp = new int[10];
            arrcopy(play, temp);
            init(maxcost);
            for (int i = 1; i <= 9; i++)
            {
                if (play[i] == 1)
                {
                    play[i] = 3;
                    maxcost[i] = newtree(2, total);
                    MessageBox.Show("COST[" + i + "] : " + maxcost[i]);
                    arrcopy(temp, play);
                }
            }


            MessageBox.Show("HEURISTIC NODE TRAVERSAL : " + this.nodes + " NODES Traversed ");
            this.nodes = 0;
            return findmax(maxcost);
        }

        public int newtree(int currturn, int total)        // TRUE HEURISTIC TREE SEARCHING with Alpha Beta Pruning ( Nailed It ! )
        {

            int nextturn;

            if (win(3) == 1)
                return 9;
            else if (win(2) == 1)
                return -9;
            //else if ((checkheuristic(1) + checkheuristic(2) + checkheuristic(4) == 0) && (checkheuristic(1) + checkheuristic(3) + checkheuristic(9) == 0))
            //     return 0;
            //else if (total == 0)
            //  return 0;
            else
            {

                if (currturn == 2)
                    nextturn = 3;
                else
                    nextturn = 2;

                int[] temp = new int[10];
                arrcopy(play, temp);
                int[] maxcost = new int[10];
                init(maxcost);

                for (int i = 1; i <= 9; i++)
                {
                    if (play[i] == 1)
                    {
                        play[i] = currturn;
                        maxcost[i] = newtree(nextturn, total - 1);
                        arrcopy(temp, play);
                        if ((maxcost[i] == 9) && (currturn == 3))               // V. V. Important     
                        {
                            this.nodes++;
                            return 9;

                        }
                        if ((maxcost[i] == -9) && (currturn == 2))            // V. V. Important
                        {
                            this.nodes++;
                            return -9;
                        }

                    }
                }
                if (currturn == 3)
                {
                    this.nodes++;
                    return (maxcost[findmax(maxcost)]);
                }
                else
                {
                    this.nodes++;
                    return (maxcost[findmin(maxcost)]);
                }
            }
        }







        public void init(int[] arr)
        {
            for (int i = 1; i < 10; i++)
                arr[i] = 0;
        }





        public int win(int who)                               // returns 0 if won
        {
            if (who == 2)
            {
                if (((checkrows(8) == 8) || (checkcol(8) == 8)) || (checkdia(8) == 8))
                    return 1;
                else
                    return 0;
            }
            else
            {
                if (((checkrows(27) == 27) || (checkcol(27) == 27)) || (checkdia(27) == 27))
                    return 1;
                else
                    return 0;
            }
        }



        public int checkheuristic(int val)
        {
            int chance = 0;
            if ((play[1] * play[2] * play[3]) == val)
                chance++;
            if ((play[4] * play[5] * play[6]) == val)
                chance++;
            if ((play[7] * play[8] * play[9]) == val)
                chance++;
            if ((play[1] * play[4] * play[7]) == val)
                chance++;
            if ((play[2] * play[5] * play[8]) == val)
                chance++;
            if ((play[3] * play[6] * play[9]) == val)
                chance++;
            if ((play[1] * play[5] * play[9]) == val)
                chance++;
            if ((play[3] * play[5] * play[7]) == val)
                chance++;
            return chance;
        }

        public int checkrows(int val)
        {
            if ((play[1] * play[2] * play[3]) == val)
            {
                if (play[1] == 1)
                    return 1;
                if (play[2] == 1)
                    return 2;
                if (play[3] == 1)
                    return 3;
                return val;
            }

            else if ((play[4] * play[5] * play[6]) == val)
            {
                if (play[4] == 1)
                    return 4;
                if (play[5] == 1)
                    return 5;
                if (play[6] == 1)
                    return 6;
                return val;
            }
            if ((play[7] * play[8] * play[9]) == val)
            {
                if (play[7] == 1)
                    return 7;
                if (play[8] == 1)
                    return 8;
                if (play[9] == 1)
                    return 9;
                return val;
            }

            return 0;

        }

        public int checkcol(int val)
        {
            if ((play[1] * play[4] * play[7]) == val)
            {
                if (play[1] == 1)
                    return 1;
                if (play[4] == 1)
                    return 4;
                if (play[7] == 1)
                    return 7;
                return val;
            }

            else if ((play[2] * play[5] * play[8]) == val)
            {
                if (play[2] == 1)
                    return 2;
                if (play[5] == 1)
                    return 5;
                if (play[8] == 1)
                    return 8;
                return val;
            }
            if ((play[3] * play[6] * play[9]) == val)
            {
                if (play[3] == 1)
                    return 3;
                if (play[6] == 1)
                    return 6;
                if (play[9] == 1)
                    return 9;
                return val;
            }

            return 0;

        }

        public int checkdia(int val)
        {
            if ((play[1] * play[5] * play[9]) == val)
            {
                if (play[1] == 1)
                    return 1;
                if (play[5] == 1)
                    return 5;
                if (play[9] == 1)
                    return 9;
                return val;
            }

            else if ((play[3] * play[5] * play[7]) == val)
            {
                if (play[3] == 1)
                    return 3;
                if (play[5] == 1)
                    return 5;
                if (play[7] == 1)
                    return 7;
                return val;
            }

            return 0;

        }

        public void computermove()
        {
            int move = 0;

            counter++;
            if (gameon == 1)
            {



                if (checkrows(9) != 0)                 //        Removing normal checking doesn't help
                {
                    move = checkrows(9);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }
                else if (checkcol(9) != 0)
                {
                    move = checkcol(9);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }
                else if (checkdia(9) != 0)
                {
                    move = checkdia(9);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }
                else if (checkrows(4) != 0)
                {
                    move = checkrows(4);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }
                else if (checkcol(4) != 0)
                {
                    move = checkcol(4);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }
                else if (checkdia(4) != 0)
                {
                    move = checkdia(4);
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    MessageBox.Show("COMPUTER's MOVE : " + move.ToString());
                    Application.DoEvents();
                }

                else
                {
                    int total;
                    int[] aux = new int[10];
                    init(aux);
                    total = validmoves(aux);
                    MessageBox.Show("TOTAL VALID MOVES REMAINING : " + total.ToString());
                    move = heuristic(total);
                    MessageBox.Show("COMPUTER's MOVE (Heuristic) : " + move.ToString());
                    play[move] = 3;
                    button[move].Text = "" + comp;
                    Application.DoEvents();

                }

                label3.Text = "YOUR MOVE ";
                this.turn = 2;
                Application.DoEvents();

                if (win(3) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "COMPUTER WON";
                    Application.DoEvents();
                }

                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }


            }
        }
        
        
        
        public Form1()
        {
            InitializeComponent();
            label1.Text = "WHO WILL START ? ";
            label2.Text = "Choose your Sign ";
            button = new Button[10];
            play = new int[10];
            button[1] = button1;
            button[2] = button2;
            button[3] = button3;
            button[4] = button4;
            button[5] = button5;
            button[6] = button6;
            button[7] = button7;
            button[8] = button8;
            button[9] = button9;
            this.player = 'c';
            this.nodes = 0;
            this.starter = -1;
            this.gameon = 0;
            this.turn = 0;
            this.counter = 0;

            for (int i = 1; i <= 9; i++)
            {
                this.play[i] = 1;
                button[i].Text = "";
            }
            Application.DoEvents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.starter = 2;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (((this.player == 'X') || (this.player == 'O')) && ((this.starter == 2) || (this.starter == 3)))
            {
                if (this.player == 'X')
                    this.comp = 'O';
                else
                    this.comp = 'X';
                this.turn = this.starter;
                this.gameon = 1;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                label3.Text = "GAME IS COMMENCING ";
                Application.DoEvents();



            }

            else
            {
                label3.Text = "SELECT THE SETTINGS FIRST PROPERLY";
                Application.DoEvents();
            }

            if (this.gameon == 1)
            {
                if (this.turn == 2)
                {
                    label3.Text = "YOUR MOVE ";
                    Application.DoEvents();
                }
                else
                {
                    label3.Text = "COMPUTER's MOVE ";
                    Application.DoEvents();
                    this.computermove();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[1].Text = "" + player;
                play[1] = 2;

                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }

                this.turn = 3;
                computermove();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[2].Text = "" + player;
                play[2] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[3].Text = "" + player;
                play[3] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[4].Text = "" + player;
                play[4] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[5].Text = "" + player;
                play[5] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[6].Text = "" + player;
                play[6] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[7].Text = "" + player;
                play[7] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[8].Text = "" + player;
                play[8] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (gameon == 1)
            {
                counter++;
                button[9].Text = "" + player;
                play[9] = 2;
                label3.Text = "COMPUTERS MOVE ";
                Application.DoEvents();
                if (win(2) == 1)
                {
                    this.gameon = 0;
                    label3.Text = "PLAYER WON";
                    Application.DoEvents();
                }
                if (counter == 9)
                {
                    this.gameon = 0;
                    label3.Text = "DRAW";
                    Application.DoEvents();
                }
                this.turn = 3;
                computermove();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.starter = 3;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.player = 'X';
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.player = 'O';
        }
    }
}