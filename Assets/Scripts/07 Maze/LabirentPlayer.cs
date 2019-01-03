using UnityEngine;

public class LabirentPlayer : MonoBehaviour
{
    public AudioClip HitSound;
    public AudioClip CoinSound;

    public MazeManager Manager;
    private Animator _animator;

    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
            if (_audioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
                _audioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);

        if (!coll.gameObject.tag.Equals("Goal")) return;
        if (_audioSource != null && CoinSound != null)
            _audioSource.PlayOneShot(CoinSound);

        Destroy(coll.gameObject);
        _animator.Play("Collect");
        Manager.CoinCollected();
    }

}