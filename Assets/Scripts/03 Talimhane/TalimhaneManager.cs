using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalimhaneManager : MonoBehaviour
{
    [SerializeField] private Text _arrows;
    [SerializeField] private Text _score;
    [SerializeField] private Text _distance;

    public int Arrows = 10;
    private int _points;

    [SerializeField] private GameObject _player;

    [SerializeField] private Animator _targetAnimator;
    [SerializeField] private Animator _strawAnimator;

    public CongratsUtil Congrats;

    private TalimhaneMusicPlayer _musicPlayer;

    private void Start()
    {
        _musicPlayer = FindObjectOfType<TalimhaneMusicPlayer>();
        _arrows.text = Arrows.ToString();
        _score.text = _points.ToString();
        RecalculateDistance();
    }

    public void RecalculateDistance()
    {
        _distance.text = CalculateDistance();
    }

    private string CalculateDistance()
    {
        var targeTransformPos = FindObjectOfType<TalimhaneTarget>().transform.position;
        var distance = Vector3.Distance(_player.transform.position, targeTransformPos);
        return distance.ToString("F2") + "m";
    }

    [UsedImplicitly]
    public void Back()
    {
        SceneManager.LoadScene("08 EbuTalib");
    }

    public bool HasArrows()
    {
        return Arrows > 0;
    }

    public void Hit()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.ArrowImpact);
        Arrows--;
        _arrows.text = Arrows.ToString();
        _points += 10;
        _score.text = _points.ToString();
        Invoke("Congrat", 1);
    }

    private void Congrat()
    {
        Congrats.ShowSuccess(2.0f);
    }

    public void NotHit()
    {
        Arrows--;
        _arrows.text = Arrows.ToString();
        _points += 0;
        _score.text = _points.ToString();
        Congrats.ShowFail(2.0f);
    }

    public void SetTrigger(string triggerName)
    {
        if (triggerName == "hit")
            _strawAnimator.SetTrigger(triggerName);
        else
            _targetAnimator.SetTrigger(triggerName);
    }

    public void MoveTarget(bool straw)
    {
        var hitTargetAnimator = straw ? _strawAnimator : _targetAnimator;
        hitTargetAnimator.transform.parent.GetComponent<TalimhaneTarget>().SetPostion();
    }

    public void EndOfGame()
    {
        var score = _points / 10;
        var success = !(score < 6);
        if (success)
        {
//            UserManager.Reward(CommonResources.Building.EbuTalib, score * 50);
//            if (score == 10)
//                UserManager.Instance.UnlockAchievement(CommonResources.Extras(CommonResources.Building.EbuTalib), 250);
        }
        
        Invoke("Back", 2);
    }
}