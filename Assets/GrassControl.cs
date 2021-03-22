using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class GrassControl : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<PressGesture>() != null) GetComponent<PressGesture>().Pressed += ItemControl_Pressed;
    }

    private void ItemControl_Pressed(object sender, System.EventArgs e)
    {
        AudioSource audioSource = transform.GetComponent<AudioSource>();
        audioSource.Play();
    }
}
