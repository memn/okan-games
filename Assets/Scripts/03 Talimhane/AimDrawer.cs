using UnityEngine;

public class AimDrawer : MonoBehaviour
{
    public void AdjustAccordingToDistance(float distance)
    {
        var factor = Mathf.Clamp(distance / 50.0f, 1f, 1.5f);
        transform.localPosition *= factor;
        transform.localScale *= factor;
    }
}