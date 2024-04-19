using UnityEngine;
using TMPro;

public class Tape : MonoBehaviour
{
    public TextMeshPro label;
    public TapeScriptableObject data;
    public bool canBeSocketed;

    private void Awake()
    {
        if (!gameObject.CompareTag("Draggable"))
            Debug.Log("Tape " + gameObject.name + " is not tagged as Draggable.");
    }

    private void Start()
    {
        label.text = data.name;
    }

    public int GetSongStartingPoint()
    {
        return data.startingMillisecond;
    }

    private void OnMouseDown()
    {
        canBeSocketed = false;
    }

    private void OnMouseUp()
    {
        canBeSocketed = true;
    }

}
