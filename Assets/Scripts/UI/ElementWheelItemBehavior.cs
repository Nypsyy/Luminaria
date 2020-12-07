using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementWheelItemBehavior : MonoBehaviour
{
    public Image background;
    public Vector3 hoverScale;
    public Color hoverColor;
    public Color baseColor;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        background.color = baseColor;
    }

    public void Select()
    {
        animator.SetBool("Select", true);
    }

    public void Deselect()
    {
        animator.SetBool("Select", false);
    }

    public void ResetUI()
    {
        animator.SetBool("Select", false);
        transform.localScale = Vector3.one;
        background.color = baseColor;
    }
}
