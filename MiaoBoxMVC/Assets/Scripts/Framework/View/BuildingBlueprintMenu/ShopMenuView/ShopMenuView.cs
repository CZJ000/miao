using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
using SUIFW;
public class ShopMenuView : BaseUIForm {




#region  Mono字段
    public Button leftBtn;
    public Button rightBtn;
    public Button cancelBtn;
    public Button sortChangeBtn;
    public Button tipWindowCloseBtn;
    public Button tipWindowSureBtn;
   

    public Text buidingName;
    public Text recruitCatName;
    public Text introText;
    public Text level;
    public Text sortTypeText;
    public Text buyBuidingName;
    public GameObject[] GoodsParents;
    public GameObject modelRef;
    public GameObject tipWindow;

    #endregion

    #region 字段

    public static int buildingSelectItem;

    int sortTypePoint ;
    int point;
    string[] sortText;
    int[] bluePrintIdList;
    int shopMenuLayer;


    public VoidDelegate resetTipWinPosi;

    public VoidDelegate cancelChangeModel;


    /// <summary>
    /// int :id    string: 可雇佣猫的名字；
    /// </summary>
    private Dictionary<int, string> recruitTypeDic;


    /// <summary>
    /// 建筑蓝图静态数据 用Tag值进行分开， int--蓝图类型：建筑、设施等等
    /// </summary>
    private Dictionary<int, List<stat_blueprintRow>> buildingBluepritData = new Dictionary<int, List<stat_blueprintRow>>();


    /// <summary>
    /// 所有的建筑蓝图数据，用建筑ID存储。方便查找  
    /// </summary>
    private Dictionary<int, stat_blueprintRow> allBuildingBluepritData = null;


    /// <summary>
    /// 建筑数据 
    // from-- BulidBluePrintProxy
    /// </summary>
    private Dictionary<int, stat_buildingRow> allBulidData = new Dictionary<int, stat_buildingRow>();




    #endregion



