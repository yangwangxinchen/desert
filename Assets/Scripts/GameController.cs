using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TouchScript.Gestures;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(3072, 768, true);
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Debug.Log(Input.mousePosition);
        //    //Debug.DrawRay(Camera.main.transform.position, Input.mousePosition - Camera.main.transform.position, Color.blue);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        {
        //            AnimalMove animalMove = hit.collider.GetComponent<AnimalMove>();
        //            Animator animator = hit.collider.GetComponent<Animator>();
        //            NavMeshAgent agent = hit.collider.GetComponent<NavMeshAgent>();
        //            if (hit.collider.tag != "Horse" && hit.collider.tag != "SeagulSit" && agent != null)
        //            {
        //                agent.isStopped = true;
        //            }

        //            if (hit.collider.tag == "Bear")
        //            {
        //                animator.SetTrigger("isStanding");
        //                animator.SetBool("isWalking", false);
        //            }
        //            else if (hit.collider.tag == "Elephant")
        //            {
        //                animator.SetTrigger("isDrinking");
        //                animator.SetBool("isWalking", false);
        //            }
        //            else if (hit.collider.tag == "Giraffe")
        //            {
        //                animator.SetTrigger("isEating");
        //                animator.SetBool("isWalking", false);
        //            }
        //            else if (hit.collider.tag == "SeagulSit")
        //            {
        //                //hit.collider.gameObject.AddComponent<DOTweenPath>();
        //                //DOTweenPath doPath = hit.collider.gameObject.GetComponent<DOTweenPath>();
        //                //doPath.wps.Add(new Vector3(-46.0f,10.0f,3.8f));
        //                //doPath.wps.Add(new Vector3(-200.0f,12.0f,-37f));
        //                //doPath.wps.Add(new Vector3(-33.0f, 3.0f, 37f));
        //                //doPath.duration = 5;
        //                //doPath.isSpeedBased = true;
        //                //doPath.loops = 0;
        //                //doPath.lockRotation = AxisConstraint.X;
        //                //doPath.orientType =  DG.Tweening.Plugins.Options.OrientType.ToPath;

        //                Vector3 startPos = hit.collider.gameObject.transform.position;
        //                Vector3[] vectors = new Vector3[3];

        //                int randomNum = UnityEngine.Random.Range(0, 3);
        //                if (randomNum == 0)
        //                {
        //                    vectors[0] = new Vector3(-41.0f, 10.3f, 25.5f);
        //                }
        //                else if (randomNum == 1)
        //                {
        //                    vectors[0] = new Vector3(-25.0f, 10.3f, 21.5f);
        //                }
        //                else
        //                {
        //                    vectors[0] = new Vector3(-46.0f, 10.0f, 3.8f);
        //                }
        //                vectors[1] = new Vector3(-200.0f, 12.0f, -37f);
        //                vectors[2] = startPos;

        //                hit.collider.gameObject.transform.DOPath(vectors, 60.0f, PathType.CatmullRom).OnComplete(delegate
        //                 {
        //                     animator.SetBool("isSitting", true);
        //                 }).SetLoops(0);
        //                //hit.collider.gameObject.transform.GetComponent<Tweener>().SetLoops(0);
        //                animator.SetBool("isSitting", false);
        //            }
        //            else if (hit.collider.tag == "Horse")
        //            {
        //                animator.SetBool("isRunning", true);
        //                animator.SetBool("isWalking", false);
        //                agent.speed = 3.5f;
        //                animalMove.RandomTarget();
        //                animalMove.SetDes();
        //            }

        //            print(hit.collider.tag);
        //        }
        //    }
        //}
    }
}
