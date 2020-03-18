using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 主画板
        /// </summary>
        public Canvas canvas;

        //网格宽高
        public float width;
        public float height;
        //是否开始
        private bool isRun = false;
        //当前等级
        private int Level = 1;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            isRun = false;
            pictureBox1.Focus();
            canvas = new Canvas(15, 20);

            //网格宽高
            width = pictureBox1.Width / canvas.Columns;
            height = pictureBox1.Height / canvas.Rows;

            timer1.Enabled = true;
            isRun = true;
            timer1.Start();
        }
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (canvas.Run())
            {
                Draw();
                DrawMinCanvas();
                textBox1.Text =$"分数：{canvas.CurrentScore} 分";

                if (canvas.CurrentScore>0&&canvas.CurrentScore/10>=Level)
                {
                    Level++;
                    timer1.Stop();
                    FormMessageBox formMessage = new FormMessageBox($"恭喜你升到{Level}级！");
                    
                    formMessage.Show();

                    timer1.Start();

                    timer1.Interval += 100;
                }
            }
            else
            {

                timer1.Stop();
                if (canvas.CurrentScore >= 100)
                {
                    if (MessageBox.Show("游戏胜利！是否继续游戏？","提示",MessageBoxButtons.OKCancel,
           MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        button1_Click(sender,e);
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    if (MessageBox.Show("游戏失败！是否继续游戏？", "提示", MessageBoxButtons.OKCancel,
          MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        button1_Click(sender, e);
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }


        }
        /// <summary>
        /// 绘图
        /// </summary>
        private void Draw()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < canvas.Columns; i++)
            {
                for (int j = 0; j < canvas.Rows; j++)
                {
                    if (canvas.Graphs[i, j] == 1)
                    {
                        graphics.FillRectangle(Brushes.Blue, i*width-1, j*height-1, width - 2, height - 2);
                    }
                }
            }

            pictureBox1.BackgroundImage = bitmap;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// 绘图小画板
        /// </summary>
        private void DrawMinCanvas()
        {
            Bitmap bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            int[,] arr = new int[5, 5]{{0,0,0,0,0},
                                         {0,0,0,0,0},
                                         {0,0,0,0,0},
                                         {0,0,0,0,0},
                                         {0,0,0,0,0,}};
            switch (canvas.NextBrick.NeedColumns)
            {
                case 2:
                    arr[2, 2] = 1;
                    arr[2, 3] = 1;
                    arr[3, 2] = 1;
                    arr[3, 3] = 1;
                    break;
                case 3:
                    for (int i = 1, m = 0; i < 4; i++, m++)
                    {
                        for (int j = 1, n = 0; j < 4; j++, n++)
                        {
                            arr[i, j] = canvas.NextBrick.DistortionRangge[m, n];
                        }
                    }
                    break;
                case 5:
                    arr = canvas.NextBrick.DistortionRangge;
                    break;
                default:
                    return;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        graphics.FillRectangle(Brushes.Blue, j * pictureBox2.Width/5, i * pictureBox2.Height/5, pictureBox2.Width/5 - 2, pictureBox2.Height/5 - 2);
                    }
                }
            }

            pictureBox2.BackgroundImage = bitmap;
            pictureBox2.Refresh();
        }
        /// <summary>
        /// 键盘控制砖块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isRun)
            {
                if (e.KeyCode==Keys.Up)
                {
                    pictureBox1.Refresh();
                    canvas.BrickTransform();
                    Draw();
                }
                if (e.KeyCode==Keys.Left)
                {
                    pictureBox1.Refresh();
                    canvas.BrickLeftMove();
                    Draw();
                }
                if (e.KeyCode == Keys.Right)
                {
                    pictureBox1.Refresh();
                    canvas.BrickRightMove();
                    Draw();
                }
                if (e.KeyCode == Keys.Down)
                {
                    pictureBox1.Refresh();
                    canvas.BrickDownMove();
                    Draw();
                }
            }
        }

        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            timer1.Start();
            pictureBox1.Focus();
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            timer1.Stop();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
            "窗口关闭后，数据即将丢失！是否现在关闭窗口",
            "提示",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
           
        }
    }
}