    #region 属性

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
                    OpenUIForm("ShopMenuView");
            }
            //if (transform.localScale == Vector3.one)
            //{
            //    MainMenuView.Instance.blockPanel.SetActive(true);
            //}


        }
    }


    #endregion


    #region 方法



    public void GoodsInBuyField(int modelId)
    {
        buildingSelectItem = modelId;

        buyBuidingName.text = allBuildingBluepritData[modelId].name;
        tipWindow.SetActive(true);
    }





    /// <summary>
    /// 点击3DUI时候刷新建筑介绍UI信息
    /// </summary>
    /// <param name="id">模型id</param>
    public void SetTextInfoWhenSelect(int id)
    {
        
      
        recruitCatName.text = recruitTypeDic[id];
        buidingName.text = allBuildingBluepritData[id].name;
        introText.text = allBuildingBluepritData[id].description;
    }





    /// <summary>
    /// 设置可雇佣信息
    /// </summary>
    void SetEmployeeNameFromBuilding()
    {
        if (recruitTypeDic == null)
        {
            recruitTypeDic = new Dictionary<int, string>();
        }
            List<stat_catRow> list = stat_cat.GetInstance().rowList;
        //for (int i=0;i<allBulidData.Count;i++)
        //{
        //    int id = int.Parse(buildingData[i]);
        //    int recruitId = int.Parse(allBulidData[id].recruittype);
        //    recruitTypeDic[id]= list[recruitId - 2].name;
        //}

            foreach (int i in allBulidData.Keys)
            {
                int recruitId = int.Parse(allBulidData[i].recruittype);
                recruitTypeDic[i] = list[recruitId - 2].name;
            }

        

    }


    /// <summary>
    /// 获取建筑的信息，在获取信息之后展示数据
    /// </summary>
    public void GetBluePrintData()
    {
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDINGBLUEPRINT_DATA);
        AppFacade.GetInstance().SendNotification(NotiConst.GET_STATBUILD_DATA);
    }


    public void Show()
    {
        GetBluePrintData();
        IsInvoke = true;
        RefreshGoodsList();
    }

    /// <summary>
    /// 设置蓝图的静态数据，在启动的时候读取
    /// </summary>
    /// <param name="data">Data.</param>
    public void setBuildingBlueprintData(object data)
    {

        Debug.Log("设置商店静态蓝图数据");
        int i = 0;
       
        allBuildingBluepritData = data as Dictionary<int, stat_blueprintRow>;
        bluePrintIdList = new int[allBuildingBluepritData.Count];
        
        foreach (stat_blueprintRow blueprint in allBuildingBluepritData.Values)
        {
            if (buildingBluepritData == null)
            {
                buildingBluepritData = new Dictionary<int, List<stat_blueprintRow>>();
            }
            if (buildingBluepritData.ContainsKey(blueprint.type))
            {
                buildingBluepritData[blueprint.type].Add(blueprint);
            }
            else
            {
                
                List<stat_blueprintRow> list = new List<stat_blueprintRow>();
                list.Add(blueprint);
                buildingBluepritData[blueprint.type] = list;
            }
            bluePrintIdList[i++] = blueprint.id;
        }
    }


    /// <summary>
    /// 设置建筑静态数据
    /// </summary>
    /// <param name="data"></param>
    public void setBuildStatData(object data)
    {
        Dictionary<int, stat_buildingRow> Data = data as Dictionary<int, stat_buildingRow>;
        if (allBulidData == null)
        {
            allBulidData = new Dictionary<int, stat_buildingRow>();
        }
        foreach (stat_buildingRow build in Data.Values)
        {
            if (!allBulidData.ContainsKey(build.id))
            {
                allBulidData.Add(build.id, build);
            }

        }
        SetEmployeeNameFromBuilding();

    }


    /// <summary>
    /// 刷新拥有蓝图的蓝图3D列表
    /// </summary>
    void RefreshGoodsList()
    {
        Debug.Log("refresh");
        for (int i = 0; i < 6; i++)
        {

           // GameObjectUtils.SetActiveRecursively(GoodsParents[i], false);
            GoodsParents[i].transform.DestroyChildren();
        }

        //所拥有蓝图个数
        int len = bluePrintIdList == null ? 0 : bluePrintIdList.Length;

        for (int i = 0; i < 6; i++)
        {

            if (point + i < len)
            {
                if (i == 0)
                {
                    recruitCatName.text =recruitTypeDic[ bluePrintIdList[0]];
                    buidingName.text = allBuildingBluepritData[bluePrintIdList[0]].name;
                    introText.text= allBuildingBluepritData[bluePrintIdList[0]].description;
                }
                int buildingModelId = bluePrintIdList[point + i];
                GameObject buildingModel = BulitPool.GetInstance().GetBulitPool(buildingModelId).CreateObject(Vector3.zero);
                buildingModel.AddComponent<ShopMenu3DItem>();             
                buildingModel.GetComponent<ShopMenu3DItem>().blueprintData = allBuildingBluepritData[buildingModelId];
                buildingModel.GetComponent<ShopMenu3DItem>().id = buildingModelId;
                buildingModel.GetComponent<BoxCollider>().isTrigger = true;
                buildingModel.transform.rotation = modelRef.transform.rotation;
                buildingModel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                buildingModel.transform.parent = GoodsParents[i].transform;
                buildingModel.layer = shopMenuLayer;
               // GameObjectUtils.SetLayerRecursively(buildingModel, shopMenuLayer);
               buildingModel.transform.SetChildLayer(shopMenuLayer);
                buildingModel.transform.localPosition = Vector3.zero;
                //  OwnBluePrintList[i].SetActive(true);
            }
            else
            {
                for (; i < 6; i++)
                {
                    //OwnBluePrintList[i].SetActive(false);
                    GoodsParents[i].transform.DestroyChildren();
                    //GameObjectUtils.SetActiveRecursively(GoodsParents[i], false);
                }
                break;
            }
        }
    }

    #endregion





    #region Unity回调

   protected  override void Awake()
    {
        base.Awake();

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;

        shopMenuLayer = LayerMask.NameToLayer("ShopMenuLayer");
        sortTypePoint = 0;
        point = 0;
        sortText = new string[] { "按字母顺序", "按1", "按2" };

        //sortInfos = new string[] { "按ID", "按1", "按2" };
        //recruitTypeDic = new Dictionary<int, string>();
        //LandedModelLayer = LayerMask.NameToLayer("LandedEstateLayer");
        leftBtn.onClick.AddListener(LeftBtnOn);
        rightBtn.onClick.AddListener(RightBtnOn);
        cancelBtn.onClick.AddListener(CancelBtnOn);
        //TearDownBtn.onClick.AddListener(TearDownBtnOn);
        tipWindowSureBtn.onClick.AddListener(TipWindowSureBtnOn);
        tipWindowCloseBtn.onClick.AddListener(TipWindowCloseBtnOn);
        sortChangeBtn.onClick.AddListener(ChangeSortTypeBtnOn);
    }

    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {
		
	}


    #endregion



    #region 事件回调


    public void LeftBtnOn()
    {

        if (bluePrintIdList != null)
        {
            if (point - 6 >= 0)
            {
                point -= 6;
            }
            else
            {
                return;
            }
            RefreshGoodsList();
        }
    }
    public void RightBtnOn()
    {
        if (bluePrintIdList != null)
        {
           
            if (point + 6 < bluePrintIdList.Length)
            {
                point += 6;
            }
            else
            {
                return;
            }
            RefreshGoodsList();
        }

    }

    public void CancelBtnOn()
    {
        IsInvoke = false;
        tipWindow.SetActive(false);
       
    }


    public void TipWindowCloseBtnOn()
    {
        resetTipWinPosi();
        tipWindow.SetActive(false);

    }

    public void TipWindowSureBtnOn()
    {
        Debug.Log("buy");
        UserInfoProxy userInfoProxy = AppFacade.getInstance.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
        string blueprint = userInfoProxy.UsertData.Blueprint;
        if (blueprint != "")
        {
            blueprint += "," + buildingSelectItem;
            userInfoProxy.setBlueprintsbudin(blueprint);
        }
        else
        {
            blueprint +=  buildingSelectItem;
            userInfoProxy.setBlueprintsbudin(blueprint);
        }


        int gold = userInfoProxy.UsertData.Gold;

        gold -= allBuildingBluepritData[buildingSelectItem].cost_coin;
        userInfoProxy.SetGold(gold);


        resetTipWinPosi();
        tipWindow.SetActive(false);
    }


    public void ChangeSortTypeBtnOn()
    {
        sortTypePoint++;
        if (sortTypePoint < sortText.Length)
        {
            sortTypeText.text = sortText[sortTypePoint];
        }
        else
        {
            sortTypePoint = 0;
            sortTypeText.text = sortText[sortTypePoint];
        }

    }
    #endregion
}
