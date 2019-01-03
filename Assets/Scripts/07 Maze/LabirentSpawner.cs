using UnityEngine;

public class LabirentSpawner : MonoBehaviour
{
    public MazeManager Manager;

    public GameObject Floor;
    public GameObject[] WallFabs;
    public GameObject[] GoalPrefabs;
    public GameObject Pillar;


    private const int Rows = 7;
    private const int Columns = 7;
    private const float CellWidth = 4;
    private const float CellHeight = 4;


    private void Start()
    {
        var front = transform.Find("Front");
        var back = transform.Find("Back");
        var left = transform.Find("Left");
        var right = transform.Find("Right");
        var goals = 0;

        var mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
        mMazeGenerator.GenerateMaze();

        for (var row = 0; row < Rows; row++)
        {
            for (var column = 0; column < Columns; column++)
            {
                var x = column * CellWidth;
                var z = row * CellHeight;
                var cell = mMazeGenerator.GetMazeCell(row, column);
                var tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                tmp.transform.parent = transform;
                var wall = WallFabs[Random.Range(0, WallFabs.Length)];
                if (cell.WallRight)
                {
                    tmp = Instantiate(wall, new Vector3(x + CellWidth / 2, 0, z), Quaternion.Euler(0, 90, 0));
                    tmp.transform.parent = right;
                }

                if (cell.WallFront)
                {
                    tmp = Instantiate(wall, new Vector3(x, 0, z + CellHeight / 2), Quaternion.Euler(0, 0, 0));
                    tmp.transform.parent = front;
                }

                if (cell.WallLeft)
                {
                    tmp = Instantiate(wall, new Vector3(x - CellWidth / 2, 0, z), Quaternion.Euler(0, 270, 0));
                    tmp.transform.parent = left;
                }

                if (cell.WallBack)
                {
                    tmp = Instantiate(wall, new Vector3(x, 0, z - CellHeight / 2), Quaternion.Euler(0, 180, 0));
                    tmp.transform.parent = back;
                }

                if (!cell.IsGoal || GoalPrefabs == null || GoalPrefabs.Length < 1) continue;
                var goalPrefab = GoalPrefabs[Random.Range(0, GoalPrefabs.Length)];
                tmp = Instantiate(goalPrefab, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                tmp.transform.parent = transform;
                goals++;
            }
        }

        if (Pillar != null)
        {
            for (var row = 0; row < Rows + 1; row++)
            {
                for (var column = 0; column < Columns + 1; column++)
                {
                    var x = column * CellWidth;
                    var z = row * CellHeight;
                    var position = new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2);
                    var tmp = Instantiate(Pillar, position, Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }

        Manager.SetRemaining(goals);
    }
}