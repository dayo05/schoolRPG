using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public class EntityHandler: MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject monster;

        private List<GameObject> monsters = new();

        private void Start()
        {
            SpawnMonster((2, 2));
        }

        public void SpawnMonster((int x, int y) loc)
        {
            var g = Instantiate(monster);
            g.transform.position = EntityBase.GetCurrentWorldPos(loc.x, loc.y);

            if (!g.GetComponent<Monster>().ValidatePos())
            {
                Destroy(g);
                throw new ArgumentOutOfRangeException(nameof(loc), "Not able to spawn monster on that place");
            }
            monsters.Add(g);
        }

        public IEnumerable<GameObject> Entities => monsters.Where(x => !x.IsDestroyed()).Concat(new[]{player});
    }
}