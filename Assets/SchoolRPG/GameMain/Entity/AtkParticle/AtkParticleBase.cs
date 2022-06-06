using System.Linq;
using SchoolRPG.GameMain.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public abstract class AtkParticleBase: EntityBase
    {
        public abstract double Atk { get; }
        public abstract float DeltaTime { get; }
        public int DataValue { get; set; } = -1;

        private bool isFinished = false;

        private Vector3 maintainDist = Vector3.zero;
        private GameObject other;
        private float startTime;
        protected float dt => Time.time - startTime;
        public GameObject super;

        protected Vector3 startPos;

        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            startTime = Time.time;
        }

        protected virtual void Update()
        {
            if (isFinished)
            {
                if (other is null) return;
                if(other.IsDestroyed())
                    Destroy(gameObject);
                else transform.position = other.transform.position + maintainDist;
                return;
            }

            if (!IsCollided())
                Move();
        }

        protected virtual bool IsCollided()
        {
            if (!ValidatePos())
            {
                DestroySelf(null);
                return true;
            }

            foreach (var component in handler.Entities
                         .Select(x => x.GetComponent<UnitBase>()).Where(component => IsCollide(component, this)))
                switch (component)
                {
                    case NormalMonsterBase monster:
                        return OnMonsterAtk(monster);
                    case Player player:
                        return OnPlayerAtk(player);
                    case BossMonster boss:
                        return OnBossMonsterAtk(boss);
                }

            return false;
        }

        protected void DestroySelf(GameObject otherBy, float deleteAfter = 2)
        {
            other = otherBy;
            if (otherBy is not null)
                maintainDist = transform.position - otherBy.transform.position;
            if(TryGetComponent<ParticleSystem>(out var p))
                p.Play();
            Destroy(gameObject, deleteAfter);
            isFinished = true;
        }

        protected abstract void Move();
        protected virtual bool OnMonsterAtk(NormalMonsterBase shortDistMonster)
        {
            shortDistMonster.Hp -= Atk;
            DestroySelf(shortDistMonster.gameObject);
            shortDistMonster.Nuckback((Direction) DataValue);
            return true;
        }

        protected virtual bool OnPlayerAtk(Player player)
        {
            player.Hp -= Atk;
            DestroySelf(player.gameObject);
            return true;
        }

        protected virtual bool OnBossMonsterAtk(BossMonster boss)
        {
            if(!boss.IsIgnoreAtk)
                boss.Hp -= Atk;
            DestroySelf(boss.gameObject);
            return true;
        }
    }
}