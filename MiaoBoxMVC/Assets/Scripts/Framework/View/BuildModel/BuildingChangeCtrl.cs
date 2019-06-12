using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;



/*
 * 
 * 
 * 
 * 类名：BuildingChangeCtrl
 * 
 * 
 * 
 * 作用：控制模型切换和更新加载的方法
 * 
 * 日期 2017/1/6
 * */
public class BuildingChangeCtrl : MonoBehaviour
{
    //  public GameObject UIButton;

    private bool isbuilt { get; set; }                                                           //是否建立
    private int ModelTrsID;                                                                      //建筑的位置ID

    public GameObject[] Model;                                                                   //加载模型对象
    private Dictionary<int, Transform> Buildspts = new Dictionary<int, Transform>();             //模型位置字典
    private Dictionary<int, GameObject> tempmodel = new Dictionary<int, GameObject>();
    private Dictionary<int, BuildModelVo> BuildModelVoDic = null;

    //private Dictionary<int, stat_buildingspRow> statbuildingspdata = new Dictionary<int, stat_buildingspRow>();

    void Start()
    {
        Init();
        getfoodBowIdBuiltdata();

    }

    /// <summary>
    /// 初始化 将foodbow的位置与模型对应
    /// </summary>
    public void Init()
    {
        foreach (GameObject temp in Model)
        {

            BuildingBluepointCtrl cl = temp.GetComponent<BuildingBluepointCtrl>();
            Debug.Log("showinit");
            ModelTrsID = cl.modeltrsID;
            Transform ts = temp.transform;
            if (!Buildspts.ContainsKey(ModelTrsID))
            {
                Buildspts.Add(ModelTrsID, ts);
            }
        }
    }

    /// <summary>
    /// 得到模型数据
    /// </summary>
    void getfoodBowIdBuiltdata()
    {

        AppFacade.getInstance.SendNotification(NotiConst.GET_BUILDING_MODEL_DATA);
    }

    public void showinitmodel(object data)
    {
        BuildModelVoDic = data as Dictionary<int, BuildModelVo>;
        foreach (GameObject temp in Model)
        {
            BuildingBluepointCtrl cl = temp.GetComponent<BuildingBluepointCtrl>();
            if (BuildModelVoDic.ContainsKey(cl.modeltrsID))
            {
                InitBuilt(BuildModelVoDic[cl.modeltrsID], cl.foodBowID);
            }
        }


    }

    /// <summary>
    /// 加载时更新模型
    /// </summary>
    /// <param name="foodbowid"></param>
    public void InitBuilt(BuildModelVo vo, int foodbow)
    {
        if (tempmodel != null && !tempmodel.ContainsKey(vo.ModeltrsId))
        {
            GameObject building = BulitPool.mInstance.GetBulitPool(vo.Modelid).CreateObject(Buildspts[vo.ModeltrsId].position, Vector3.zero, Buildspts[vo.ModeltrsId].localRotation);
            Debug.Log(building);
            if (vo.ModeltrsId < 5)
            {


                BuildingBluepointCtrl bl = building.AddComponent<BuildingBluepointCtrl>();
                bl.modeltrsID = vo.ModeltrsId;
                bl.isBuild = vo.Modelid != 0 ? true : false;
                bl.foodBowID = foodbow;

                bl.modelID = vo.Modelid;
            }
            tempmodel.Add(vo.ModeltrsId, building);
        }
    }
    /// <summary>
    /// 建造模型
    /// </summary>
    /// <param name="vo"></param>
    public void ChangeBulit(BuildModelVo vo)
    {
        Debug.Log(vo.Modelid + "  " + vo.ModeltrsId);
        if (tempmodel.ContainsKey(vo.ModeltrsId))
        {
            tempmodel[vo.ModeltrsId].SetActive(false);

            GameObject building = BulitPool.mInstance.GetBulitPool(vo.Modelid).CreateObject(Buildspts[vo.ModeltrsId].position, Vector3.zero, Buildspts[vo.ModeltrsId].localRotation);
            if (vo.ModeltrsId < 5)
            {
                BuildingBluepointCtrl bl = building.GetComponent<BuildingBluepointCtrl>();
                if (bl == null) bl = building.AddComponent<BuildingBluepointCtrl>();
                bl.modeltrsID = vo.ModeltrsId;
                bl.isBuild = vo.Modelid != 0 ? true : false;
                bl.foodBowID = vo.foodbowid;
                bl.modelID = vo.Modelid;
            }
            tempmodel[vo.ModeltrsId] = building;
        }
    }

    private void setmodelfalse(BuildModelVo vo)
    {
        foreach (GameObject temp in Model)
        {
            BuildingBluepointCtrl cl = temp.GetComponent<BuildingBluepointCtrl>();
            if (cl.modeltrsID == vo.ModeltrsId)
            {

                temp.SetActive(false);
            }
        }

    }

    public void OnDisable()
    {
        Model = null;
    }
}