﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 砖块
    /// </summary>
   public abstract class Brick
    {
        /// <summary>
        /// 当前变形形状
        /// </summary>
        public int CurrentShape { get; set; }
        /// <summary>
        /// 需要行数
        /// </summary>
        public int NeedRows { get; set; }
        /// <summary>
        /// 需要列数
        /// </summary>
        public int NeedColumns { get; set; }
        /// <summary>
        /// 变形范围
        /// </summary>
        public int[,] DistortionRangge { get; set; }
        /// <summary>
        /// 砖块中心点
        /// </summary>
        public Point BrickCenterPoint { get; set; }
        /// <summary>
        /// 砖块位置
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// 能否变形（能变形的条件为在砖块的变形范围内不能有其他砖块）
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public abstract bool CanTransform(int[,] arr, int rows, int columns);

        /// <summary>
        /// 变形
        /// </summary>
        public abstract void Transform();

        /// <summary>
        /// 能否左移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public abstract bool CanLeftMove(int[,] arr, int rows, int columns);

        /// <summary>
        /// 左移
        /// </summary>
        public void LeftMove()
        {
            Location.X -= 1;
        }

        /// <summary>
        /// 能否右移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public abstract bool CanRightMove(int[,] arr, int rows, int columns);

        /// <summary>
        /// 右移
        /// </summary>
        public void RightMove()
        {
            Location.X += 1;
        }

        /// <summary>
        /// 能否下移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public abstract bool CanDropMove(int[,] arr, int rows, int columns);

        /// <summary>
        /// 下移
        /// </summary>
        public void DropMove()
        {
            Location.Y += 1;
        }

        /// <summary>
        /// 随机生成一个可以通过变形得到的形状
        /// </summary>
        public void RandomShape()
        {
            Random random = new Random();
            this.CurrentShape = random.Next(4);
            this.Transform();
        }

        /// <summary>
        /// 设置中心点相对于画布的位置
        /// </summary>
        /// <param name="x">横向位置</param>
        /// <param name="y">纵向位置</param>
        public void SetCenterPos(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 获取砖块出现时中心点的Y轴坐标
        /// </summary>
        /// <returns></returns>
        public abstract int Appear();
    }
}
