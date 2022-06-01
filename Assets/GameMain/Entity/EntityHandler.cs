using System.Collections.Generic;
using UnityEngine;

namespace SchoolRPG.GameMain
{
    public class EntityHandler: MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject monster;

        public List<GameObject> entities = new();
        
        void Start()
        {
            player = Instantiate(player);
            entities.Add(player);
            map = Instantiate(map);
        }

        void RegisterMonster()
        {
            var g = Instantiate(monster);
            entities.Add(g);
        }
    }
}