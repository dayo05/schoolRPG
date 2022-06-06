using System;
using System.Collections.Generic;
using System.Linq;
using SchoolRPG.GameMain.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SchoolRPG.GameMain.Entity
{
    public class EntityHandler: MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject bossMap;
        public GameObject shortAtkMonster;
        public GameObject farAtkMonster;
        public GameObject boss;
        public GameObject statsWindow;
        public static bool IsInversedMap => Global.CurrentLevel % 2 == 1 && !Global.IsBossFightMap;

        private List<GameObject> monsters = new();

        private void Start()
        {
            if (Global.IsBossFightMap)
            {
                player.transform.position = EntityBase.GetCurrentWorldPos(16, 2);
                bossMap.transform.localScale = new Vector3(1, 1, 1);
                bossMap.transform.position = new Vector3(15.5f, 8.5f, 0);
                bossMap.SetActive(true);
                map.SetActive(false);
                transform.position = new Vector3(15.5f, 8.5f, -10);
                boss.transform.position = new Vector3(15.5f, 8.5f, 0);
                boss.SetActive(true);

                GetComponent<Camera>().orthographicSize = 9;
            }
            else
            {
                map.transform.localScale = IsInversedMap ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
                map.transform.position = IsInversedMap ? new Vector3(15.5f, -8.5f, 0) : new Vector3(15.5f, 8.5f, 0);
                bossMap.SetActive(false);
                map.SetActive(true);
                player.transform.position = Global.IsMovingNext
                    ? EntityBase.GetCurrentWorldPos(2, 2)
                    : EntityBase.GetCurrentWorldPos(26, 2);

                GetComponent<Camera>().orthographicSize = 4.5f;
                SpawnMonster((25, 9));
                SpawnMonster((25, 10), true);
            }
        }

        private void Update()
        {
            if(!Global.IsBossFightMap)
                transform.position = player.transform.position + new Vector3(0, 0, -10);

            if (Input.GetKey(KeyCode.F))
            {
                statsWindow.transform.GetChild(1).GetComponent<Text>().text =
                    $"체력: {player.GetComponent<Player>().Hp} / {player.GetComponent<Player>().MaxHp}";
                statsWindow.transform.GetChild(2).GetComponent<Text>().text =
                    $"원거리 공격력: {5 * Global.PlayerArrowAtk}";
                statsWindow.transform.GetChild(3).GetComponent<Text>().text =
                    $"근거리 공격력: {10 * Global.PlayerChairAtk}";
                statsWindow.transform.GetChild(4).GetComponent<Text>().text =
                    $"지성: {Global.Knowledge}";
                statsWindow.transform.GetChild(5).GetComponent<Text>().text =
                    $"현재 맵: {(Global.IsBossFightMap ? "보스맵" : Global.CurrentLevel + 1)}";
                statsWindow.SetActive(true);
            }
            else statsWindow.SetActive(false);
        }

        public void SpawnMonster((int x, int y) loc, bool isFarAtk = false)
        {
            var g = Instantiate(isFarAtk ? farAtkMonster : shortAtkMonster);
            g.transform.position = EntityBase.GetCurrentWorldPos(loc.x, loc.y);

            if (!g.GetComponent<EntityBase>().ValidatePos())
            {
                Destroy(g);
                throw new ArgumentOutOfRangeException(nameof(loc), "Not able to spawn monster on that place");
            }
            g.SetActive(true);
            monsters.Add(g);
        }

        public IEnumerable<GameObject> Entities => monsters.Concat(new[]{player, boss}).Where(x => x is not null && !x.IsDestroyed());
    }
}