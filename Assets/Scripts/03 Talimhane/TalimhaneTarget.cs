using UnityEngine;

public class TalimhaneTarget : MonoBehaviour
{
    private const float XMin = -12.0f;
    private const float XMax = 12.0f;
    private const float ZMin = -350.0f; // -400-350 = 50 meters far
    private const float ZMax = -310.0f; //  -400-310  = 90 meters far

    public Terrain terrain;
    private TalimhaneManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<TalimhaneManager>();
        PutGround();
        _manager.RecalculateDistance();
    }

    private void PutGround()
    {
        transform.position =
            new Vector3(transform.position.x, HeightAtTerrain(transform.position), transform.position.z);
    }

    private float HeightAtTerrain(Vector3 pos)
    {
        return terrain.SampleHeight(pos);
    }

    public void SetPostion()
    {
        var x = Random.Range(XMin, XMax);
        var z = Random.Range(ZMin, ZMax);
        var y = HeightAtTerrain(new Vector3(x, transform.position.y, z));
        transform.position = new Vector3(x, y, z);
        _manager.RecalculateDistance();
    }
}