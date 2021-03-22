using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnElephant : AAnimalBase
{
    public override void AgentMove()
    {
        base.AgentMove();
        StartCoroutine(DelayAgentMove(ResetAnimalState, "Eating"));
    }

    public void ResetAnimalState()
    {
        animator.SetBool("isEat", false);
    }
    public void Eat()
    {
        Idle();
        animator.SetBool("isEat", true);
    }


    public override void ShowAnimalAction()
    {
        base.ShowAnimalAction();
        Eat();
    }
}
