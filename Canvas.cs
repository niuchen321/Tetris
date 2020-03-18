using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 主题画板
    /// </summary>
    public class Canvas
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public Canvas(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;
            CurrentHeight = 0;
            Graphs = new int[Columns, Rows];
            CurrentScore = 0;
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// 当前高度
        /// </summary>
        public int CurrentHeight { get; set; }
        /// <summary>
        /// 图表信息数组
        /// </summary>
        public int[,] Graphs { get; set; }
        /// <summary>
        /// 当前分数
        /// </summary>
        public int CurrentScore { get; set; }
        /// <summary>
        /// 当前砖块
        /// </summary>
        public Brick CurrentBrick { get; set; }
        /// <summary>
        /// 下一个砖块
        /// </summary>
        public Brick NextBrick { get; set; }
        /// <summary>
        /// 定时器砖块下移
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            //判断是否为空
            lock(Graphs)
            {
                if (CurrentBrick==null&&NextBrick==null)
                {
                    CurrentBrick = GetBrick();
                    NextBrick = GetBrick();
                    NextBrick.RandomShape();
                    CurrentBrick.SetCenterPos(Columns / 2 - 1, CurrentBrick.Appear());
                    SetGraphs();
                }
                else if (CurrentBrick==null)
                {
                    CurrentBrick = NextBrick;
                    NextBrick = GetBrick();
                    NextBrick.RandomShape();
                    CurrentBrick.SetCenterPos(Columns / 2 - 1, CurrentBrick.Appear());
                    SetGraphs();
                }
                else
                {
                    if (CurrentBrick.CanDropMove(Graphs,Rows,Columns))
                    {
                        //清除数据
                        SetGraphs(true);
                        CurrentBrick.DropMove();
                        SetGraphs();
                    }
                    else
                    {
                        CurrentBrick = null;
                        CurrentHeightCount();
                        ClearRow();
                    }
                }
            }

            if (CurrentScore>=100||CurrentHeight>=Rows)
            {
                return false;
            }

            return true;
        }

        //随机获取一个砖块
        private  Brick GetBrick()
        {
            Random random = new Random();
            int index =  random.Next(7);
            Brick brick;
            switch (index)
            {
                case 0:
                    brick = new Brick1();
                    break;
                case 1:
                    brick = new Brick2();
                    
                    break;
                case 2:
                    brick = new Brick3();
                    break;
                case 3:
                    brick = new Brick4();
                    break;
                case 4:
                    brick = new Brick5();
                    break;
                case 5:
                    brick = new Brick6();
                    break;
                case 6:
                    brick = new Brick7();
                    break;
                default:
                    brick = new Brick1();
                    break;
            }
           
            return brick;
        }
        /// <summary>
        /// 根据砖块设置图表值
        /// </summary>
        /// <param name="isClear">是否清除数据</param>
        private void SetGraphs(bool isClear=false)
        {
            for (int i = 0; i < CurrentBrick.NeedColumns; i++)
            {
                for (int j = 0; j < CurrentBrick.NeedRows; j++)
                {
                    int realX = CurrentBrick.Location.X + (j - CurrentBrick.BrickCenterPoint.X);
                    int realY = CurrentBrick.Location.Y + (i - CurrentBrick.BrickCenterPoint.Y);

                    if (realX<0||realX>=Columns||realY<0||realY>=Rows)
                    {
                        continue;
                    }

                    if (CurrentBrick.DistortionRangge[i,j]==1)
                    {
                        if (isClear)
                        {
                            Graphs[realX, realY] = 0;
                        }
                        else
                        {
                            Graphs[realX, realY] = 1;
                        }                        
                    }
                }
            }
        }

        //计算当期高度
        private void CurrentHeightCount()
        {
            CurrentHeight = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Graphs[j, i] == 1)
                    {
                        CurrentHeight ++;
                       break;
                    }
                }
            }
        }

        /// <summary>
        /// 清除当前填满的行,有消除加分
        /// </summary>
        private void ClearRow()
        {
            //清空行集合
            List<int> clearRows = new List<int>();
            //遍历所有存在砖块的行
            for (int i = Rows - 1; i >=Rows- CurrentHeight; i--)
            {
                //是否清空
                var isClear = true;

                for (int j = 0; j < Columns; j++)
                {
                    //行存在空白，结束行循环
                    if (Graphs[j,i]==0)
                    {
                        isClear = false;
                        break; ;
                    }
                }
                if (isClear)
                {
                    for (int k = 0; k < Columns; k++)
                    {
                        //清空行
                        Graphs[ k,i] = 0;
                    }
                    CurrentScore++;
                    clearRows.Add(i);
                }
            }
            //存在清空行，将该行上面所有砖块下移
            //最下面空行和第二空行之间下移一行，2，3行之间下移2行，以此类推
            //最高砖块行减去清空行数
            if (clearRows.Count>0)
            {
                if (clearRows.Count< CurrentHeight)
                {
                    for (int i = 1; i <= clearRows.Count; i++)
                    {
                        //如果是最高清空行，需要下移该行和最高行砖块
                        if (i == clearRows.Count)
                        {
                            for (int j = clearRows[i - 1]-1; j >= Rows - CurrentHeight; j--)
                            {
                                for (int k = 0; k < Columns; k++)
                                {
                                    Graphs[ k, j + i] = Graphs[ k, j];
                                }
                            }
                        }
                        else//不是最高清空行，下移该行和上一行之间砖块
                        {
                            for (int j = clearRows[i - 1] - 1; j > clearRows[i]; j--)
                            {
                                for (int k = 0; k < Columns; k++)
                                {
                                    Graphs[k, j + i] = Graphs[ k,j];
                                }
                            }
                        }
                    }
                }
               

                for (int i =Rows- CurrentHeight; i <Rows- CurrentHeight+clearRows.Count; i++)
                {
                    for (int k = 0; k < Columns ; k++)
                    {
                        Graphs[k, i] = 0;
                    }
                }

                CurrentHeight -= clearRows.Count;
            }
            
        }

        /// <summary>
        /// 砖块变形
        /// </summary>
        public void BrickTransform()
        {
            lock (Graphs)
            {
                if (CurrentBrick!=null&&CurrentBrick.CanTransform(Graphs,Rows,Columns))
                {
                    SetGraphs(true);
                    CurrentBrick.Transform();
                    SetGraphs();
                }
            }
        }

        /// <summary>
        /// 砖块左移
        /// </summary>
        public void BrickLeftMove()
        {
            lock (Graphs)
            {
                if (CurrentBrick != null && CurrentBrick.CanLeftMove(Graphs, Rows, Columns))
                {
                    SetGraphs(true);
                    CurrentBrick.LeftMove();
                    SetGraphs();
                }
            }
        }

        /// <summary>
        /// 砖块右移
        /// </summary>
        public void BrickRightMove()
        {
            lock (Graphs)
            {
                if (CurrentBrick != null && CurrentBrick.CanRightMove(Graphs, Rows, Columns))
                {
                    SetGraphs(true);
                    CurrentBrick.RightMove();
                    SetGraphs();
                }
            }
        }

        /// <summary>
        /// 砖块下移
        /// </summary>
        public void BrickDownMove()
        {
            lock (Graphs)
            {
                if (CurrentBrick != null && CurrentBrick.CanDropMove(Graphs, Rows, Columns))
                {
                    SetGraphs(true);
                    CurrentBrick.DropMove();
                    SetGraphs();
                }
            }
        }
    }
}
