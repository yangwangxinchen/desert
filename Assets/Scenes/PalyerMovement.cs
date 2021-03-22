using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PalyerMovement : MonoBehaviour
{
   private NavMeshAgent agent;
    Animator animator;
    void Start()
    {
        //获取角色上的NavMeshAgent组件
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider)
            {
                // Debug.Log(hit.point);
                //hit.point射线触碰的Position
                //SetDestination设置下一步的位置
                animator.SetBool("isWalking", true);
                agent.SetDestination(hit.point);
                //transform.LookAt(hit.point);
            }
        }
    }

}
