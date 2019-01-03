using UnityEngine;

public class TalimhaneCameraController : MonoBehaviour
{
    public Camera fpc;
    public Camera following;

    // Use this for initialization
    private void Start()
    {
        fpc.enabled = true;
        following.enabled = false;
    }

    public void ToggleCameras(string source)
    {
//        print("Toggled by " + source);
        fpc.enabled = !fpc.enabled;
        following.enabled = !following.enabled;
    }

    public bool IsFpc()
    {
        return fpc.enabled;
    }

    public bool IsFollowingCam()
    {
        return following.enabled;
    }
}