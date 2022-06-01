using UnityEngine;

namespace SchoolRPG.GameMain
{
    public abstract class EntityBase: MonoBehaviour
    {
        private static bool[][] playerMap = new bool[][]
        {
            new bool[] {false, },
            new bool[] {true,}
        };

        protected bool IsPlayerLocated((int x, int y) t)
        {
            return t.x is > 1 and < 31 && t.y is > 1 and < 17;
            return playerMap[t.y][t.x];
        }

        protected bool ValidatePos() => IsPlayerLocated(GetCurrentMapPos());
        protected bool ValidatePos(Vector3 vec) => IsPlayerLocated(GetCurrentMapPos(vec));

        private float lastShootTime;
        protected float DeltaShootTime { get; set; }
        
        protected bool TryShoot()
        {
            if (Time.time < lastShootTime + DeltaShootTime) return false;
            lastShootTime = Time.time;
            Shoot();
            return true;
        }

        protected abstract void Shoot();
        
        public int Hp { get; protected set; }
        public int Atk { get; protected set; }

        protected (int, int) GetCurrentMapPos() => GetCurrentMapPos(transform.localPosition);
        
        protected (int, int) GetCurrentMapPos(Vector3 vec)
            => ((int)(vec.x * 18 / 10) + 16,
                (int)(vec.y * 18 / 10) + 9);

        protected Vector3 GetCurrentWorldPos(int x, int y)
            => new((x - 15.5f) * 10.0f / 18, (y - 8.5f) * 10.0f / 18, 0);
    }
}