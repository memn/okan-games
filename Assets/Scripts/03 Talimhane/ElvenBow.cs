using JetBrains.Annotations;
using UnityEngine;

public class ElvenBow : MonoBehaviour
{
    public TalimhaneManager Manager;
    public GameObject Controls;
    public GameObject ArrowPrefab;
    public GameObject Aim;
    private GameObject _arrow;
    private TalimhaneMusicPlayer _musicPlayer;
    private Animator _animator;

    private bool _gameEnded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _musicPlayer = FindObjectOfType<TalimhaneMusicPlayer>();
    }

    private void Update()
    {
        if (_gameEnded) return;
        if (FindObjectOfType<TalimhaneCameraController>().IsFollowingCam()) return;

        if (Manager.HasArrows()) return;
        _animator.Play("talimhane_out_of_arrows");
        Controls.SetActive(false);
        Manager.EndOfGame();
        _gameEnded = true;
    }


    [UsedImplicitly]
    public void ArrowReady()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.ArrowNock);
    }

    [UsedImplicitly]
    private void ArrowReleased()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.ArrowSwoosh);
        Cancel();
        CreateArrow();
        ShootArrow();
    }

    private void CreateArrow()
    {
        _arrow = Instantiate(ArrowPrefab, Vector3.zero, Quaternion.identity);
        _arrow.name = "arrow";

        _arrow.transform.position = Aim.transform.position;
        _arrow.transform.rotation = Aim.transform.rotation;
        _arrow.transform.parent = Aim.transform.parent;
        _arrow.transform.localScale = Aim.transform.localScale * 5;

        FindObjectOfType<FollowingCamera>().setTarget(_arrow.transform);
    }

    private void ShootArrow()
    {
        _arrow.AddComponent<ElvenArrow>();
        var rigid = _arrow.GetComponent<Rigidbody>();

        rigid.isKinematic = false;
        rigid.velocity = _arrow.transform.forward * 100;

        FindObjectOfType<TalimhaneCameraController>().ToggleCameras("shoot");
    }


    [UsedImplicitly]
    private void StringReleased()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.StringRelease);
    }

    [UsedImplicitly]
    private void StringPulled()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.StringPull);
    }

    [UsedImplicitly]
    public void Pulled()
    {
        _animator.SetBool("pulled", true);
    }

    [UsedImplicitly]
    public void Cancel()
    {
        _animator.SetBool("hazir", false);
        _animator.SetBool("cancel", false);
        _animator.SetBool("release", false);
        _animator.SetBool("pulled", false);
    }

    public void Set()
    {
        if (!_animator.GetBool("hazir"))
            _animator.SetBool("hazir", true);
    }

    public void Release()
    {
        if (_animator.GetBool("hazir")) 
            _animator.SetBool(_animator.GetBool("pulled") ? "release" : "cancel", true);
    }
}