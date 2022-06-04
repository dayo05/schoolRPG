using System;
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
        private void Start()
        {
            Atk.Add(ChairAtk);
            Atk.Add(ArrowAtk);
        }

        protected override float MoveDist => 4.0f * Time.deltaTime;

        // Update is called once per frame
        private new void Update()
        {
            base.Update();
            if (Input.GetKey(KeyCode.W))
                TryMoveBy(Up);

            if (Input.GetKey(KeyCode.A))
                TryMoveBy(Left);

            if (Input.GetKey(KeyCode.S))
                TryMoveBy(Down);

            if (Input.GetKey(KeyCode.D))
                TryMoveBy(Right);

            if (Input.GetKeyDown(KeyCode.J))
                TryShoot(0, lastMoveDirection.I());
            if (Input.GetKeyDown(KeyCode.UpArrow))
                TryShoot(1, Up.I());
            if (Input.GetKeyDown(KeyCode.DownArrow))
                TryShoot(1, Down.I());
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                TryShoot(1, Left.I());
            if (Input.GetKeyDown(KeyCode.RightArrow))
                TryShoot(1, Right.I());
        }

        public override double MaxHp { get; protected set; } = 20;

        protected override float NuckbackDist => MoveDist;

        public override float width { get; set; } = 0.9f;
        public override float height { get; set; } = 0.9f;

        protected override (bool, Vector3) TryMoveBy(Direction direction, float? dist = null)
        {
            var d = base.TryMoveBy(direction, dist);
            var mp = GetCurrentMapPos(d.Item2);
            
            switch (mp.x)
            {
                case >= 2 and <= 3 when mp.y == 1:
                    cam.GetComponent<EntityHandler>().MoveToPreviousLevel();
                    break;
                case >= 26 and <= 27 when mp.y == 1:
                    cam.GetComponent<EntityHandler>().MoveToNextLevel();
                    break;
            }
            return d;
        }
    }
}
