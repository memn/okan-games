using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestionRepoHandler
{
    [Serializable]
    private class QuestionRepo
    {
        public QuestionData[] Repo;
    }

    [Serializable]
    public class QuestionData
    {
        public string topic;
        public string text;
        public string[] choices;
        public int answer;
    }

    private static IEnumerable<Question> _repo;

    private const string QuestionsFileName = "questions.json";

    public static IEnumerable<Question> Questions()
    {
        if (_repo == null)
            _repo = LoadQuestions();

        return _repo ?? new List<Question>();
    }

    private static IEnumerable<Question> LoadQuestions()
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, QuestionsFileName);

        if (File.Exists(filePath))
        {
            var dataAsJson = File.ReadAllText(filePath);
            var repo = JsonUtility.FromJson<QuestionRepo>(dataAsJson);
            return repo.Repo.Select(data => new Question(data));
        }
        else
        {
            Debug.LogError("Cannot find Questions repository file!");
        }

        return null;
    }
}