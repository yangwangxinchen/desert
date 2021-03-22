using UnityEngine.SceneManagement;
using UnityEngine;
//using TouchScript.Gestures;


public class TimeQuit : MonoBehaviour
{
    public Texture2D _BG;
    public bool _isQuit = false;

    private GUIStyle _gUIStyle;

    public static TimeQuit instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
      

        //预加载资源
       // LoadText.Ins.InitLoadText();
       // LoadAudio.Ins(this);
    }

    private void Start()
    {
        if (_isQuit)
        {
            _gUIStyle = new GUIStyle();
            _gUIStyle.normal.background = _BG;    //设置背景填充
            //gameObject.AddComponent<PressGesture>().Pressed += TimeQuit_Pressed;

            OpenPanel();
            Invoke("ClosePanel", 3);
            Invoke("OpenPanel", 1790);
            Invoke("Quit", 1800);
        }
        //Invoke("JumpScene", 1f);
    }

    void JumpScene()
    { 
        //SceneManager.LoadScene(1);
    }

    private void TimeQuit_Pressed(object sender, System.EventArgs e)
    {
        ClosePanel();
    }


    void ClosePanel()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
    void OpenPanel()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }
    void Quit()
    {
        Application.Quit();
    }

    void OnGUI()
    {
        if (_isQuit)
        {
            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 350, 500, 700), "", _gUIStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 300, 200, 50), "Close"))
            {
                ClosePanel();
            }
        }

    }

    private void OnDestroy()
    {
        //GetComponent<PressGesture>().Pressed -= TimeQuit_Pressed;
    }

}
