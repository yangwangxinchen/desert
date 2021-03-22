using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SeagulSit : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    public void setAnimatorSit()
    {
        animator.SetBool("isSitting", true);

        DOTweenPath dOTweenPath = transform.gameObject.GetComponent<DOTweenPath>();
        Destroy(dOTweenPath);
    }
}
