using System;
using System.Linq;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public abstract class AtkParticleBase: EntityBase
    {
        public abstract double Atk { get; set; }
        public abstract float DeltaTime { get; set; }
        public int DataValue { get; set; } = -1;

        public GameObject cam;
        private bool isFinished = false;

        protected virtual void Update()
        {
            if (!ValidatePos())
                Destroy(gameObject);
            else
            {
                if (isFinished) return;

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
            }
        }

        protected virtual void OnMonsterAtk(Monster monster)
        {
            monster.Hp -= Atk;
            Destroy(gameObject, 0.1f);
            isFinished = true;
        }

        protected virtual void OnPlayerAtk(Player player)
        {
            player.Hp -= Atk;
            Destroy(gameObject, 0.1f);
            isFinished = true;
        }
    }
}