using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;

using static SchoolRPG.GameMain.Entity.Direction;

namespace SchoolRPG.GameMain.Entity
{
    public class Player : UnitBase
    {
        public GameObject ChairAtk;
        public GameObject ArrowAtk;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = GetCurrentWorldPos(2, 2);
            Atk.Add(ChairAtk);
            Atk.Add(ArrowAtk);
        }

        private const float moveDist = 0.05f;
        private Direction direction = Right;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W) && ValidatePos(transform.position + moveDist * Vector3.up))
            {
                transform.position += moveDist * Vector3.up;
                direction = Up;
            }

            if (Input.GetKey(KeyCode.A) && ValidatePos(transform.position + moveDist * Vector3.left))
            {
                transform.position += moveDist * Vector3.left;
                direction = Left;
            }

            if (Input.GetKey(KeyCode.S) && ValidatePos(transform.position + moveDist * Vector3.down))
            {
                transform.position += moveDist * Vector3.down;
                direction = Down;
            }

            if (Input.GetKey(KeyCode.D) && ValidatePos(transform.position + moveDist * Vector3.right))
            {
                transform.position += moveDist * Vector3.right;
                direction = Right;
            }

            if (Input.GetKeyDown(KeyCode.J))
                TryShoot(0, direction.I());
            if (Input.GetKeyDown(KeyCode.UpArrow))
                TryShoot(1, Up.I());
            if (Input.GetKeyDown(KeyCode.DownArrow))
                TryShoot(1, Down.I());
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                TryShoot(1, Left.I());
            if (Input.GetKeyDown(KeyCode.RightArrow))
                TryShoot(1, Right.I());
        }

        public override double Hp { get; set; }
        protected override float NuckbackDist { get; set; } = moveDist;
        
        public override float width { get; set; } = 1;
        public override float height { get; set; } = 1;
    }
}
