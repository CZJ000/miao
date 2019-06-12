using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using Global;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using SUIFW;


public delegate void VoidDelegate();
public delegate void OneArgDelegate(object arg);
/// <summary>
/// 地产信息Mono视图，从BuildingBlueprintMenu分离出来，共享BlueprintMeditor 和BlueprintProxy
/// </summary>
/// 
public class LandedEstateMenuView : BaseUIForm {




    int LandedModelLayer;



    /// <summary>
    /// 当前选择的建筑,作为rotation 的参照
    /// </summary>
    public GameObject SelectBuildingModel;


    /// <summary>
    /// 自己所拥有的建筑蓝图
    /// </summary>
    public GameObject[] OwnBluePrintList;

    public GameObject BuildPoint;
    public GameObject ChaneModelTipWindow;


    public Button LeftBtn;
    public Button RightBtn;
    public Button CancelBtn;
    public Button TearDownBtn;
    public Button TipWindowCloseBtn;
    public Button SureChangeModelBtn;
    public Button ChangeSortTypeBtn;

    public Text BuildingInfo;
    public Text employeeInfo;
    public Text buildName;
    public Text sortTypeText;


    public GameObject textParent;

    public string[] sortInfos;


    public VoidDelegate resetTipWinPosi;

    public VoidDelegate cancelChangeModel;

    public VoidDelegate tearDownBuilding;

    /// <summary>
    /// 建筑蓝图静态数据 用Tag值进行分开， int--蓝图类型：建筑、设施等等
    /// </summary>
    private Dictionary<int, List<stat_blueprintRow>> buildingBluepritData = new Dictionary<int, List<stat_blueprintRow>>();


    /// <summary>
    /// int :id    string: 可雇佣猫的名字；
    /// </summary>
    private Dictionary<int, string> recruitTypeDic;


    /// <summary>
    /// 所有的建筑蓝图数据，用建筑ID存储。方便查找  
    /// </summary>
    private Dictionary<int, stat_blueprintRow> allBuildingBluepritData = null;


    /// <summary>
    /// 模型地理位置信息
    /// </summary>
    public int modeltrsid { get; set; }


    /// <summary>
    /// 猫粮的ID 用于区分展示建筑的信息
    /// </summary>
    public int foodBowID;

    /// <summary>
    /// 建筑数据 
   // from-- BulidBluePrintProxy
    /// </summary>
    private Dictionary<int, stat_buildingRow> allBulidData = new Dictionary<int, stat_buildingRow>();





    /// <summary>
    /// 建筑信息是单选，只要存储一个就行
    /// </summary>
    public static int buildingSelectItem;



    /// <summary>
    /// 翻页已有蓝图指针，指向左边第一个
    /// </summary>
    int point;


    int sortTypePoint=0;


    stat_buildingRow selectedBuildInfo;


