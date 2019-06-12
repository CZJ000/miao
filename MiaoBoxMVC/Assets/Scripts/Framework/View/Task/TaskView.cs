using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
public class TaskView : MonoBehaviour {


        /// <summary>
    /// 静态单例；
    /// </summary>
    public static TaskView Instance;
    /// <summary>
    /// 是否获取过所有组件；
    /// </summary>
    private bool IsGetAllComponents = false;

    #region 控制组件；

    public Button LeftButton;
    public Button RightButton;
    public Button CancelButton;
    public Button SureButton;

    public Transform FinishUIView;
    public Text GainExpText;
    public Text GainCatFoodText;
    #endregion



    #region 属性方法




    #endregion

    #region 控制后台通信

    #endregion



    /// <summary>
    /// 是否激活；通过设置Scale来显示
    /// </summary>
    public bool IsInvoke
    {
        set
        {
            transform.localScale = value ? Vector3.one : Vector3.zero;
        }
        get
        {
            return transform.localScale != Vector3.zero;
        }
    }


    private void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 获取所有的组件；
    /// </summary>
    void GetAllCompoenets()
    {
        if (IsGetAllComponents) return;
        IsGetAllComponents = true;

        CancelButton.onClick.AddListener(delegate ()
        {
            IsInvoke = false;
        }
        
        );

        RightButton.onClick.AddListener(delegate ()
        {
            AppFacade.getInstance.SendNotification(NotiConst.C_CHANGE_BATTLE_AI_USER);


        });
        LeftButton.onClick.AddListener(delegate ()
        {
            AppFacade.getInstance.SendNotification(NotiConst.C_CHANGE_BATTLE_AI_USER);
        });



        ////星星，对应的是PromoteBtn；
        //StarButton.onClick.AddListener(delegate ()
        //{
        //    CustomerMediator customerMediator = AppFacade.GetInstance().RetrieveMediator(CustomerMediator.NAME) as CustomerMediator;
        //    (customerMediator.ViewComponent as CustomerView).RemoveCustomer();
        //});
        ////货按钮，对应的是BattleBtn；
        //GoodsButton.onClick.AddListener(delegate ()
        //{
        //    //进行战斗场景的加载；
        //    (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene(EnumScene.SceneBattle);
        //});
        ////猫分组按钮，对应的是CatGroupBtn。
        //CatGroupButton.onClick.AddListener(delegate ()
        //{
        //    //打开猫分组；
        //    Debug.Log(" 打开猫分组");
        //    AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
        //    CatGroupMenuMediator catGroupMediator = AppFacade.getInstance.RetrieveMediator(CatGroupMenuMediator.NAME) as CatGroupMenuMediator;
        //    //            catGroupMediator.catGroupMenuView.catGroupMenuGameObject.SetActive(true);
        //    //0107 lgx 修改 显示猫分组不能这就草草的显示界面就行需要准备
        //    CatGroupContoler.Instance.menutype = MenuType.CatGroupMenu;
        //    CatGroupContoler.Instance.IsInvoke = true;
        //});
        //BulidButton.onClick.AddListener(delegate ()
        //{

        //});
        //SettingButton.onClick.AddListener(delegate ()
        //{

        //});
        ////翅膀按钮，对应的是Promote_Exp Btn
        //WingButton.onClick.AddListener(delegate ()
        //{
        //    //经验值+1000？
        //    SetExp(Exp + 1000);
        //});
    }


    /// <summary>
    /// 完成战斗后UI回显刷新
    /// </summary>
    /// <param name="arg" >战斗结束后需要传来的参数——BattleSettleVO  类</param>
    public void FinishBattle(object arg)
    {

        BattleSettleVO battleSettleVO = arg as BattleSettleVO;
        FinishUIView.transform.localScale = new Vector3(1,1,1);
        GainExpText.text = battleSettleVO.GainExp;
        GainCatFoodText.text = battleSettleVO.GainCatFood;
    }

}
