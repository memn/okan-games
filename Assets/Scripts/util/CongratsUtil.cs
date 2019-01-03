using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CongratsUtil : MonoBehaviour
{
    public Sprite[] Success;
    public Sprite[] Fail;
    public AudioClip SuccessClip;
    public AudioClip FailClip;
    public GameObject Back;
    private UnityAction _action;

    private static Sprite RandomR(IList<Sprite> sprites)
    {
        var r = Random.Range(0, sprites.Count);
        return sprites[r];
    }

    public void ShowSuccess(float closeAfter, UnityAction action =null)
    {
        _action = action;
        Show(closeAfter, Success);
        GetComponent<AudioSource>().PlayOneShot(SuccessClip);
    }

    public void ShowFail(float closeAfter, UnityAction action =null)
    {
        _action = action;
        Show(closeAfter, Fail);
        GetComponent<AudioSource>().PlayOneShot(FailClip);
    }

    private void Show(float closeAfter, IList<Sprite> success)
    {
        Back.SetActive(true);
        gameObject.SetActive(true);
        GetComponent<Image>().sprite = RandomR(success);
        if (closeAfter > 0) Invoke("CloseAfter", closeAfter);
        
    }

    private void CloseAfter()
    {
        Back.SetActive(false);
        gameObject.SetActive(false);
        if (_action != null)
            _action();
    }

    public void Deactivate()
    {
        CloseAfter();
    }
}