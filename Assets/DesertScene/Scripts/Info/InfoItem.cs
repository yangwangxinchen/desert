using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void InfoDestroyDelegate();
public class InfoItem : MonoBehaviour
{
    public Image image;
    private AudioSource audioInfo;
    string itemName;
    public InfoDestroyDelegate infoDestroyDelegate;
    private void Start()
    {
        //audioInfo= gameObject.AddComponent<AudioSource>();
    }

    public void SetInfo(string animalName)
    {
        itemName = animalName;
        image.sprite = Resources.Load<Sprite>("Images/" + animalName);

        if (audioInfo==null)
        {
            audioInfo = gameObject.AddComponent<AudioSource>();        
        }
        audioInfo.clip = Resources.Load<AudioClip>("Clips/" + animalName);
        audioInfo.Play();

        StartCoroutine(DelayDes());
    }

    IEnumerator DelayDes()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            //Debug.Log(audioInfo.isPlaying);
            if (audioInfo.isPlaying==false)
            {           
                DestroySelf();          
                break;
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        //ResumeMove();
        if (infoDestroyDelegate!=null)
        {
            Debug.Log(itemName);
            infoDestroyDelegate();          
        }
    }
}
