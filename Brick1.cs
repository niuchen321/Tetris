using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 田形砖块
    /// </summary>
    public class Brick1 : Brick
    {
        public Brick1()
        {
            this.NeedColumns = 2;
            NeedRows = 2;
            CurrentShape = 0;
            DistortionRangge = new int[2, 2] { { 1, 1 }, { 1, 1 } };
            BrickCenterPoint = new Point(0, 0);
        }

        /// <summary>
        /// 获取砖块出现时中心点的y坐标
        /// </summary>
        /// <returns></returns>
        public override int Appear()
        {
           return -1;
        }
        /// <summary>
        /// 能否下移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public override bool CanDropMove(int[,] arr, int rows, int columns)
        {
            if (Location.Y == rows-2 || arr[Location.X , Location.Y + 2] == 1 || arr[Location.X + 1, Location.Y + 2] == 1)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 能否左移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public override bool CanLeftMove(int[,] arr, int rows, int columns)
        {
            if (Location.Y<0)
            {
                if (Location.X==0||arr[Location.X-1,Location.Y+1]==1)
                {
                    return false;
                }
                return true;
            }

            if (Location.X==0||arr[Location.X-1,Location.Y+1]==1||arr[Location.X-1,Location.Y]==1)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 能否右移
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public override bool CanRightMove(int[,] arr, int rows, int columns)
        {
            if (Location.Y < 0)
            {
                if (Location.X == columns-2 || arr[Location.X +2, Location.Y + 1] == 1)
                {
                    return false;
                }
                return true;
            }

            if (Location.X == columns-2 || arr[Location.X +2, Location.Y + 1] == 1 || arr[Location.X +2, Location.Y ] == 1)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 能否变形
        /// </summary>
        /// <param name="arr">画布模型</param>
        /// <param name="rows">画布行数</param>
        /// <param name="columns">画布列数</param>
        /// <returns></returns>
        public override bool CanTransform(int[,] arr, int rows, int columns)
        {
            return false;
        }
        /// <summary>
        /// 变形
        /// </summary>
        public override void Transform()
        {
            return;
        }
    }
}
