using System.Collections;
using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;

using static System.Linq.Enumerable;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class UnitBase: EntityBase
    {
        private List<float> lastShootTime;
        
        protected virtual GameObject TryShoot(int atkIdx, int subData = 0)
        {
            lastShootTime ??= new(Repeat<float>(0, Atk.Count));
            
            if (Time.time < lastShootTime[atkIdx] + Atk[atkIdx].GetComponent<AtkParticleBase>().DeltaTime) return null;
            lastShootTime[atkIdx] = Time.time;
            var g = Instantiate(Atk[atkIdx]);
            g.transform.position = transform.position;
            g.GetComponent<AtkParticleBase>().DataValue = subData;
            g.SetActive(true);
            return g;
        }

        public abstract double Hp { get; set; }
        protected List<GameObject> Atk { get; } = new();
        protected abstract float NuckbackDist { get; set; }

        private IEnumerator _Nuckback(Direction d)
        {
            foreach (var i in Range(0, 5))
            {
                if (ValidatePos(transform.position + NuckbackDist * d.V()))
                    transform.position += NuckbackDist * d.V();
                yield return new WaitForFixedUpdate();
            }
        }
        
        public void Nuckback(Direction d)
            => StartCoroutine(_Nuckback(d));
    }
}