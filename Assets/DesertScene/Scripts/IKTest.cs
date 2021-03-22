using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    [Range(0, 1)]
    // IK权重
    public float weight = 1;
    // IK跟随的物体
    public Transform rightHandFollowObj;
    public Transform leftHandFollowObj;
    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // 设置Position和Rotation权重
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);

            // 改变Position和Rotation
            if (rightHandFollowObj != null)
            {
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandFollowObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandFollowObj.rotation);
            }
            if (leftHandFollowObj != null)
            {
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandFollowObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandFollowObj.rotation);
            }
        }
    }

}
