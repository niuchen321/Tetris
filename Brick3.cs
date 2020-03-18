using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 土形砖块
    /// </summary>
    public class Brick3 : Brick
    {
        public Brick3()
        {
            this.NeedColumns = 3;
            NeedRows = 3;
            CurrentShape = 0;
            DistortionRangge = new int[3, 3]{{0,1,0},
                                         {1,1,1},
                                         {0,0,0}};
            BrickCenterPoint = new Point(1, 1);
        }

        /// <summary>
        /// 获取砖块出现时中心点的y坐标
        /// </summary>
        /// <returns></returns>
        public override int Appear()
        {
            switch (CurrentShape)
            {
                case 0:
                    return 0;
                case 1:
                case 2:
                case 3:
                    return -1;
                default:
                    throw new NotImplementedException();
            }
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
            switch (CurrentShape)
            {
                case 0:
                    if (Location.Y == rows - 1 ||(Location.Y>-2&& (arr[Location.X - 1, Location.Y + 1] == 1 || arr[Location.X, Location.Y + 1] == 1 || arr[Location.X + 1, Location.Y + 1] == 1)))
                    {
                        return false;
                    }
                    return true;
                case 1:
                    if (Location.Y == rows - 2 || arr[Location.X, Location.Y + 2] == 1||(Location.Y>-2 &&arr[Location.X+1,Location.Y+1]==1))
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (Location.Y == rows - 2 ||(Location.Y>-2&&( arr[Location.X - 1, Location.Y + 1] == 1 || arr[Location.X + 1, Location.Y + 1] == 1)) || arr[Location.X, Location.Y + 2] == 1 )
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (Location.Y == rows - 2 || arr[Location.X, Location.Y + 2] == 1 || (Location.Y > -2 && arr[Location.X-1, Location.Y + 1] == 1))
                    {
                        return false;
                    }
                    return true;
                default:
                    throw new NotImplementedException();
            }
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
            switch (CurrentShape)
            {
                case 0:
                    if (Location.X <= 1 ||(Location.Y > -1 && arr[Location.X - 2, Location.Y] == 1) || (Location.Y > 0&&arr[Location.X - 1, Location.Y-1] == 1))
                    {
                        return false;
                    }
                    return true;
                case 1:
                    if (Location.X <= 0 || (Location.Y > 0 && arr[Location.X - 1, Location.Y - 1] == 1) || (Location.Y >= 0 && arr[Location.X - 1, Location.Y] == 1) || (Location.Y > -2 && arr[Location.X - 1, Location.Y + 1] == 1 ))
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (Location.X <= 1 || (Location.Y >= 0 && arr[Location.X - 2, Location.Y] == 1) || (Location.Y > -2 && arr[Location.X - 1, Location.Y+1] == 1))
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (Location.X <= 1 || (Location.Y > 0 && arr[Location.X - 1, Location.Y - 1] == 1) || (Location.Y >= 0 && arr[Location.X - 2, Location.Y] == 1) || (Location.Y > -2 && arr[Location.X - 1, Location.Y + 1] == 1 ))
                    {
                        return false;
                    }
                    return true;
                default:
                    throw new NotImplementedException();
            }
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
            switch (CurrentShape)
            {
                case 0:
                    if (Location.X >= columns-2 || (Location.Y >= 0 && arr[Location.X + 2, Location.Y] == 1) || (Location.Y > 0 && arr[Location.X + 1, Location.Y - 1] == 1 ))
                    {
                        return false;
                    }
                    return true;
                case 1:
                    if (Location.X >= columns-2 || (Location.Y > 0 && arr[Location.X + 1, Location.Y - 1] == 1 ) || (Location.Y >= 0 && arr[Location.X + 2, Location.Y] == 1 ) || (Location.Y > -2 && arr[Location.X + 1, Location.Y + 1] == 1))
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (Location.X >=columns-2 || (Location.Y >= 0 && arr[Location.X + 2, Location.Y] == 1) || (Location.Y > -2 && arr[Location.X + 1, Location.Y + 1] == 1))
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (Location.X >= columns-1 || (Location.Y > 0 && arr[Location.X + 1, Location.Y - 1] == 1 ) || (Location.Y >= 0 && arr[Location.X +1, Location.Y] == 1) || (Location.Y > -2 && arr[Location.X + 1, Location.Y + 1] == 1))
                    {
                        return false;
                    }
                    return true;
                default:
                    throw new NotImplementedException();
            }
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
            switch (CurrentShape)
            {
                case 0:
                    if (Location.Y > rows - 2 || (Location.Y >-2 && arr[Location.X, Location.Y + 1] == 1))
                    {
                        return false;
                    }
                    return true;
                case 1:
                    if (Location.X<=0||(Location.Y >= 0 && arr[Location.X - 1, Location.Y] == 1 ))
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (Location.Y > 0 && arr[Location.X, Location.Y - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (Location.X>=columns-1||(Location.Y >= 0 && arr[Location.X + 1, Location.Y] == 1))
                    {
                        return false;
                    }
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 变形
        /// </summary>
        public override void Transform()
        {
            switch (CurrentShape)
            {
                case 0:
                    DistortionRangge = new int[3, 3]{{0,1,0},
                                            {0,1,1},
                                            {0,1,0}};
                    CurrentShape = 1;
                    break;
                case 1:
                    DistortionRangge = new int[3, 3]{{0,0,0},
                                            {1,1,1},
                                            {0,1,0}};
                    CurrentShape = 2;
                    break;
                case 2:
                    DistortionRangge = new int[3, 3]{{0,1,0},
                                           {1,1,0},
                                           {0,1,0}};
                    CurrentShape = 3;
                    break;
                case 3:
                    DistortionRangge = new int[3, 3]{{0,1,0},
                                            {1,1,1},
                                            {0,0,0}};
                    CurrentShape = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
