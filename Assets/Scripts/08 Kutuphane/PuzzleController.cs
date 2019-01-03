using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private const int MinWordLength = 2;
    [SerializeField] private Camera _camera;

    public GameObject AnswerObject;
    public Transform AnswerParentTransform;
    public KutuphaneMap Map;

    private Stack<PuzzleObject> _selectedPuzzleObjects;
    private string _answer;

    private AudioSource _source;
    public AudioClip SuccessClip;
    public AudioClip FailClip;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _answer = "";
        _selectedPuzzleObjects = new Stack<PuzzleObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ControlState();
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (hitInfo)
        {
            HandleTouchOn(hitInfo.transform.gameObject);
            // Here you can check hitInfo to see which collider has been hit, and act appropriately.
        }
    }

    private void ControlState()
    {
        if (_answer.Length < MinWordLength) ClearSelectedLetters();
        else
        {
            if (Map.CheckAnswer(_answer)) SuccessCase();
            else FailCase();
        }

        ClearAnswerLine();
    }

    private void ClearAnswerLine()
    {
        _answer = "";
        AnswerParentTransform.position += Vector3.right * 0.4f * AnswerParentTransform.childCount;
        Util.ClearChildren(AnswerParentTransform);
    }

    private void FailCase()
    {
        _source.PlayOneShot(FailClip);
        foreach (var puzzleObject in _selectedPuzzleObjects)
        {
            puzzleObject.Wrong();
        }

        Invoke("ClearSelectedLetters", 0.5f);
    }

    private void SuccessCase()
    {
        _source.PlayOneShot(SuccessClip);
        while (_selectedPuzzleObjects.Count > 0)
        {
            var puzzleObject = _selectedPuzzleObjects.Pop();
            puzzleObject.Correctify();
            puzzleObject.Explode();
        }

        Invoke("AnyWordsLeft", 0.8f);
        ClearHintLine();   
    }
    private void ClearHintLine()
    {
        HintTransform.position += Vector3.right * 0.4f * HintTransform.childCount;
        Util.ClearChildren(HintTransform);
    }

    private void ClearSelectedLetters()
    {
        while (_selectedPuzzleObjects.Count > 0)
        {
            _selectedPuzzleObjects.Pop().Unselect();
        }
    }

    private void AnyWordsLeft()
    {
        if (!Map.AnyWordsLeft())
        {
            Map.Done();
        }
    }

    private void HandleTouchOn(GameObject transformGameObject)
    {
        var puzzleComponent = transformGameObject.GetComponent<PuzzleObject>();
        if (!puzzleComponent) return;
        if (_selectedPuzzleObjects.Count == 0)
        {
            Push(puzzleComponent);
        }
        else if (puzzleComponent == _selectedPuzzleObjects.Peek()) return; // holding last
        else
        {
            if (puzzleComponent == _selectedPuzzleObjects.Peek()) return;
            if (!puzzleComponent.Selected())
            {
                Push(puzzleComponent);
            }
            else
            {
                // is it before last selected item.
                var last = _selectedPuzzleObjects.Pop();
                if (_selectedPuzzleObjects.Peek() == puzzleComponent)
                {
                    last.Unselect();
                    Pop();
                }
                else
                {
                    _selectedPuzzleObjects.Push(last);
                }
            }
        }
    }

    private void Pop()
    {
        Destroy(AnswerParentTransform.GetChild(AnswerParentTransform.childCount - 1).gameObject);
        AnswerParentTransform.position += Vector3.right * 0.4f;
        _answer = _answer.Remove(_answer.Length - 1);
    }

    private void Push(PuzzleObject puzzleObject)
    {
        puzzleObject.Select();
        _selectedPuzzleObjects.Push(puzzleObject);
        var c = puzzleObject.Letter;
        _answer += c;

        var go = Instantiate(AnswerObject, Vector3.zero, Quaternion.identity);
        go.GetComponent<PuzzleObject>().SetCharacter(c);
        go.transform.parent = AnswerParentTransform;
        go.transform.localPosition = Vector2.right * 0.8f * _selectedPuzzleObjects.Count;
        go.transform.localScale = Vector3.one;
        AnswerParentTransform.position += Vector3.left * 0.4f;
    }

    public Transform HintTransform;

    public void Hint(char c)
    {
        var go = Instantiate(AnswerObject, Vector3.zero, Quaternion.identity);
        go.GetComponent<PuzzleObject>().SetCharacter(c);
        go.transform.parent = HintTransform;
        go.transform.localPosition = Vector2.right * 0.8f * HintTransform.childCount;
        go.transform.localScale = Vector3.one;
        HintTransform.position += Vector3.left * 0.4f;
    }
}