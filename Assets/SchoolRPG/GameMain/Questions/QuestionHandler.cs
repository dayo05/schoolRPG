using System.Collections.Generic;

namespace SchoolRPG.GameMain.Questions
{
    public static class QuestionHandler
    {
        public static List<Question> questions = new List<Question>
        {
            new Question
            {
                Text =
                    @"서로 다른 형질을 발현하는 두 대립 유전자 쌍 Aa, Bb에 대해 A는 a에 대해 우성, B는 b에 대해 우성이다. 어떤 핵상이 2n=6인 생물의 1번 염색체에서의 A와 B 유전자가 상인 연관이고, 두 유전자 간 거리가 10cM일때, 이 생물을 검정교배 했을 때 나오는 표현형의 정수비[AB]:[Ab]:[aB]:[ab]는 어떻게 되는가? 숫자만 입력할 것. (돌연변이 X, 교차는 한 번만.) (제한 시간 3분)",
                Answer = "9119",
                TimeLimit = 180,
                Damage = 50,
                Hint = "( hint. 교차율 = 1/(n+1) )",
                HintLimit = 20
            },
            new Question
            {
                Text = @"혈액형이 A형인 남자와 혈액형이 B형인 여자가 결혼해 자손을 낳았는데, 자손들의 혈액형이 ⓐ형, 또는 ⓑ형만 나왔다고 한다. (단, 남자와 여자 둘 중 한 명은 동형접합자이다.)
여자의 유전자형과 ⓐ, ⓑ에 들어가는 A, B, O의 문자 갯수의 합을 적어라.
(제한시간 3분)",
                Answer = "5",
                TimeLimit = 180,
                Damage = 50,
                Hint = "( hint. 빠르게 case를 나누어 볼 것 )",
                HintLimit = 10
            },
            new Question
            {
                Text = @"식물의 엽록체에서 붉은 계열의 색소로 자리잡고 있는 물질의 이름은 무엇인가?
①엽록소 a ②크산토필 ③피브리노겐 ④엽록소 b ⑤카로티노이드",
                Answer = "5",
                TimeLimit = 15,
                Damage = 20
            },
            new Question {
                Text = @"다음 면역글로불린 중 이량체는 무엇인가?
①IgA ②IgD ③Ig E ④Ig G ⑤Ig M",
                Answer = "1",
                TimeLimit = 15,
                Damage = 20
            }
        };
    }
}