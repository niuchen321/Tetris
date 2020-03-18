using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 坐标点
    /// </summary>
   public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// x坐标
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// y坐标
        /// </summary>
        public int Y { get; set; }
    }
}
