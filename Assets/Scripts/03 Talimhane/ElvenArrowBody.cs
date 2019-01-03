using UnityEngine;

public class ElvenArrowBody : MonoBehaviour
{

    private bool _collided;

    private void Start()
    {
        _collided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collided) return;
        
        // either plane or goal
        if (other.transform.name == "Plane")
        {
            _collided = true;
            transform.parent.GetComponent<ElvenArrow>().Hit(false);
        }

        if (!other.transform.CompareTag("Goal")) return;
        _collided = true;
        transform.parent.transform.parent = other.transform;

        switch (other.name)
        {
            case ("bind_r_bag01"):
                transform.parent.GetComponent<ElvenArrow>().RightHit();
                break;
            case ("bind_l_bag01"):
                transform.parent.GetComponent<ElvenArrow>().LeftHit();
                break;
            case ("bind_perekladina01"):
                transform.parent.GetComponent<ElvenArrow>().Hit(true);
                break;
            case ("bind_targetStraw01"):
                transform.parent.GetComponent<ElvenArrow>().HitStraw();
                break;
            default:
                LogUtil.Error("Unknown target name!" + other.name);
                break;
        }
    }
}