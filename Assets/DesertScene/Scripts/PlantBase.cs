using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    
    public string plantName;

    Transform parent;
    PressGesture pressGesture;
    // Start is called before the first frame update
    void Start()
    {
        pressGesture = transform.GetComponent<PressGesture>();
        pressGesture.Pressed += PressGesture_Pressed;
        parent = GameObject.Find("Canvas").transform;
    }

    private void PressGesture_Pressed(object sender, EventArgs e)
    {
        ShowPlantInfo();
    }

    public void ShowPlantInfo()
    {
        bool isFlag = false;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == plantName)
            {
                //同一个植物信息存在
                isFlag = true;
                break;
            }
            else
            {
                //不是同一个 摧毁之前的信息
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        if (isFlag == false)
        {
            GameObject prefab = Resources.Load<GameObject>("InfoItem");
            GameObject infoGo = Instantiate(prefab, parent, false);
            infoGo.name = plantName;
            infoGo.GetComponent<InfoItem>().SetInfo(plantName);

            //Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            //infoGo.GetComponent<RectTransform>().anchoredPosition = screenPos;

            //Vector3 pos = infoGo.transform.localPosition;
            if (plantName == "胡杨")
            {
                infoGo.transform.localPosition = new Vector2(-162, 121);
            }

        }          
    }
}
