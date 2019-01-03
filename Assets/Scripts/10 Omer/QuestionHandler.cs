using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public Text Topic;
    public Text QuestionText;
    public GameObject ChoicesParent;
    public GameObject ChoicePrefab;
    internal QuestionManager Manager;

    private Question _theQuestion;


    public void ShowQuestion(Question questionData)
    {
        _theQuestion = questionData;
        QuestionText.text = questionData.Text;
        Topic.text = questionData.Topic;
        Util.ClearChildren(ChoicesParent.transform);
        Util.Load(ChoicesParent, ChoicePrefab, questionData.Choices, (choice, choiceText) =>
        {
            choice.GetComponentInChildren<Text>().text = choiceText;
            var button = choice.GetComponent<Button>();
            if (!questionData.Answered)
            {
                button.onClick.AddListener(delegate { AnswerHandler(choiceText); });
            }
            else
            {
                button.interactable = false;
                if (_theQuestion.ChoiceText == choiceText)
                {
                    button.GetComponent<Image>().color = Color.red;
                }

                if (_theQuestion.AnswerText == choiceText)
                {
                    button.GetComponent<Image>().color = Color.green;
                }
            }
        });
    }

    private void AnswerHandler(string answer)
    {
        _theQuestion.UpdateChoice(answer);
        PaintQuestion();
        Manager.Answer(_theQuestion.AnswerText == answer);
    }

    private void PaintQuestion()
    {
        for (var i = 0; i < _theQuestion.Choices.Length; i++)
        {
            var child = ChoicesParent.transform.GetChild(i);
            if (i == _theQuestion.Choice)
                child.GetComponent<Image>().color = Color.red;

            if (i == _theQuestion.Answer)
                child.GetComponent<Image>().color = Color.green;

            child.GetComponent<Button>().interactable = false;
        }
    }
}