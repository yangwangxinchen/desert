using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pathfinding.RVO;

public class AnimalObstacle : MonoBehaviour
{
    public Transform[] trans;
    Vector3[] verts=new Vector3[4];
    void Start()
    {
        //Get the simulator for this scene
        Pathfinding.RVO.Simulator sim = (FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator).GetSimulator();

        //Define the vertices of our obstacle
        //Vector3[] verts = new Vector3[] { new Vector3(1, 0, -1), new Vector3(1, 0, 1), new Vector3(-1, 0, 1), new Vector3(-1, 0, -1) };

        for (int i = 0; i < trans.Length; i++)
        {
            verts[i] = trans[i].position;
        }

        //Add our obstacle to the simulation, we set the height to 2 units
        sim.AddObstacle(verts, 2);  
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
