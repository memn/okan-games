using System;
using System.Collections.Generic;
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

    public static IEnumerable<Question> InitQuestions(string json)
    {
        var repo = JsonUtility.FromJson<QuestionRepo>(json);
        _repo = repo.Repo.Select(data => new Question(data));
        return _repo;
    }
   
}