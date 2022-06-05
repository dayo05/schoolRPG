using SchoolRPG.GameMain.Utils.AtkParticle;
using UnityEngine;

using static System.Linq.Enumerable;
using static SchoolRPG.GameMain.Utils.Direction;

namespace SchoolRPG.GameMain.Utils
{
    public class Player : UnitBase
    {
        public GameObject ChairAtk;

        public GameObject ArrowAtk;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Atk.Add(ChairAtk);
            Atk.Add(ArrowAtk);
        }

        protected override float MoveDist => 4.0f * Time.deltaTime;

        // Update is called once per frame
        private void Update()
        {
            lastShootTime ??= new(Repeat<float>(0, Atk.Count));
            foreach (var i in Range(0, Atk.Count))
            {
                var tr = transform.GetChild(i + 1);
                var rng = (Time.time - lastShootTime[i]) / Atk[i].GetComponent<AtkParticleBase>().DeltaTime;
                if (rng > 1) rng = 1;
                tr.localPosition = new Vector3((float) (-0.5 + rng / 2), 0.7f + i * 0.1f, 0);
                tr.localScale = new Vector3(rng, 0.1f, 0);
            }

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

        public override double MaxHp => 20;

        protected override float NuckbackDist => MoveDist;

        public override float width => 0.9f;
        public override float height => 0.9f;

        protected override (bool, Vector3) TryMoveBy(Direction direction, float? dist = null)
        {
            var d = base.TryMoveBy(direction, dist);
            var mp = GetCurrentMapPos(d.Item2);

            switch (mp.x)
            {
                case >= 2 and <= 3 when mp.y == 1:
                    handler.MoveToPreviousLevel();
                    break;
                case >= 26 and <= 27 when mp.y == 1:
                    handler.MoveToNextLevel();
                    break;
            }

            return d;
        }

        protected override void OnDie()
        {
            Debug.Log("Game Over");
            base.OnDie();
        }
    }
}
