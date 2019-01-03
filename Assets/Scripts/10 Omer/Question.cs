public class Question
{
    private readonly QuestionRepoHandler.QuestionData _data;

    public Question(QuestionRepoHandler.QuestionData data)
    {
        _data = data;
    }

    public int Choice = -1;

    public bool Answered
    {
        get { return Choice != -1; }
    }

    public string ChoiceText
    {
        get { return Answered ? Choices[Choice] : ""; }
    }

    public int Answer
    {
        get { return _data.answer; }
    }

    public string Text
    {
        get { return _data.text; }
    }

    public string[] Choices
    {
        get { return _data.choices; }
    }

    public string Topic
    {
        get { return _data.topic; }
    }

    public string AnswerText
    {
        get { return Choices[Answer]; }
    }

    public void UpdateChoice(string choice)
    {
        for (var i = 0; i < Choices.Length; i++)
        {
            if (choice != Choices[i]) continue;
            Choice = i;
            break;
        }
    }
}