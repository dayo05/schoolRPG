using System;
using System.Collections;
using System.Collections.Generic;
using SchoolRPG.GameMain.Questions;
using SchoolRPG.GameMain.Utils;
using UnityEngine;
using UnityEngine.UI;
using static System.Linq.Enumerable;
using Random = System.Random;

namespace SchoolRPG.GameMain.Entity
{
    public class BossMonster: UnitBase
    {
        public override float width => 1.8f;
        public override float height => 2.7f;
        public override double MaxHp => 1000;
        protected override float NuckbackDist => 0;
        protected override float MoveDist => 0;

        public GameObject bossAtk1;
        public GameObject bossAtk2;
        public GameObject bossAtk3;

        public GameObject question;

        private Question currentQuestion;
        private bool answerReceived = false;

        private HashSet<int> finishedIndex = new();

        public GameObject RedSquare;

        public bool IsIgnoreAtk => question.activeSelf;

        protected override void Start()
        {
            base.Start();
            Atk.Add(bossAtk1);
            Atk.Add(bossAtk2);
            Atk.Add(bossAtk3);
            
            StartCoroutine(BossCoroutine());
            question.transform.Find("ApplyButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                var ipf = question.transform.Find("Answer").GetComponent<InputField>();
                if (string.IsNullOrWhiteSpace(ipf.text)) return;
                if (currentQuestion.Answer == ipf.text.Trim())
                    Hp -= currentQuestion.Damage;
                answerReceived = true;
            });
        }

        private IEnumerator Pattern1(float time)
        {
            const float delta = 0.5f;
            var bias = 0;
            foreach (var x in Range(0, (int) (time / delta)))
            {
                for (var i = bias; i < 360 + bias; i += 60)
                    TryShoot(0, i % 360);
                bias += 5;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator Pattern4()
        {
            if (finishedIndex.Count == QuestionHandler.questions.Count) yield break;
            var idx = new Random().Next(0, QuestionHandler.questions.Count);
            while (finishedIndex.Contains(idx))
                idx = new Random().Next(0, QuestionHandler.questions.Count);

            currentQuestion = QuestionHandler.questions[idx];
            finishedIndex.Add(idx);
            
            question.SetActive(true);
            question.transform.Find("QuestionText").GetComponent<Text>().text = currentQuestion.Text;
            var startTime = Time.time;
            answerReceived = false;
            var timeLeftBar = question.transform.Find("timeLeft").GetComponent<RectTransform>();
            question.transform.Find("hintText").GetComponent<Text>().text = currentQuestion.HintLimit == 0
                ?
                "이 문제는 힌트가 없는듯하다."
                : Global.Knowledge >= currentQuestion.HintLimit
                    ? currentQuestion.Hint
                    : "힌트를 얻기에는 능지가 부족한 것 같다.";
            
            while (true)
            {
                timeLeftBar.localScale = new Vector3(1 - (Time.time - startTime) / currentQuestion.TimeLimit, 1, 1);
                if (Time.time - startTime >= currentQuestion.TimeLimit || answerReceived)
                    break;
                yield return null;
            }
            question.SetActive(false);
        }

        private IEnumerator Pattern2()
        {
            transform.position = new Vector3(15.5f, 8.5f, 0);
            var playerX = GetCurrentMapPos(handler.player.transform.position).x;
            RedSquare.SetActive(true);
            RedSquare.transform.position = new Vector3(playerX, 0, 0);
            RedSquare.transform.localScale = new Vector3(2, 50, 0);
            
            yield return new WaitForSeconds(2);
            TryShoot(1, playerX);
            RedSquare.SetActive(false);
            gameObject.transform.position = new Vector3(100, 100);
        }

        private IEnumerator Pattern3()
        {
            transform.position = new Vector3(15.5f, 8.5f, 0);
            var playerY = GetCurrentMapPos(handler.player.transform.position).y;
            RedSquare.SetActive(true);
            RedSquare.transform.position = new Vector3(0, playerY, 0);
            RedSquare.transform.localScale = new Vector3(100, 3, 0);
            
            yield return new WaitForSeconds(2);
            TryShoot(2, playerY);
            RedSquare.SetActive(false);
            gameObject.transform.position = new Vector3(100, 100);
        }

        private IEnumerator BossCoroutine()
        {
            yield return new WaitForSeconds(3);
            while (true)
            {
                yield return new Random().NextDouble() switch
                {
                    < 0.3 => Pattern1(new Random().Next(5, 25)),
                    < 0.4 => Pattern4(),
                    < 0.7 => Pattern2(),
                    _ => Pattern3()
                };
                yield return new WaitForSeconds(5);
            }
        }
    }
}