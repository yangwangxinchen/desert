using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

[RequireComponent(typeof(PressGesture))]
public class InfoCloseBtn : MonoBehaviour
{
    PressGesture pressGesture;

    private void Start()
    {
        pressGesture = transform.GetComponent<PressGesture>();
        pressGesture.Pressed += PressGesture_Pressed;
    }

    private void PressGesture_Pressed(object sender, System.EventArgs e)
    {

        Destroy(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        
    }

}
