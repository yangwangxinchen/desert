using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.AI;
using DG.Tweening;
public class ItemControl : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<PressGesture>() != null) GetComponent<PressGesture>().Pressed += ItemControl_Pressed; ;
    }

    private void ItemControl_Pressed(object sender, System.EventArgs e)
    {
        AnimalMove animalMove = transform.GetComponent<AnimalMove>();
        Animator animator = transform.GetComponent<Animator>();
        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        if (transform.tag == "Snake" || transform.tag == "Cow" || transform.tag == "Bear" || transform.tag == "Elephant" || transform.tag == "Giraffe" && agent != null)
        {
            
        }

        if (transform.tag == "Bear")
        {
            animator.SetTrigger("isStanding");
            animator.SetBool("isWalking", false);
        }
        else if (transform.tag == "Elephant")
        {
            animator.SetTrigger("isDrinking");
            animator.SetBool("isWalking", false);
        }
        else if (transform.tag == "Snake")
        {
            animator.SetTrigger("isAttacking");
            animator.SetBool("isWalking", false);
        }
        else if (transform.tag == "Giraffe")
        {
            animator.SetTrigger("isEating");
            animator.SetBool("isWalking", false);
        }
        else if (transform.tag == "Cow")
        {
            animator.SetTrigger("isEating");
            animator.SetBool("isWalking", false);
        }
        else if (transform.tag == "SeagulSit")
        {
            Vector3 startPos = transform.position;
            Vector3[] vectors = new Vector3[3];
            AudioSource audioSource = transform.GetComponent<AudioSource>();
            audioSource.Play();
            int randomNum = UnityEngine.Random.Range(0, 3);
            if (randomNum == 0)
            {
                vectors[0] = new Vector3(-41.0f, 10.3f, 25.5f);
            }
            else if (randomNum == 1)
            {
                vectors[0] = new Vector3(-25.0f, 10.3f, 21.5f);
            }
            else
            {
                vectors[0] = new Vector3(-46.0f, 10.0f, 3.8f);
            }
            vectors[1] = new Vector3(-200.0f, 12.0f, -37f);
            vectors[2] = startPos;

            transform.DOPath(vectors, 100.0f, PathType.CatmullRom).OnComplete(delegate
            {
                animator.SetBool("isSitting", true);
            }).SetLoops(0);
            //hit.collider.gameObject.transform.GetComponent<Tweener>().SetLoops(0);
            animator.SetBool("isSitting", false);
        }
        else if (transform.tag == "Horse")
        {
            agent.isStopped = false;
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            agent.speed = 3.5f;
            animalMove.RandomTarget();
            animalMove.SetDes();
        }

        print(transform.tag);
    }
}
