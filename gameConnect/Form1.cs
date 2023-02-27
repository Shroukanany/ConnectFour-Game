using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameConnect
{


    public partial class Form1 : Form
    {
        private Rectangle[] boardColumns;
        private int[,] board;
        private int turn;
        int counter = 0;

        public Form1()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];

            this.turn = 1;



        }



        private void Form1_Print(object sender, PaintEventArgs e)
        {


            this.Width = 650;
            this.Height = 450;
            this.Text = "connect 4";
            this.BackColor = Color.AntiqueWhite;

            e.Graphics.FillRectangle(Brushes.DarkGray, 24, 24, 340, 300);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        this.boardColumns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);

                }
            }

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            int columnIndex = this.columnNumber(e.Location);
            //Console.WriteLine( );
            //MessageBox.Show(columnIndex.ToString());
            if (columnIndex != -1)
            {
                int rowIndex = this.EmptyRow(columnIndex);
                if (rowIndex != -1)
                {
                    this.board[rowIndex, columnIndex] = this.turn;
                    if (this.turn == 1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Blue, 32 + 48 * columnIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                    else if (this.turn == 2)
                    {
                        Graphics graphics = this.CreateGraphics();
                        graphics.FillEllipse(Brushes.Red, 32 + 48 * columnIndex, 32 + 48 * rowIndex, 32, 32);

                    }
                    int winner = this.winnerPlayer(this.turn);
                    if (winner != -1)
                    {
                        string player = (winner == 1) ? "blue" : "red";
                        MessageBox.Show( "\t"+ player + " won" + "\n\n congratulations for " + player );
                        Application.Restart();
                    }
                    if (this.turn == 1)
                        this.turn = 2;
                    else
                        this.turn = 1;
                    counter += e.Clicks;
                    if (counter == 42)
                    {
                        MessageBox.Show("oops, it's a draw");
                        Application.Restart();
                    }
                }
            }

        }



        private int winnerPlayer(int playerToCheck)
        {
            //1) Vertical winning check
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col], this.board[row + 2, col], this.board[row + 3, col]))
                    {
                        return playerToCheck;
                    }
                }
            }
            //horizontal

            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row, col + 1], this.board[row, col + 2], this.board[row, col + 3]))
                    {
                        return playerToCheck;
                    }

                }
            }
            //diagonal 

            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col + 1], this.board[row + 2, col + 2], this.board[row + 3, col + 3]))
                    {
                        return playerToCheck;
                    }

                }


            }
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 3; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col - 1], this.board[row + 2, col - 2], this.board[row + 3, col - 3]))
                    {
                        return playerToCheck;
                    }

                }
            }






            return -1;

        }
        private bool AllNumbersEqual(int toCheck, params int[] numbers)
        {
            foreach (int num in numbers)
            {
                if (num != toCheck)
                    return false;
            }
            return true;


        }
        private int columnNumber(Point mouse)
        {
            for (int i = 0; i < this.boardColumns.Length; i++)
            {
                if ((mouse.X >= this.boardColumns[i].X) && (mouse.Y >= this.boardColumns[i].Y))
                {
                    if ((mouse.X <= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int EmptyRow(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (this.board[i, col] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
         Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
          
        }

       
    }
}