    /// <summary>
    /// 用户拥有的建筑蓝图,"id"
    /// </summary>
    string[] buildingData;


    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {

            //  Camera.main.GetComponent<UICamera>().eventType = UICamera.EventType.UI_2D;
          //  return transform.localScale != Vector3.zero;
            return gameObject.activeSelf;
        }
        set
        {
            //  transform.localScale = value ? Vector3.one: Vector3.zero;
            if (value == false)
            {
                CloseUIForm();
            }
            else
            {
                if(!gameObject.activeSelf)
                OpenUIForm("LandedEstateMenuView");
            }
            //if (transform.localScale == Vector3.one)
            //{
            //    MainMenuView.Instance.blockPanel.SetActive(true);
            //}
            //else
            //{
            //    MainMenuView.Instance.blockPanel.SetActive(false);
            //}

        }
    }




    /// <summary>
    /// 建筑是否已经建立
    /// </summary>
    public bool isBulid
    {
        get;
        set;
    }


    private void Awake()
    {
        sortInfos = new string[] { "按ID", "按1", "按2" };

        LandedModelLayer = LayerMask.NameToLayer("LandedEstateLayer");
        recruitTypeDic = new Dictionary<int, string>();

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }



    private void Start()
    {
        
        LeftBtn.onClick.AddListener(LeftBtnOn);
        RightBtn.onClick.AddListener(RightBtnOn);
        CancelBtn.onClick.AddListener(CancelBtnOn);
        TearDownBtn.onClick.AddListener(TearDownBtnOn);
        SureChangeModelBtn.onClick.AddListener(SureChangeModelBtnOn);
        TipWindowCloseBtn.onClick.AddListener(TipWindowCloseBtnOn);
        ChangeSortTypeBtn.onClick.AddListener(ChangeSortTypeBtnOn);


    }


    /// <summary>
    /// 设置蓝图的静态数据，在启动的时候读取
    /// </summary>
    /// <param name="data">Data.</param>
    public void setBuildingBlueprintData(object data)
    {
      
        allBuildingBluepritData = data as Dictionary<int, stat_blueprintRow>;
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

    }









    /// <summary>
    /// 获取建筑的信息，在获取信息之后展示数据
    /// </summary>
    public void getBuildingData()
    {
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDINGBLUEPRINT_DATA);
        AppFacade.GetInstance().SendNotification(NotiConst.GET_STATBUILD_DATA);
    }

    /// <summary>
    /// 打开地产信息视图,点击地产的调用
    /// </summary>
    /// <param name="foodbowId">地理位置ID</param>
    /// <param name="modelId">该位置上的建筑ID</param>
    public void ShowLandedEstateView(int foodbowId, int modelId)
    {
        getBuildingData();


        Debug.Log(modelId);
        if (modelId != 0)
        {
            selectedBuildInfo = allBulidData[modelId];
            buildingSelectItem = selectedBuildInfo.id;
        }
        else
        {
            buildingSelectItem = 0;
        }
        this.foodBowID = foodbowId;

        //IsInvoke = true;


        //拿到用户拥有的建筑蓝图data，对应setBulidingData方法
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDING_DATA);


        //显示 建筑信息
        showBuildingDataItem();
    }

    /// <summary>
    /// 设置建筑信息，每次打开建筑界面都需要读取（为了保证数据的一致性）
    /// </summary>
    /// <param name="data">Data.</param>
    public void setBuildingData(string data)
    {
        Debug.Log(data);
        SetEmployeeNameFromBuilding();
        if (data == null)
        {
            // removeAllGridChildren();
            buildingData = null;
          

        }
        else
        {
            buildingData = data.Split(new char[] { ',' });
        }
     
        //        buildingType = BuildingBlueprintType.Building;
       

    }


    void SetEmployeeNameFromBuilding()
    {
      
           List<stat_catRow> list= stat_cat.GetInstance().rowList;
        //for (int i=0;i<allBulidData.Count;i++)
        //{
        //    int id = int.Parse(buildingData[i]);
        //    int recruitId = int.Parse(allBulidData[id].recruittype);
        //    recruitTypeDic[id]= list[recruitId - 2].name;
        //}
        Debug.Log(list);
        Debug.Log(recruitTypeDic);
        foreach (int i in allBulidData.Keys)
            {
                int recruitId = int.Parse(allBulidData[i].recruittype);
                recruitTypeDic[i] = list[recruitId - 2].name;

            }
        
        

    }



    public void Click3DItemChangeInfo(int showIndex)
    {

        RefreshSelectModelUIInfo(showIndex);

    }



    /// <summary>
    /// 显示所有的建筑信息
    /// </summary>
    private void showBuildingDataItem()
    {

       
        Debug.Log("show");
        point = 0;
       // GameObjectUtils.SetActiveRecursively(SelectBuildingModel,false );
        SelectBuildingModel.transform.DestroyChildren();
        GameObject selectBuildingModel = BulitPool.GetInstance().GetBulitPool(buildingSelectItem).CreateObject(Vector3.zero);
        selectBuildingModel.transform.rotation = SelectBuildingModel.transform.rotation;
        selectBuildingModel.transform.localScale = new Vector3(1f, 1f, 1f);

        selectBuildingModel.transform.parent = SelectBuildingModel.transform;
        selectBuildingModel.transform.localPosition = Vector3.zero;
        selectBuildingModel.transform.tag = TagName.SELECTBUILDING3DUI;

        selectBuildingModel.layer = LandedModelLayer;
        selectBuildingModel.transform.SetChildLayer(LandedModelLayer);
        //GameObjectUtils.SetLayerRecursively(selectBuildingModel, LandedModelLayer);
       
        selectBuildingModel.AddComponent<Rigidbody>();
        selectBuildingModel.GetComponent<Rigidbody>().useGravity = false; 

        if (buildingSelectItem != 0)
        {
            selectBuildingModel.AddComponent<SelectBuilding3DItem>().buildingId = buildingSelectItem;
            
            BuildingInfo.text = allBulidData[buildingSelectItem].description2;
            employeeInfo.text = recruitTypeDic[buildingSelectItem];
            buildName.text = allBulidData[buildingSelectItem].name;
        }
        else
        {
            textParent.SetActive(false);
            selectBuildingModel.transform.localScale = new Vector3(110 , 110, 110);
            selectBuildingModel.transform.localRotation = Quaternion.Euler(new Vector3(0, -100, 0));
            selectBuildingModel.transform.GetComponent<BoxCollider>().size = Vector3.one;
            selectBuildingModel.transform.localPosition=new Vector3(-15.99984f,- 3.000267f,- 3.000339f);
           
        }
       

        

        RefreshOwnBluePrintList();




    }


   


    /// <summary>
    /// 刷新当前选择UIModel 的信息， 建筑说明，雇员信息等等
    /// </summary>
    void RefreshSelectModelUIInfo(int index)
    {
        if (index == 0)
        {
            textParent.SetActive(false);
        }
        else
        {
            textParent.SetActive(true);
        }
       
        BuildingInfo.text = allBulidData[index].description2;
        employeeInfo.text = recruitTypeDic[index];
        buildName.text = allBulidData[index].name;

    }


    /// <summary>
    /// 刷新拥有蓝图的蓝图3D列表
    /// </summary>
    void RefreshOwnBluePrintList()
    {
        //拿到用户拥有的建筑蓝图data，对应setBulidingData方法
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDING_DATA);

        Debug.Log("refresh");
        for (int i = 0; i < 3; i++)
        {

            //GameObjectUtils.SetActiveRecursively(OwnBluePrintList[i], false);
            OwnBluePrintList[i].transform.DestroyChildren();
        }

        //所拥有蓝图个数
        int len = buildingData==null ? 0 : buildingData.Length;

        for (int i = 0; i < 3; i++)
        {

            if (point+i< len)
            {                         
                int buildingModelId = int.Parse(buildingData[point+i]);
                GameObject buildingModel = BulitPool.GetInstance().GetBulitPool(buildingModelId).CreateObject(Vector3.zero);
                buildingModel.AddComponent<BuildingModel3DItem>();
                buildingModel.GetComponent<BuildingModel3DItem>().bulitData = allBulidData[buildingModelId]; //.
                buildingModel.GetComponent<BuildingModel3DItem>().blueprintData = allBuildingBluepritData[buildingModelId];
                buildingModel.GetComponent<BuildingModel3DItem>().buildingID = allBulidData[buildingModelId].id;
                buildingModel.GetComponent<BoxCollider>().isTrigger = true;
               
                buildingModel.transform.rotation = SelectBuildingModel.transform.rotation;
                buildingModel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                buildingModel.transform.parent = OwnBluePrintList[i].transform;        
                buildingModel.layer = LandedModelLayer;
               // GameObjectUtils.SetLayerRecursively(buildingModel, LandedModelLayer);
                buildingModel.transform.SetChildLayer(LandedModelLayer);
                buildingModel.transform.localPosition = Vector3.zero;
              //  OwnBluePrintList[i].SetActive(true);
            }
            else
            {
                for (; i < 3; i++)
                {
                    //OwnBluePrintList[i].SetActive(false);
                    //GameObjectUtils.SetActiveRecursively(OwnBluePrintList[i], false);
                    OwnBluePrintList[i].transform.DestroyChildren();
                }
                break;
            }
        }
    }

