using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public abstract class AtkParticleBase: EntityBase
    {
        public abstract double Atk { get; set; }
        public abstract float DeltaTime { get; set; }
        public int DataValue { get; set; } = -1;

        private bool isFinished = false;

        private Vector3 maintainDist = Vector3.zero;
        private GameObject other;

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

            if (!ValidatePos())
                DestroySelf(null);
            else
            {
                foreach (var component in cam.GetComponent<EntityHandler>().Entities
                             .Select(x => x.GetComponent<UnitBase>()).Where(component => IsCollide(component, this)))
                    switch (component)
                    {
                        case Monster monster:
                            OnMonsterAtk(monster);
                            break;
                        case Player player:
                            OnPlayerAtk(player);
                            break;
                    }

                Move();
            }
        }

        private void DestroySelf(GameObject otherBy)
        {
            other = otherBy;
            if (otherBy is not null)
                maintainDist = transform.position - otherBy.transform.position;
            if(TryGetComponent<ParticleSystem>(out var p))
                p.Play();
            Destroy(gameObject, 2);
            isFinished = true;
        }

        protected abstract void Move();
        protected virtual void OnMonsterAtk(Monster monster)
        {
            monster.Hp -= Atk;
            DestroySelf(monster.gameObject);
        }

        protected virtual void OnPlayerAtk(Player player)
        {
            player.Hp -= Atk;
            DestroySelf(player.gameObject);
        }
    }
}