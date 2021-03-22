using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;
using UnityEngine.UI;

public class TreadSnow : MonoBehaviour
{
    PressGesture myPress;

    public GameObject Box;
    public GameObject hole;

    //public GameObject bg;

    //private float minX = -6.56f;
    //private float maxX= 6.491f;
    //private float minY= -3.85f;
    //private float maxY= 5.809f;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 1000; i++)
        //{
        //   GameObject go= Instantiate<GameObject>(bg, transform);
        //    go.transform.position = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 77.9f);
        //}
        Screen.SetResolution(1024, 768, true);
        myPress = GetComponent<PressGesture>();
        myPress.Pressed += _Press;
    }

    private void _Press(object sender, EventArgs e)
    {
        GameObject go= Instantiate<GameObject>(hole);
        go.transform.eulerAngles =  new Vector3(90.0f,0.0f,0.0f);
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(myPress.ScreenPosition);
        go.transform.position = new Vector3(pos1.x, 1.0f, pos1.z);
        go.transform.localScale = go.transform.localScale * UnityEngine.Random.Range(0.3f, 0.7f);
        
        GameObject box = Instantiate<GameObject>(Box);
        box.transform.position = new Vector3(pos1.x, 1.5f, pos1.z);
        box.transform.localScale = go.transform.localScale;

        AudioSource audioSource = transform.GetComponent<AudioSource>();
        audioSource.Play();
        Destroy(go,3.0f);
        Destroy(box,3.0f);

    }
}
