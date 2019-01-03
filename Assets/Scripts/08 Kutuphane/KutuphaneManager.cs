using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class KutuphaneManager : MonoBehaviour
{
    public Text topicHead;
    public Text Scoreboard;

    private KutuphaneMap _map;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;

    private Dictionary<string, string>.Enumerator _enumerator;

    private Dictionary<string, string> _dict;

    private float _startTime;
    private float _spentTime;

    private void Start()
    {
        _map = GetComponent<KutuphaneMap>();
//        _dict = WordUtil.FindDictionaryByLevel(UserManager.Game.Level);
        _enumerator = _dict.GetEnumerator();
        UpdateScore(0);
        StartGame();
    }

    private void StartGame()
    {
        LoadNextTopic();
        StartPuzzle();
        _startTime = Time.time;
    }


    private void Update()
    {
        // idle movements can be done here?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    [UsedImplicitly]
    public void Back()
    {
    }

    private void LoadNextTopic()
    {
        _spentTime += Time.time - _startTime;
        _startTime = Time.time;

        if (!_enumerator.MoveNext())
        {
            var score = int.Parse(Scoreboard.text);
            LogUtil.Log("Kelimelik success " + score + " remaining time in sec:" + _spentTime);
//            UserManager.Reward(CommonResources.Building.DarulErkam, (int) (score - _spentTime / 10));
            EndOfLevel();
            return;
        }

        var current = _enumerator.Current;
        topicHead.text = string.Join("\n", current.Key.Split());
        _map.LoadPuzzle(current.Value);
    }

    [UsedImplicitly]
    public void StartPuzzle()
    {
        if (!_winScreen.activeSelf) return;
        _puzzleScreen.SetActive(true);
        _winScreen.GetComponent<CongratsUtil>().Deactivate();
        _map.StartPuzzle();

    }

    private bool started;

    public void Congrats()
    {
        _winScreen.GetComponent<CongratsUtil>().ShowSuccess(-1);
        _puzzleScreen.SetActive(false);
        LoadNextTopic();
        Invoke("StartPuzzle", 3.5f);
    }

    private void EndOfLevel()
    {
        Invoke("Back", 2);
    }

    public void UpdateScore(int score)
    {
        Scoreboard.text = score.ToString();
    }
}