using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnZebra : AAnimalBase
{
    public override void AgentMove()
    {
        base.AgentMove();
        StartCoroutine(DelayAgentMove(EatEnd, "End Eating"));
        
    }

    public override void ShowAnimalAction()
    {
        base.ShowAnimalAction();
        Eat();
    }

    public override void Idle()
    {
        base.Idle();
        animator.SetBool("isEatEnd",false);
    }
    public void Eat()
    {
        Idle();
        animator.SetBool("isEat", true);
    }

    public void EatEnd()
    {
        animator.SetBool("isEatEnd", true);
        animator.SetBool("isEat", false);
    }
}
