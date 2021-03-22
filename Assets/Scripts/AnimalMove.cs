using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.ImageEffects;


public class AnimalMove : MonoBehaviour
{
    float minX =-31.0f;
    float minZ =45.0f;
    float minY =0.0f;
              
    float maxX =-10.0f;
    float maxZ =80.0f;
    float maxY =0.0f;

    NavMeshAgent agent;
    Vector3 target;
    Animator animator;
    int index;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        StartCoroutine(enumerator());
        StartCoroutine(randomIndex());
        agent = transform.GetComponent<NavMeshAgent>();
        target = transform.position;
    }
    IEnumerator randomIndex()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            index = Random.Range(0, 2);
        }
    }
    IEnumerator enumerator()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // 移动终止
                //Debug.Log("移动结束了");
                if (transform.tag == "Horse")
                {
                    animator.SetBool("isRunning", false);

                }
                agent.isStopped = true;
                if (index==0)
                {
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    animator.SetBool("isWalking", true);
                    RandomTarget();
                    agent.isStopped = false;
                    agent.speed = 0.8f;
                    if (transform.tag == "Elephant")
                    {
                        agent.speed = 1.0f;
                    }
                    if (transform.tag == "Snake")
                    {
                        agent.speed = 2.2f;
                    }
                    SetDes();
                }
            }
            
        }
    }
    public void SetDes()
    {
        agent.SetDestination(target);
    }
    public void RandomTarget()
    {
        float x = UnityEngine.Random.Range(minX, maxX);
        float z = UnityEngine.Random.Range(minZ, maxZ);
        float y = UnityEngine.Random.Range(minY, maxY);
        target = new Vector3(x, y, z);
    }
}
