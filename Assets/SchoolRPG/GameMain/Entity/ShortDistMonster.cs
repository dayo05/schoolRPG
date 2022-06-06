using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public class ShortDistMonster: NormalMonsterBase
    {
        protected override GameObject TryShoot(int atkIdx, int subData = 0)
        {
            var t = base.TryShoot(atkIdx, subData);
            if(t is not null)
                gameObject.SetActive(false);
            return t;
        }

        public override double MaxHp => (Global.CurrentLevel + 1) * 20;

        protected override float NuckbackDist => MoveDist * 10;
        protected override float MoveDist => Time.deltaTime * 2;
        
        public override float width => 0.9f;
        public override float height => 0.9f;
    }
}