#region 事件回调

    public void LeftBtnOn()
    {
       
        if (buildingData != null)
        {
            if (point - 3 >= 0)
            {
                point -= 3;
            }
            else
            {
                return;
            }
            RefreshOwnBluePrintList();
        }
    }
    public void RightBtnOn()
    {
        if (buildingData != null)
        {
            Debug.Log(buildingData.Length);
            if (point + 3 < buildingData.Length)
            {
                point += 3;
            }
            else
            {
                return;
            }
            RefreshOwnBluePrintList();
        }
       
    }
    public void CancelBtnOn()
    {
        Debug.Log("close");
      
        IsInvoke = false;
        ChaneModelTipWindow.SetActive(false);

    }



    public void TearDownBtnOn()
    {

        BuildModelVo modelVo = new BuildModelVo
        {

            //foodBowID modeltrsid  在点击建筑的时候设置
            foodbowid = foodBowID,
            ModeltrsId = modeltrsid,
            //在点击拖动3D UI 的时候设置；
            Modelid = 0
        };
        tearDownBuilding();
        buildingSelectItem = 0;
      

        //设置建筑模型数据----写入数据库
        AppFacade.getInstance.SendNotification(NotiConst.SET_BUILDING_MODEL_DATA, modelVo);

        //更换3D模型
        AppFacade.getInstance.SendNotification(NotiConst.GET_CHNAGE_MODEL_DATA, modelVo);

        //拿到用户拥有的建筑蓝图data，对应setBulidingData方法
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDING_DATA);
        showBuildingDataItem();

    }


    public void TipWindowCloseBtnOn()
    {
        cancelChangeModel();
        resetTipWinPosi();
        ChaneModelTipWindow.SetActive(false);

    }

    public void SureChangeModelBtnOn()
    {
        BuildModelVo modelVo = new BuildModelVo
        {

            //foodBowID modeltrsid  在点击建筑的时候设置
            foodbowid = foodBowID,
            ModeltrsId = modeltrsid,
            //在点击拖动3D UI 的时候设置；
            Modelid = buildingSelectItem
        };

        resetTipWinPosi();
       
        //设置建筑模型数据----写入数据库
        AppFacade.getInstance.SendNotification(NotiConst.SET_BUILDING_MODEL_DATA, modelVo);
        
        //更换3D模型
        AppFacade.getInstance.SendNotification(NotiConst.GET_CHNAGE_MODEL_DATA, modelVo);

        ChaneModelTipWindow.gameObject.SetActive(false);

      

        //拿到用户拥有的建筑蓝图data，对应setBulidingData方法
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BUILDING_DATA);
        showBuildingDataItem();

     
    }

    public void ChangeSortTypeBtnOn()
    {
        sortTypePoint++;
        if (sortTypePoint < sortInfos.Length)
        {
            sortTypeText.text = sortInfos[sortTypePoint];          
        }
        else
        {
            sortTypePoint = 0;
            sortTypeText.text = sortInfos[sortTypePoint];
        }
     
    }

#endregion
}
