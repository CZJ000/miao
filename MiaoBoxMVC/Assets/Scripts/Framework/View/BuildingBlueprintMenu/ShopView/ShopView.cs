using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;
using Global;
public class ShopView : BaseUIForm {




    #region Mono字段
    public Button sysBtn;
    public Button closeBtn;

#endregion

    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {

            //  Camera.main.GetComponent<UICamera>().eventType = UICamera.EventType.UI_2D;
            return gameObject.activeSelf;
            // return gameObject.activeInHierarchy;
        }
        set
        {
            if (value == false)
            {
                CloseUIForm();
            }
            else
            {
                if (!gameObject.activeSelf)
                    OpenUIForm("ShopView");
            }


        }
    }


    public void Show()
    {
        IsInvoke = true;
      
    }


    #region 事件回调
    public void OnSysBtnOn()
    {
        OpenUIForm("ShopMenuView");
        UIBaseBehaviour<ShopMenuViewMediator>.CreateUI<ShopMenuView>();
        ShopMenuView view= AppFacade.Instance.RetrieveMediator(ShopMenuViewMediator.NAME).ViewComponent as ShopMenuView;
        view.Show();
      
    }

    private void OnCloseBtnOn()
    {
        IsInvoke = false;
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }

    // Use this for initialization
    void Start () {

        sysBtn.onClick.AddListener(OnSysBtnOn);
        closeBtn.onClick.AddListener(OnCloseBtnOn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
