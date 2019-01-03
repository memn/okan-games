using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{
    private int _remaining;
    private int _collected;
    public Text CollectedText;
    public Text RemainingText;

    public Timer Timer;
    public GameObject PlayerControlButtons;

    public GameObject Congrats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    [UsedImplicitly]
    public void Back()
    {
    }

    internal void CoinCollected()
    {
        _collected++;
        _remaining--;
        CollectedText.text = (_collected * 10).ToString();
        RemainingText.text = _remaining.ToString();
        if (_remaining == 0) EndOfGame(true);
    }

    public void EndOfGame(bool success)
    {
        Timer.Stop();
        PlayerControlButtons.SetActive(false);
        if (success)
        {
            Congrats.GetComponent<CongratsUtil>().ShowSuccess(-1);
            var timerRemaining = Timer.Remaining;
            LogUtil.Log("Success with " + _collected + " collected and remaining time in sec:" + timerRemaining);
//            UserManager.Reward(CommonResources.Building.Abdulmuttalib, (int) (timerRemaining * 4) + _collected * 10);

            Invoke("Back", 5);
        }
        else
        {
            Congrats.GetComponent<CongratsUtil>().ShowFail(-1);
            Invoke("Back", 5);
        }
    }

    internal void SetRemaining(int v)
    {
        _remaining = v;
        RemainingText.text = _remaining.ToString();
    }
}