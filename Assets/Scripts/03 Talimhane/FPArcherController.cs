using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPArcherController : MonoBehaviour
{
    private bool _mIsWalking = false;
    [SerializeField] [Range(0f, 1f)] private float _runstepLenghten;
    private float _gravityMultiplier = 2;
    [SerializeField] private FOVKick _fovKick = new FOVKick();
    [SerializeField] private CurveControlledBob _headBob = new CurveControlledBob();
    [SerializeField] private float _stepInterval;

    [SerializeField] private AudioClip _breathSound; // the sound played when character leaves the ground.
    [SerializeField] private AudioClip _landSound; // the sound played when character touches back on ground.

    private Camera _camera;
    private bool _hold;
    private float _yRotation;
    private Vector2 _input;
    private CharacterController _characterController;
    private bool _previouslyGrounded;
    private AudioSource _audioSource;
    private ArcherMouseLook _mouseLook;
    private ElvenBow _elvenBow;

    // Use this for initialization
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _fovKick.Setup(_camera);
        _headBob.Setup(_camera, _stepInterval);
        _audioSource = GetComponent<AudioSource>();
        _mouseLook = GetComponent<ArcherMouseLook>();
        _mouseLook.Init(transform, _camera.transform);
        _elvenBow = FindObjectOfType<ElvenBow>();
    }


    // Update is called once per frame
    private void Update()
    {
        RotateView();
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            _mouseLook.Holding = true;
            if (!_hold)
            {
                _elvenBow.Set();
                _hold = true;
                _audioSource.loop = true;
                PlayHoldSound();
            }
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            _mouseLook.Holding = false;
            if (_hold)
            {
                _elvenBow.Release();
                _audioSource.loop = false;
                _audioSource.Stop();
                _hold = false;
            }
        }


        if (!_previouslyGrounded && _characterController.isGrounded)
        {
            PlayLandingSound();
        }

        _previouslyGrounded = _characterController.isGrounded;
    }


    private void PlayLandingSound()
    {
        _audioSource.clip = _landSound;
        _audioSource.Play();
    }


    private void FixedUpdate()
    {
        _mouseLook.UpdateCursorLock();
    }


    private void PlayHoldSound()
    {
        _audioSource.clip = _breathSound;
        _audioSource.Play();
    }

    private void RotateView()
    {
        _mouseLook.LookRotation(transform, _camera.transform);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it

        if (body == null || body.isKinematic)
        {
            return;
        }

        body.AddForceAtPosition(_characterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}