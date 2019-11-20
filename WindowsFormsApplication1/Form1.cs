using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Label[] bricks = new Label[15];
        private int index = 0;
        private Label enemy = new Label();
        public Form1()
        {
            InitializeComponent();

            // set bricks
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    bricks[index] = new Label();
                    bricks[index].SetBounds(100 * j, 50 * i, 100, 50);
                    bricks[index].BackColor = Color.Red;
                    bricks[index].BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(bricks[index]);
                    index++;
                }
            }
            // set user control brick
            board.SetBounds(185, 500, 150, 30);
            board.BackColor = Color.Gray;
            board.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(board);
            board_x = board.Location.X;
            board_y = board.Location.Y;

            // set enemy brick
            enemy.SetBounds(200, 250, 100, 30);
            enemy.BackColor = Color.Gray;
            enemy.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(enemy);

            // timer
            timer1.Enabled = true;
            timer1.Interval = 1;

            // ball
            ball.Image = Image.FromFile(@"..\..\IMG\pp.gif");
            ball.SetBounds(230, 465, 35, 35);
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            ball.BackColor = Color.Transparent;
            this.Controls.Add(ball);

            ball_direction = rnd.Next(10, 20) % 1 + 1;
        }
        private PictureBox ball = new PictureBox();
        private bool ismove = false;
        private bool first_move = false;
        private int board_x, board_y,mouse_x1,mouse_x2;
        private int ball_direction = 1;
        private Random rnd = new Random();
        
        private void board_MouseDown(object sender, MouseEventArgs e)
        {
            ismove = true;
            first_move = true;
            mouse_x1 = Cursor.Position.X;
            board_x = board.Location.X;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ismove)
            {
                board.SetBounds(board_x-mouse_x1+mouse_x2, 500, 100, 30);
            }
            if (first_move)
            {
                if(ball_direction == 1) // left-up
                {
                    ball.SetBounds(ball.Location.X - 5, ball.Location.Y - 5, 35, 35);
                }
                else if(ball_direction == 2) // right-up
                {
                    ball.SetBounds(ball.Location.X + 5, ball.Location.Y - 5, 35, 35);
                }
                else if(ball_direction == 3) // right-down
                {
                    ball.SetBounds(ball.Location.X + 5, ball.Location.Y + 5, 35, 35);
                }
                else // left-down
                {
                    ball.SetBounds(ball.Location.X - 5, ball.Location.Y + 5, 35, 35);
                }
            }

            // check bound
            if (ball.Location.X <= 0)
            {
                switch (ball_direction)
                {
                    case 1:
                        ball_direction = 2;
                        break;
                    case 4:
                        ball_direction = 3;
                        break;
                }
            }
            if (ball.Location.X >= 465)
            {
                switch (ball_direction)
                {
                    case 2:
                        ball_direction = 1;
                        break;
                    case 3:
                        ball_direction = 4;
                        break;
                }
            }
            if (ball.Location.Y <= 0)
            {
                switch (ball_direction)
                {
                    case 1:
                        ball_direction = 4;
                        break;
                    case 2:
                        ball_direction = 3;
                        break;
                }
            }
            if (ball.Location.Y + 35 == board.Location.Y && ball.Location.X + 35 >= board.Location.X && ball.Location.X <= board.Location.X + board.Width) 
            {
                switch (ball_direction)
                {
                    case 3:
                        ball_direction = 2;
                        break;
                    case 4:
                        ball_direction = 1;
                        break;
                }
            }
            // check if hit bricks

            if (first_move)
            {
                for (int i = 0; i < 15; i++)
                {

                    if (ball.Location.Y + 35 == bricks[i].Location.Y && ball.Location.X + 35 >= bricks[i].Location.X && ball.Location.X <= bricks[i].Location.X + bricks[i].Width)
                    {
                        if (bricks[i].Enabled == true)
                        {
                            switch (ball_direction)
                            {
                                case 3:
                                    ball_direction = 2;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                                case 4:
                                    ball_direction = 1;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                            }
                        }
                    }
                    else if (ball.Location.Y == bricks[i].Location.Y + 50 && ball.Location.X + 35 >= bricks[i].Location.X && ball.Location.X <= bricks[i].Location.X + bricks[i].Width)
                    {
                        if (bricks[i].Enabled == true)
                        {
                            switch (ball_direction)
                            {
                                case 1:
                                    ball_direction = 4;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                                case 2:
                                    ball_direction = 3;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                            }
                        }
                    }

                    else if (ball.Location.X + 35 == bricks[i].Location.X && ball.Location.Y + 35 >= bricks[i].Location.Y && ball.Location.Y + 35 <= bricks[i].Location.Y + bricks[i].Height)
                    {
                        if (bricks[i].Enabled == true)
                        {
                            switch (ball_direction)
                            {
                                case 2:
                                    ball_direction = 1;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                                case 3:
                                    ball_direction = 4;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                            }
                        }
                    }

                    else if (ball.Location.X == bricks[i].Location.X + bricks[i].Width && ball.Location.Y + 35 >= bricks[i].Location.Y && ball.Location.Y <= bricks[i].Location.Y + bricks[i].Height)
                    {
                        if (bricks[i].Enabled == true)
                        {
                            switch (ball_direction)
                            {
                                case 1:
                                    ball_direction = 2;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                                case 4:
                                    ball_direction = 3;
                                    bricks[i].Enabled = false;
                                    bricks[i].Hide();
                                    break;
                            }
                        }
                    }
                }
            }
            // check if hit enemy
            if (ball.Location.Y + 35 == enemy.Location.Y && ball.Location.X + 35 >= enemy.Location.X && ball.Location.X <= enemy.Location.X + enemy.Width)
            {
                switch (ball_direction)
                {
                    case 3:
                        ball_direction = 2;
                        break;
                    case 4:
                        ball_direction = 1;
                        break;
                }
            }
            else if (ball.Location.Y == enemy.Location.Y + 50 && ball.Location.X + 35 >= enemy.Location.X && ball.Location.X <= enemy.Location.X + enemy.Width)
            {
                switch (ball_direction)
                {
                    case 1:
                        ball_direction = 4;
                        break;
                    case 2:
                        ball_direction = 3;
                        break;
                }
            }

            else if (ball.Location.X + 35 == enemy.Location.X && ball.Location.Y + 35 >= enemy.Location.Y && ball.Location.Y + 35 <= enemy.Location.Y + enemy.Height)
            {
                switch (ball_direction)
                {
                    case 2:
                        ball_direction = 1;
                        break;
                    case 3:
                        ball_direction = 4;
                        break;
                }
            }

            else if (ball.Location.X == enemy.Location.X + enemy.Width && ball.Location.Y + 35 >= enemy.Location.Y && ball.Location.Y <= enemy.Location.Y + enemy.Height)
            {
                switch (ball_direction)
                {
                    case 1:
                        ball_direction = 2;
                        break;
                    case 4:
                        ball_direction = 3;
                        break;
                }
            }

            if (ball.Location.Y >= 550)
            {
                timer1.Enabled = false;
                if(MessageBox.Show("Game Over") == DialogResult.OK)
                {
                    // reset timer
                    timer1.Enabled = true;

                    // reset bricks
                    for(int i = 0; i < 15; i++)
                    {
                        bricks[i].Show();
                        bricks[i].Enabled = true;
                    }
                    // set user control brick
                    board.SetBounds(185, 500, 150, 30);
                    board_x = board.Location.X;
                    board_y = board.Location.Y;
                    ismove = false;

                    // ball
                    ball.SetBounds(230, 465, 35, 35);

                    first_move = false;
                    ball_direction = rnd.Next(10, 20) % 1 + 1;

                }

            }
            int flag = 0;
            for(int i = 0; i < 15; i++)
            {
                if (bricks[i].Enabled == true)
                {
                    flag = 1;
                }
            }
            if(flag == 0)
            {
                timer1.Enabled = false;
                if (MessageBox.Show("Game Over") == DialogResult.OK)
                {
                    // reset timer
                    timer1.Enabled = true;

                    // reset bricks
                    for (int i = 0; i < 15; i++)
                    {
                        bricks[i].Show();
                        bricks[i].Enabled = true;
                    }
                    // set user control brick
                    board.SetBounds(185, 500, 150, 30);
                    board_x = board.Location.X;
                    board_y = board.Location.Y;
                    ismove = false;

                    // ball
                    ball.SetBounds(230, 465, 35, 35);

                    first_move = false;
                    ball_direction = rnd.Next(10, 20) % 1 + 1;

                }
            }

        }

        private void board_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismove)
            {
                mouse_x2 = Cursor.Position.X;
            }
        }

        private void board_MouseUp(object sender, MouseEventArgs e)
        {
            ismove = false;
        }
    }
}
