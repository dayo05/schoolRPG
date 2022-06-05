using Unity.Mathematics;
using UnityEngine;
using static SchoolRPG.GameMain.Utils.Direction;

namespace SchoolRPG.GameMain.Utils
{
    public enum Direction: int
    {
        Up, Down, Left, Right
    }
    
    public static class DirectionUtil {
        public static int I(this Direction d) => (int) d;

        public static Vector3 V(this Direction d) => d switch
        {
            Up => new Vector3(0, 1, 0),
            Down => new Vector3(0, -1, 0),
            Right => new Vector3(1, 0, 0),
            Left => new Vector3(-1, 0, 0)
        };

        public static Direction L(this Direction d) => d switch
        {
            Up => Left,
            Left => Down,
            Down => Right,
            Right => Up
        };

        public static Direction R(this Direction d) => d switch
        {
            Up => Right,
            Right => Down,
            Down => Left,
            Left => Up
        };

        public static Quaternion Q(this Direction d) => d switch
        {
            Up => Quaternion.Euler(0, 0, -90),
            Right => quaternion.Euler(0, 0, 0),
            Down => Quaternion.Euler(0, 0, 90),
            Left => Quaternion.Euler(0, 0, 180)
        };
    }
}