using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SquareChaser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //global variables

        //player, power up, ball, and border dimensions
        Rectangle player1 = new Rectangle(30, 50, 25, 25);
        Rectangle player2 = new Rectangle(360, 50, 25, 25);
        Rectangle powerUp = new Rectangle(200, 300, 10, 10);
        Rectangle point = new Rectangle(100, 200, 15, 15);
        Rectangle border = new Rectangle(20, 20, 390, 390);

        //players score
        int player1score = 0;
        int player2score = 0;

        //player speed and power up moving
        int player1Speed = 5;
        int player2Speed = 5;

        //player 1 movement keys
        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;

        //player 2 movement keys
        bool upArrowPressed = false;
        bool downArrowPressed = false;
        bool leftArrowPressed = false;
        bool rightArrowPressed = false;

        //pens and brushes
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Pen redPen = new Pen(Color.Red);

        //random generator and stopwatch
        Random randgen = new Random();
        Stopwatch p1stopwatch = new Stopwatch();
        Stopwatch p2stopwatch = new Stopwatch();

        //Sound
        SoundPlayer collision = new SoundPlayer(Properties.Resources.Collision_Sound);


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //switch statements for movement
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.Up:
                    upArrowPressed = false;
                    break;
                case Keys.Down:
                    downArrowPressed = false;
                    break;
                case Keys.Left:
                    leftArrowPressed = false;
                    break;
                case Keys.Right:
                    rightArrowPressed = false;
                    break;
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //switch statements for movement
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.Up:
                    upArrowPressed = true;
                    break;
                case Keys.Down:
                    downArrowPressed = true;
                    break;
                case Keys.Left:
                    leftArrowPressed = true;
                    break;
                case Keys.Right:
                    rightArrowPressed = true;
                    break;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //shows whats on screen
            e.Graphics.DrawRectangle(redPen, border);
            e.Graphics.FillRectangle(greenBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, point);
            e.Graphics.FillRectangle(yellowBrush,powerUp);

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            //player 1 movement
            if (wPressed == true && player1.Y > border.Y)
            {
                player1.Y -= player1Speed;
            }

            if(sPressed == true && player1.Y < border.Y + border.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            if (aPressed == true && player1.X > border.X)
            {
                player1.X -= player1Speed;
            }

            if(dPressed == true && player1.X < border.X + border.Width - player1.Width)
            {
                player1.X += player1Speed;
            }

            //Player 2 movement
            if (upArrowPressed == true && player2.Y > border.Y)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowPressed == true && player2.Y < border.Y + border.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (leftArrowPressed == true && player2.X > border.X)
            {
                player2.X -= player2Speed;
            }

            if (rightArrowPressed == true && player2.X < border.X + border.Width - player2.Width)
            {
                player2.X += player2Speed;
            }



            //Ball and power up respawn and player score
            if (player1.IntersectsWith(point))
            {
                collision.Play();
                player1score++;
                player1scoreLabel.Text = $"P1:{player1score}";
                point.X = randgen.Next(30, 380);
                point.Y = randgen.Next(30, 380);
            }

            if (player2.IntersectsWith(point))
            {
                collision.Play();
                player2score++;
                player2scoreLabel.Text = $"P2:{player2score}";
                point.X = randgen.Next(30, 380);
                point.Y = randgen.Next(30, 380);
            }

            if (player1.IntersectsWith(powerUp) && p1stopwatch.ElapsedMilliseconds == 0)
            {
                collision.Play();
                player1Speed *= 2;
                powerUp.X = randgen.Next(30, 380);
                powerUp.Y = randgen.Next(30, 380);
                p1stopwatch.Start();

            }

            if (p1stopwatch.ElapsedMilliseconds > 3000)
            {
                player1Speed /= 2;
                p1stopwatch.Restart();
                p1stopwatch.Stop();
            }

            if (player2.IntersectsWith(powerUp) && p2stopwatch.ElapsedMilliseconds == 0)
            {
                collision.Play();
                player2Speed *= 2;
                powerUp.X = randgen.Next(30, 380);
                powerUp.Y = randgen.Next(30, 380);
                p2stopwatch.Start();

            }

            if (p2stopwatch.ElapsedMilliseconds > 3000)
            {
                player2Speed /= 2;
                p2stopwatch.Restart();
                p2stopwatch.Stop();
            }

            if (player1score == 5)
            {
                gameTimer.Stop();
                outputLabel.Text = "The Winner is: Player 1!";
            }
            else if (player2score == 5)
            {
                gameTimer.Stop();
                outputLabel.Text = "The Winner is: Player 2!";
            }

            Refresh();
        }

       
    }
}
