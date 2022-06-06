using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class EntityBase: MonoBehaviour
    {
        private const bool T = true;
        private const bool F = false;
        //public GameObject cam;
        protected EntityHandler handler;

        protected bool[][] playerMap = !Global.IsBossFightMap ? new bool[][]
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
        } : new bool[][]
        {
            new bool[] {F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, F},
            new bool[] {F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F},
        };

        public static Vector3 GetCurrentWorldPos(int x, int y)
            => new(x, EntityHandler.IsInversedMap ? -y : y);

        protected (int x, int y) GetCurrentMapPos() => GetCurrentMapPos(transform.position);

        public static (int x, int y) GetCurrentMapPos(Vector3 vec)
            => ((int) vec.x, (int) Mathf.Abs(vec.y));

        public bool ValidatePos() => ValidatePos(transform.localPosition);

        protected virtual bool ValidatePos(Vector3 vec)
        {
            var normAng = transform.rotation.eulerAngles.z % 180;
            if (normAng < 0) normAng += 180;
            var (rw, rh) = normAng is < 135 and > 45 ? (height, width) : (width, height);
            
            if (vec.y < 0) vec.y = -vec.y;
            vec += new Vector3(0.5f, 0.5f);
            for(var i = (int)(vec.x - rw / 2); i <= vec.x + rw / 2; i++)
                for(var j = (int)(vec.y - rh/ 2); j <= vec.y + rh / 2; j++)
                    if (!playerMap[j][i])
                        return false;
            return true;
        }

        protected virtual void Start()
        {
            handler = GameObject.Find("Main Camera").GetComponent<EntityHandler>();
        }

        public abstract float width { get; }
        public abstract float height { get; }

        public static bool IsCollide(EntityBase current, EntityBase other)
        {
            var position = other.transform.position;
            var position1 = current.transform.position;
            
            return !(position.x - other.width / 2 > position1.x + current.width / 2 ||
                     position.x + other.width / 2 < position1.x - current.width / 2) &&
                   !(position.y - other.height / 2 >
                     position1.y + current.height / 2 ||
                     position.y + other.height / 2 < position1.y - current.height / 2);
        }
    }
}