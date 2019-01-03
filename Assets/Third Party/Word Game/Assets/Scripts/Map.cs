using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    private const string ImgDir = "Assets/Third Party/Word Game/Assets/Resources/Letters/";
    private const string Empty = ImgDir + "Empty.png";

    public GameObject hexPrefab;

    int width = 5, height = 5;

    private const string Word2 = "Kainatboşlukkabuletmezabii";

    private void Start()
    {
        CreatePuzzle(Word2);
    }

    private void CreatePuzzle(string word)
    {
        var puzzle = PuzzleMaker.MakePuzzle(word);
        var startY = -1 * puzzle.height / 2;
        var startX = -1 * puzzle.width / 2;
        var start = new Vector2(startX, y: startY);
        for (var x = 0; x < puzzle.width; x++)
        {
            for (var y = 0; y < puzzle.height; y++)
            {
                var fy = start.y + y;
                var fx = start.x + x;

                if (y % 2 == 0)
                {
                    fx += 0.5f;
                }

                var pos = new Vector2(fx, fy);
                var img2 = ImgDir + puzzle.puzzleData[x, y] + ".png";
                var go = Instantiate(hexPrefab, pos, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = LoadNewSprite(img2);
                go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    private static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 100.0f)
    {
        // Load a PNG or JPG image from disk to a Texture2D,
        // assign this texture to a new sprite and return its reference
        var spriteTexture = LoadTexture(filePath);
        return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height),
            new Vector2(0, 0), pixelsPerUnit);
    }

    private static Texture2D LoadTexture(string filePath)
    {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails
        var fileData = File.ReadAllBytes(File.Exists(filePath) ? filePath : Empty);
        var tex2D = new Texture2D(2, 2);
        return tex2D.LoadImage(fileData) ? tex2D : null;
    }
}