using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnRhino : AAnimalBase
{
    public override void AgentMove()
    {
        base.AgentMove();
        StartCoroutine(DelayAgentMove(StundUp, "Wake Up"));
       
    }

    public override void ShowAnimalAction()
    {
        base.ShowAnimalAction();
        LieDown();
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
        //旋转关闭
       // agent.updateRotation = false;
        animator.SetBool("isLieDown", false);
        animator.SetBool("isStundUp", true);
    }
}
