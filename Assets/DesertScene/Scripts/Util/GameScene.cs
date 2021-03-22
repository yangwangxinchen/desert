using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public enum Resolution
    {
        oneside,   //单通道
        twoside,
        threeside,
        fourside
    }

    public Resolution resolution;
    // Start is called before the first frame update
    void Start()
    {
        switch (resolution)
        {
            case Resolution.oneside:
                Screen.SetResolution(1024, 768, true);
                break;
            case Resolution.twoside:
                Screen.SetResolution(2048, 768, true);
                break;
            case Resolution.threeside:
                Screen.SetResolution(3072, 768, true);
                break;
            case Resolution.fourside:
                Screen.SetResolution(5760, 1200, true);
                break;
            default:
                break;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.platform==RuntimePlatform.WindowsPlayer)
            {
                Application.Quit();
            }
            
        }
    }
}
