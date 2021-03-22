using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AudioControl : MonoBehaviour
{
    AudioSource audioSource;
    NavMeshAgent agent ;
    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        agent = transform.GetComponent<NavMeshAgent>();
    }
    public void AudioPlay()
    {

        audioSource.Play();
        if (transform.tag != "Horse")
        {
            agent.isStopped = true;
        }

    }
    public void SetStop()
    {
        agent.isStopped = false;
    }
}
