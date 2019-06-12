using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
using UnityEngine.EventSystems;
using SUIFW;
/// <summary>
/// 一个不需要传参数，无返回值的回调函数；
/// </summary>
public delegate void CallBack();

/// <summary>
/// 主菜单面板控制；单例类；
/// </summary>
public class MainMenuView : BaseUIForm
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static MainMenuView Instance;
    /// <summary>
    /// 是否获取过所有组件；
    /// </summary>
    private bool IsGetAllComponents = false;

    #region 控制组件；

    public RawImage HeadImage;

    public Text UserNameText;

    public Text DiamondCountText;

    public Text LevelText;

    public Text TimeText;

    public Text MoneyText;

    public Text PowerText;

    public Text WingText;

    public Button StarButton;

    public Button GoodsButton;

    public Button CatGroupButton;

    public Button BulidButton;

    public Button SettingButton;

    public Button WingButton;

    /// <summary>
    /// 用于遮挡照相机，防止穿透2D UI；
    /// </summary>
 //   public  GameObject blockPanel;

    #endregion

    private void Awake()
    {
        Instance = this;
        //设置回调函数；

        

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Fixed;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;

        //获取点击事件；
        GetAllCompoenets();

    }

   

    // Use this for initialization
    void Start()
    {
      
         
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region 与后台的通信
    
    /// <summary>
    /// 设置金币；
    /// </summary>
    /// <param name="count"></param>
    public void SetGold(int count)
    {
        AppFacade.GetInstance().SendNotification(NotiConst.SET_GOLD, count);
    }
    /// <summary>
    /// 设置经验值；
    /// </summary>
    /// <param name="count"></param>
    public void SetExp(int count)
    {
        AppFacade.GetInstance().SendNotification(NotiConst.SET_EXP, count);
    }

    #endregion


    /// <summary>
    /// 获取所有的组件；
    /// </summary>
    void GetAllCompoenets()
    {
        if (IsGetAllComponents) return;
        IsGetAllComponents = true;
        //星星，对应的是PromoteBtn；
        StarButton.onClick.AddListener(delegate ()
        {
            CustomerMediator customerMediator = AppFacade.GetInstance().RetrieveMediator(CustomerMediator.NAME) as CustomerMediator;
            (customerMediator.ViewComponent as CustomerView).RemoveCustomer();
        });
        //货按钮，对应的是BattleBtn；
        GoodsButton.onClick.AddListener(delegate ()
        {
            //进行战斗场景的加载；
             (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene( EnumScene.SceneBattle);
            //进入任务系统；（未来写Command）
            
            

        });
        //猫分组按钮，对应的是CatGroupBtn。
        CatGroupButton.onClick.AddListener(delegate ()
        {
            //打开猫分组；
            Debug.Log(" 打开猫分组");
            //CatGroupContoler.Instance.IsInvoke = true;
            //AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
            //CatGroupMenuMediator catGroupMediator = AppFacade.getInstance.RetrieveMediator(CatGroupMenuMediator.NAME) as CatGroupMenuMediator;

            OpenUIForm("CatGroupView");
            UIBaseBehaviour<CatGroupViewMediator>.CreateUI<CatGroupView>();

            CatGroupViewMediator catGroupviewMediator =  AppFacade.getInstance.RetrieveMediator(CatGroupViewMediator.NAME) as CatGroupViewMediator;

            CatGroupView catGroupView= catGroupviewMediator.CatGroupView;
            catGroupView.OpenCatGroupView();


            //            catGroupMediator.catGroupMenuView.catGroupMenuGameObject.SetActive(true);
            //0107 lgx 修改 显示猫分组不能这就草草的显示界面就行需要准备
          //  CatGroupContoler.Instance.menutype= MenuType.CatGroupMenu;
        });
        BulidButton.onClick.AddListener(delegate ()
        {

        });
        SettingButton.onClick.AddListener(delegate ()
        {
            (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene(EnumScene.SceneStrategy);
        });
        //翅膀按钮，对应的是Promote_Exp Btn
        WingButton.onClick.AddListener(delegate ()
        {
            //经验值+1000？
            SetExp(Exp + 1000);
        });
    }

    #region 控制方法

    /// <summary>
    /// 用户头像；
    /// </summary>
    public Texture UserHeadImageTexture
    {
        get
        {
            return HeadImage.texture;
        }
        set
        {
            HeadImage.texture = value;
        }
    }

    /// <summary>
    /// 用户名；
    /// </summary>
    public string PlayerName
    {
        get
        {
            return UserNameText.text;
        }
        set
        {
            UserNameText.text = value;
        }
    }

    /// <summary>
    /// 钻石数量；
    /// </summary>
    public int Diamond
    {
        get
        {
            return int.Parse(DiamondCountText.text);
        }
        set
        {
            DiamondCountText.text = value.ToString();
        }
    }

    /// <summary>
    /// 等级；
    /// </summary>
    public int Level
    {
        get
        {
            return int.Parse(LevelText.text);
        }
        set
        {
            LevelText.text = value.ToString();
        }
    }

    /// <summary>
    /// 时间；
    /// </summary>
    public string Time
    {
        get
        {
            return TimeText.text;
        }
        set
        {
            TimeText.text = value;
        }
    }

    /// <summary>
    /// 金币
    /// </summary>
    public int Gold
    {
        get
        {
            return int.Parse(MoneyText.text);
        }
        set
        {
            MoneyText.text = value.ToString();
        }
    }

    /// <summary>
    /// 精力；
    /// </summary>
    public int Stamina
    {
        get
        {
            return int.Parse(PowerText.text);
        }
        set
        {
            PowerText.text = value.ToString();
        }
    }

    /// <summary>
    /// 翅膀
    /// </summary>
    public int Exp
    {
        get
        {
            return int.Parse(WingText.text);
        }
        set
        {
            WingText.text = value.ToString();
        }
    }
    
    #endregion
    
}
