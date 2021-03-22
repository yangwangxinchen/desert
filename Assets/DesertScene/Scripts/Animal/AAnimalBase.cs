using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 使用A*插件导航
/// 使用RVO实现内部规避
/// </summary>
public class AAnimalBase : MonoBehaviour
{
    protected Animator animator;
    PressGesture pressGesture;
    protected Transform[] points;
    protected IAstarAI ai;
    /// <summary>
    /// Resources 下面的音频介绍与图片信息皆根据该名字查找
    /// </summary>
    public string animalName;

    AudioSource animalSound;
    /// <summary>
    /// 是否处于点击的不移动状态
    /// </summary>
    protected bool isClickDontmove;
    Transform parent;

    [Range(0.5f, 1.5f)]
    public float animatorSpeed = 1.0f;

    public void Start()
    {
        animator = transform.GetComponent<Animator>();
        ai = GetComponent<IAstarAI>();

        pressGesture = transform.GetComponent<PressGesture>();
        pressGesture.Pressed += PressGesture_Pressed;

        animalSound = GetComponent<AudioSource>();

        parent = GameObject.Find("Canvas").transform;
        points = GetWaypoints();
        animator.speed = animatorSpeed;

        Walk();
    }

    public Transform[] GetWaypoints()
    {
        Transform point = GameObject.Find("Points").transform;
        Transform[] temps = new Transform[point.childCount];
        for (int i = 0; i < point.childCount; i++)
        {
            temps[i] = point.GetChild(i).transform;
        }

        return temps;
    }
    public void PressGesture_Pressed(object sender, System.EventArgs e)
    {
        ShowAnimalInfo();
    }
    GameObject infoGo;
    /// <summary>
    /// 显示信息
    /// </summary>
    public void ShowAnimalInfo()
    {
        isClickDontmove = true;
        bool isFlag = false;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == animalName)
            {
                //同一个动物信息存在
                isFlag = true;
                break;
            }
            else
            {
                //不是同一个 摧毁之前的动物信息
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        if (isFlag == false)
        {
            GameObject prefab = Resources.Load<GameObject>("InfoItem");
            infoGo = Instantiate(prefab, parent);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            infoGo.GetComponent<RectTransform>().anchoredPosition = screenPos;
            //infoGo.transform.localScale = Vector3.one;
            Vector3 pos = infoGo.transform.localPosition;

            if (pos.x < Screen.width / 2 && pos.y < Screen.height / 2)
            {
                infoGo.transform.DOBlendableLocalMoveBy(new Vector3(150, 150, 0), 1);
            }
            else if (pos.x > Screen.width / 2 && pos.y < Screen.height / 2)
            {
                infoGo.transform.DOBlendableLocalMoveBy(new Vector3(-150, 150, 0), 1);
            }
            else if (pos.x < Screen.width / 2 && pos.y > Screen.height / 2)
            {
                infoGo.transform.DOBlendableLocalMoveBy(new Vector3(150, -150, 0), 1);
            }
            else
            {
                infoGo.transform.DOBlendableLocalMoveBy(new Vector3(-150, -150, 0), 1);
            }



            infoGo.name = animalName;
            infoGo.GetComponent<InfoItem>().SetInfo(animalName);
            //注册恢复移动

            infoGo.GetComponent<InfoItem>().infoDestroyDelegate += AgentMove;


            ShowAnimalAction();

        }
    }

    /// <summary>
    /// 展示动物的表现行为
    /// </summary>
    public virtual void ShowAnimalAction()
    {
        //播放动物的叫声音效
        animalSound.Play();
        isCloseInfo = false;
        Idle();
    }
    /// <summary>
    /// 当前目标位置
    /// </summary>
    Vector3 currentTargetPos=Vector3.zero;

    int lastIndex=0;
    public void SetDes()
    {        
        int index = Random.Range(0, points.Length);
        currentTargetPos = points[index].position;
      //  if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))        
            ai.destination = currentTargetPos;
            ai.SearchPath();
        
    }

    IEnumerator DelayWalk()
    {
        yield return new WaitForSeconds(0.5f);
        ai.canMove = true;
    }
    public void Walk()
    {
        animator.SetBool("isWalk", true);
        SetDes();
        StartCoroutine(DelayWalk());
    }


    public virtual void Idle()
    {
        ai.canMove = false;
        animator.SetBool("isWalk", false);
    }
    /// <summary>
    /// 是否关闭了信息弹窗
    /// </summary>
    protected bool isCloseInfo;

    /// <summary>
    /// 恢复移动
    /// </summary>
    public virtual void AgentMove()
    {
        isCloseInfo = true;
        //Walk();
    }
    protected AnimatorStateInfo animatorInfo;
    /// <summary>
    /// 恢复移动延迟
    /// </summary>
    /// <param name="action">恢复移动时将动画状态更改</param>
    /// <param name="name">动画片段名</param>
    /// <returns></returns>
    public IEnumerator DelayAgentMove(UnityAction action, string name)
    {

        if (action != null)
        {
            action();
        }
        yield return null;
        while (isCloseInfo)
        {
            yield return new WaitForFixedUpdate();
            //当前动画层
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            //动画片段信息  0~1 0表示刚开始 1表示结束     animatorInfo.IsName 片段名
            //判断最后一个动画片段是否播放完了
            if ((animatorInfo.normalizedTime >= 0.9f) && (animatorInfo.IsName(name)))
            {
                isClickDontmove = false;
                Walk();
                break;
            }
        }
    }

    float timer;
    public void FixedUpdate()
    {
        //到达目的地后idle 5s后自动导航
        if (currentTargetPos!=Vector3.zero&& isClickDontmove == false)
        {
            if (Mathf.Abs(Vector3.Distance(currentTargetPos, transform.position)) <= 2)
            {
                Idle();
                timer += Time.deltaTime;
                if (timer >= 5)
                {
                    //Debug.Log("自动导航");
                    Walk();
                    timer = 0;
                }

            }
        }
        
    }


    private void OnDestroy()
    {
        if (infoGo != null)
        {
            infoGo.GetComponent<InfoItem>().infoDestroyDelegate -= AgentMove;
        }

    }



}
