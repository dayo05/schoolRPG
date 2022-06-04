using UnityEngine;
using static SchoolRPG.GameMain.Entity.Direction;

namespace SchoolRPG.GameMain.Entity
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
    }
}