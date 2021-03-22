using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetVisible", 2.0f);
    }

    // Update is called once per frame
    void SetVisible()
    {
        gameObject.SetActive(false);
    }
}
