using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SUIFW;
using Global;
/// <summary>
/// UI控制器的静态单例类；
/// 控制的UI：
/// </summary>
public class MessageView : BaseUIForm
{


  public  enum ShowType
    {
        WithYesAndNoBtn,
        WithYesBtn,
       
    }
    /* 字段 */
    private static MessageView Instance = null;

    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        //get
        //{
        //    return transform.localScale != Vector3.zero;
        //}
        //set
        //{
        //    transform.localScale = value ? Vector3.one : Vector3.zero;
        //}
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            if (value)
            {
                if(!gameObject.activeSelf)
                OpenUIForm("MessageView");

            }
            else
            {
                CloseUIForm();
            }
        }
    }

    /// <summary>
    /// 静态单例；
    /// </summary>
    public static MessageView GetInstance()
    {
        if (Instance == null)
        {
            UIManager.GetInstance().ShowUIForms("MessageView");
            return Instance;
        }

        return Instance;
    }
   
 
    /// <summary>
    /// 是否获得过所有组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 确认之后执行的回调方法；
    /// </summary>
    public CallBack AfterConfrim;
    /// <summary>
    /// 取消执行之后的回调方法；
    /// </summary>
    public CallBack AfterCancel;
    /// <summary>
    /// 提示文本；
    /// </summary>
    public string ConfrimMessage
    {
        get
        {
            return MessageText.text;
        }
        set
        {
            MessageText.text = value;
        }
    }





    #region 组件

    RectTransform rectTransform;
    /// <summary>
    /// 确认按钮；
    /// </summary>
    public Button ConfirmBtn;
    /// <summary>
    /// 取消按钮；
    /// </summary>
    public Button CancelBtn;
    /// <summary>
    /// 信息文本；
    /// </summary>
    public Text MessageText;


    public Image image;

    #endregion


    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;

    }

    /// <summary>
    /// 用这个方法初始化	
    /// </summary>
    void Start()
    {
        GetAllCompoenets();
    }

    /// <summary>
    /// Update每帧调用一次
    /// </summary>
    void Update()
    {

    }
    
    /// <summary>
    /// 展示面板的方法(无回调方法)；需要确认和取消按钮
    /// </summary>
    /// <param name="Mes">需要提示的文本</param>
    public void ShowMessage(string Mes, CallBack _afterConfrim = null, CallBack _afterCancel = null, ShowType showType=ShowType.WithYesBtn,float fadeTime=0)
    {
        IsInvoke = true;
        switch (showType)
        {
            case ShowType.WithYesBtn:

                CancelBtn.gameObject.SetActive(false);
                ConfirmBtn.gameObject.SetActive(true);
                ConfirmBtn.transform.localPosition = new Vector3(0, ConfirmBtn.transform.localPosition.y,0);
                break;
            case ShowType.WithYesAndNoBtn:
                CancelBtn.gameObject.SetActive(true);
                ConfirmBtn.gameObject.SetActive(true);
                break;
          
        }

        //赋值并展示；
        MessageText.text = Mes;
        AfterConfrim = _afterConfrim;
        AfterCancel = _afterCancel;
       
    }





    //IEnumerator Fade(float time)
    //{
         
    //}




    /// <summary>
    /// 获取所有组件的方法；
    /// </summary>
    public void GetAllCompoenets()
    {
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        //获取组件；

        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        ConfirmBtn.onClick.AddListener(delegate ()
        {
            if (AfterConfrim != null)
            {
                //如果有确认的回调方法，则执行回调方法，执行完成之后清空回调方法；
                AfterConfrim();
                AfterConfrim = null;
            }
            //关闭面板；
            IsInvoke = false;
        });
        CancelBtn.onClick.AddListener(delegate ()
        {
            if (AfterCancel != null)
            {
                //如果有取消的回调方法，则执行回调方法，执行完成之后清空回调方法；
                AfterCancel();
                AfterCancel = null;
            }
            //关闭面板；
            IsInvoke = false;
        });
    }

}
