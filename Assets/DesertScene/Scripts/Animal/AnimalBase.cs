using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;


/// <summary>
/// 使用NavMeshAgent导航   
/// 缺点：1.活动的agent会推走不活动的agent 2.agent互怼，有时导航会卡住 3.与rigidbody冲突
/// </summary>
public class AnimalBase : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent agent;
    PressGesture pressGesture;
    protected  Transform[] points;

    /// <summary>
    /// Resources 下面的音频介绍与图片信息皆根据该名字查找
    /// </summary>
    public string animalName;

    AudioSource animalSound;
    /// <summary>
    /// 是否处于点击的暂停状态
    /// </summary>
    protected bool isStop;
    Transform parent;

    [Range(0.5f, 1.5f)]
    public float animatorSpeed = 1.0f;

    public void Start()
    {
        animator = transform.GetComponent<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();

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
        isStop = true;
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
    }
    Vector3 target;
    public void Walk()
    {
        animator.SetBool("isWalk", true);
        agent.isStopped = false;


        int index = Random.Range(0, points.Length);

        agent.SetDestination(points[index].position);
        target = points[index].position;
        agent.updateRotation = true;
        //StartCoroutine(DelayUpdateRot());
    }

    IEnumerator DelayUpdateRot()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(target - transform.position), Time.deltaTime * 0.1f);
        }

    }


    public virtual void Idle()
    {
        agent.isStopped = true;
        agent.updateRotation = false;
        //停止导航
        agent.SetDestination(transform.position);
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
                isStop = false;
                Walk();
                break;
            }
        }
    }

    float timer;
    /// <summary>
    /// 射线长度
    /// </summary>
    public float distance = 2.8f;
    public void FixedUpdate()
    {
        //下次导航  判断当前是否在终点
        //agent.pathPending  是否正在计算路径的过程中
        //agent.remainingDistance 代理的位置和当前路径上的目标之间的距离。
        if (!agent.pathPending && agent.remainingDistance <= 1 && isStop == false)
        {
            //在距目标位置的这一距离内停止
            //agent.stoppingDistance = 0.05f;

            Idle();

            // Debug.Log(agent.remainingDistance);
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                Walk();
                timer = 0;
            }
        }

        //射线检测 避免导航推走
        Vector3 tep = transform.position;
        tep.y += 1;
        Ray ray = new Ray(tep, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                agent.velocity = Vector3.zero;
            }
        }
        Debug.DrawRay(tep, transform.forward * distance);
    }


    private void OnDestroy()
    {
        if (infoGo!=null)
        {
            infoGo.GetComponent<InfoItem>().infoDestroyDelegate -= AgentMove;
        }
        
    }
}




