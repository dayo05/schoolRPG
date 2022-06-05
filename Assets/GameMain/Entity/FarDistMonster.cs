using System.Collections;

namespace SchoolRPG.GameMain.Utils
{
    public class FarDistMonster: NormalMonsterBase
    {
        public override float width => 0.5f;
        public override float height => 1;
        public override double MaxHp => 20;
        protected override float NuckbackDist => MoveDist * 10;
        protected override float MoveDist => 0.005f;

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