using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnLion : AAnimalBase
{
    public override void AgentMove()
    {
        base.AgentMove();
        StartCoroutine(DelayAgentMove(ResetAnimalState, "End Rest"));
    }

    public override void ShowAnimalAction()
    {
        base.ShowAnimalAction();
        Rest();
    }
    public void Rest()
    {
        Idle();
        animator.SetBool("isRest", true);
    }

    public void ResetAnimalState()
    {
        animator.SetBool("isRest",false);
    }
}
