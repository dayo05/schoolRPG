using System.Collections;
using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public class FarDistMonster: NormalMonsterBase
    {
        public override float width => 0.5f;
        public override float height => 1;
        public override double MaxHp => 10 * (Global.CurrentLevel + 1);
        protected override float NuckbackDist => MoveDist * 10;
        protected override float MoveDist => Time.deltaTime * 1;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(t());
        }

        IEnumerator t()
        {
            while (true)
            {
                TryShoot(0, lastMoveDirection.I());
                yield return null;
            }
        }
    }
}