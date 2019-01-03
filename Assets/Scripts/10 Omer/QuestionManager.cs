﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject QuestionParent;


    public Button PrevButton;
    public Button NextButton;


    private int _enumerator;
    private List<Question> _questions;
    private int _answeredCount;
    private int _score;

    private float _start;
    public CongratsUtil Congrats;
    private QuestionHandler _theHandler;


    private void Awake()
    {
        _questions = QuestionRepoHandler.Questions().ToList();
    }

    [UsedImplicitly]
    public void Init()
    {
        _enumerator = 0;
        _answeredCount = 0;
        CreateAllQuestions();
        UpdateCurrent();
        _start = Time.time;
    }

    private void CreateAllQuestions()
    {
        var i = 0;
        foreach (Transform child in QuestionParent.transform)
        {
            var handler = child.GetComponent<QuestionHandler>();
            handler.Manager = this;
            _theHandler = handler;
        }
        _theHandler.gameObject.SetActive(true);
    }


    private void UpdateCurrent()
    {
        _theHandler.ShowQuestion(_questions[_enumerator]);
        PrevButton.interactable = _enumerator > 0;
        NextButton.interactable = _enumerator < _questions.Count - 1;
    }

    [UsedImplicitly]
    public void Next()
    {
        _enumerator++;
        UpdateCurrent();
    }

    [UsedImplicitly]
    public void Prev()
    {
        _enumerator--;
        UpdateCurrent();
    }

    public void Answer(bool correct)
    {
        if (correct)
        {
            _score++;
            Congrats.ShowSuccess(2);
        }
        else
            Congrats.ShowFail(2);

        if (++_answeredCount != _questions.Count)
        {
            Invoke("Next", 3);
            return;
        }

        EndOfGame(_score >= 6);
    }

    private void EndOfGame(bool success)
    {
        if (success)
        {
        }

        Invoke("Back", 3);
    }
}