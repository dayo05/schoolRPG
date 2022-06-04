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
            transform.position = GetCurrentWorldPos(2, 2);
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

        public override float width { get; set; } = 1;
        public override float height { get; set; } = 1;
    }
}
