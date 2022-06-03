using System;
using System.Collections;
using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;

using static System.Linq.Enumerable;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class EntityBase: MonoBehaviour
    {
        private const bool T = true;
        private const bool F = false;
        public GameObject cam;

        protected static bool[][] playerMap = new bool[][]
        {
            new bool[] {F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F},
            new bool[] {F, T, F, F, T, T, T, T, T, T, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F, F, F, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F, F, F, F},
            new bool[] {F, F, T, T, F, F, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, F, F, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, F, F, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, F, F, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, F, F, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, F, F, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F, F, F},
            new bool[] {F, F, F, F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, F, F, F, F, T, T, T, T, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, T, T, T, T, F},
            new bool[] {F, F, F, F, F, F, T, T, T, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, T, T, T, T, F},
            new bool[] {F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F},
        };
        
        public Vector3 GetCurrentWorldPos(int x, int y)
            => new(x, cam.GetComponent<EntityHandler>().isInversedMap ? -y : y);
        
        public bool ValidatePos() => ValidatePos(transform.localPosition);

        protected static bool ValidatePos(Vector3 vec)
        {
            if (vec.y < 0) vec.y = -vec.y;
            return playerMap[(int) vec.y][(int) vec.x] && playerMap[(int) (vec.y + 0.9)][(int) vec.x] &&
                playerMap[(int) vec.y][(int) (vec.x + 0.9)] && playerMap[(int) (vec.y + 0.9)][(int) (vec.x + 0.9)];
        }

        public abstract float width { get; set; }
        public abstract float height { get; set; }

        public static bool IsCollide(EntityBase current, EntityBase other)
        {
            return !(other.transform.position.x - other.width / 2 > current.transform.position.x + current.width / 2 ||
                     other.transform.position.x + other.width / 2 < current.transform.position.x - current.width / 2) &&
                   !(other.transform.position.y - other.height / 2 >
                     current.transform.position.y + current.height / 2 ||
                     other.transform.position.y + other.height / 2 < current.transform.position.y - current.height / 2);
        }
    }
}