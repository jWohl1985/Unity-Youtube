using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    private string menuOpenAnimation = "MenuOpen";
    private string menuCloseAnimation = "MenuClose";
    
    public bool IsOpen { get; private set; }
    public bool IsAnimating => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        IsOpen = true;
        animator.Play(menuOpenAnimation);
    }

    public void Close()
    {
        IsOpen = false;
        animator.Play(menuCloseAnimation);
    }
}
