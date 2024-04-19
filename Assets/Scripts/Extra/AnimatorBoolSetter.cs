using UnityEngine;

public class AnimatorBoolSetter : MonoBehaviour
{
    public string parameter;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Destroy(this);
    }

    public void SetParameter(bool on)
    {
        animator.SetBool(parameter, on);
    }
}