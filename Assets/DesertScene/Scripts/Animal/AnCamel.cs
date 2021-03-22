using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnCamel : AAnimalBase
{
    public override void AgentMove()
    {
        base.AgentMove();
        StartCoroutine(DelayAgentMove(StundUp, "Stund Up"));
    }
    
    public override void ShowAnimalAction()
    {
        base.ShowAnimalAction();
        LieDown();
        isCloseInfo = false;
    }
    public override void Idle()
    {
        base.Idle();
        animator.SetBool("isStundUp", false);
    }
    public void LieDown()
    {
        Idle();
        animator.SetBool("isLieDown", true);
    }
    
    public void StundUp()
    {
        //agent.updateRotation = false;
        animator.SetBool("isLieDown", false);
        animator.SetBool("isStundUp", true);
    }
}
