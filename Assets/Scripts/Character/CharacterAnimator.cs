using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator
{
    private Character character;
    private Animator animator;

    public string WalkingParameter => "isWalking";
    public string HorizontalParameter => "xDir";
    public string VerticalParameter => "yDir";

    public CharacterAnimator(Character character)
    {
        this.character = character;
        this.animator = character.GetComponent<Animator>();
    }

    public void ChooseLayer()
    {
        bool isWalking = character.IsMoving;
        animator.SetBool(WalkingParameter, isWalking);
    }

    public void UpdateParameters()
    {
        animator.SetFloat(HorizontalParameter, character.Facing.x);
        animator.SetFloat(VerticalParameter, character.Facing.y);
    }
}
