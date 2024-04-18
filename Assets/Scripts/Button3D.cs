using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour
{
    public UnityEvent ClickDown = new UnityEvent();
    public UnityEvent ClickHold = new UnityEvent();
    public UnityEvent ClickUp = new UnityEvent();

    private void OnMouseDown()
    {
        ClickDown.Invoke();

    }

    private void OnMouseDrag()
    {
        ClickHold.Invoke();
    }

    private void OnMouseUp()
    {
        ClickUp.Invoke();
    }
}
