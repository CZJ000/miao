using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 猫分组中间面板的控制器；单例类；
/// </summary>
public class CatGroupCenterControler : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CatGroupCenterControler Instance;
    /// <summary>
    /// 是否获得过所有组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 猫分组
    /// </summary>
    private CatInfo[,] CatGroup = new CatInfo[3,5];
    /// <summary>
    /// 所有的猫信息；
    /// </summary>
    private CatInfoCopy[,] CatGroupCopy = new CatInfoCopy[3, 5];
    /// <summary>
    /// 猫训练组的猫信息；
    /// </summary>
    private CatInfoCopy[] CatCampCopy = new CatInfoCopy[8];
    /// <summary>
    /// 猫训练营
    /// </summary>
    private CatInfo[] CatCamp = new CatInfo[8];
    /// <summary>
    /// 当前应该显示的页面下标；
    /// </summary>
    private int NowShowPanelIndex = 0;
    /// <summary>
    /// 返回当前显示的猫分组；
    /// </summary>
    public int NowShowGroupIndex
    {
        get
        {
            return NowShowPanelIndex + 1;
        }
    }

    #region 组件

    public Button LeftButton;

    public Button RightButton;
    /// <summary>
    /// 所有需要控制的面板；
    /// </summary>
    public List<Transform> ListAllPanels = new List<Transform>();

    /// <summary>
    /// 各个分组面板的名字；
    /// </summary>
    public Text CatGroupMessageText;
        
    #endregion;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        GetAllCompoenents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 给前三个面板添加猫信息；
    /// </summary>
    /// <param name="i"></param>
    public void AddCatInfoToPanel(int i, List<CatInfoCopy> cats)
    {
        for (int j = 0; j < CatGroupCopy.GetLength(1); j++)
        {
            if (j < cats.Count)
            {
                CatGroupCopy[i, j] = cats[j];
            }
            else
            {
                CatGroupCopy[i, j] = null;
            }
        }
    }

    /// <summary>
    /// 给猫训练营面板添加猫；
    /// </summary>
    /// <param name="cats"></param>
    public void AddCatInfoToCamp(List<CatInfoCopy> cats)
    {
        for (int i = 0; i < CatCampCopy.Length; i++)
        {
            if (i < cats.Count)
            {
                CatCampCopy[i] = cats[i];
            }
            else
            {
                CatCampCopy[i] = null;
            }
        }
    }

    /// <summary>
    /// 通过分组ID来将所有的猫移动到底部；
    /// </summary>
    /// <param name="_gouptID"></param>
    public void RemoveAllMembersByGroupID(int _gouptID)
    {
        //将所有组员的CatInfoCopy取出来
        CatInfoCopy[] tempCopy = new CatInfoCopy[CatGroupCopy.GetLength(1)];
        for (int i = 0; i < tempCopy.Length; i++)
        {
            tempCopy[i] = CatGroupCopy[_gouptID - 1, i];
        }
        //移动到最下面；
        CatGroupContoler.Instance.MoveCatsToBottomGroup(tempCopy);
        //设置一个空的猫信息；
        CatInfoCopy EmptyCatInfoCopy = new CatInfoCopy();
        //将保存的所有猫信息清空；
        for (int i = 0; i < CatGroup.GetLength(1); i++)
        {
            CatGroupContoler.Instance.CopyCatInfo(EmptyCatInfoCopy, CatGroup[_gouptID - 1, i]);
        }
    }

    /// <summary>
    /// 刷新；
    /// </summary>
    public void Refresh()
    {
        //进行刷新；
        for (int i = 0; i < CatGroupCopy.GetLength(0); i++)
        {
            //猫分组信息；
            string[] ListCatSlots = new string[0];
            //进行队长猫查找，将队长猫放在第一个；
            for (int j = 0; j < CatGroupCopy.GetLength(1); j++)
            {
                if (CatGroupCopy[i, j] != null && CatGroupCopy[i, j].CatCaptainTypeid == 1)
                {
                    SwitchCatGroupCopy(i, 0, j);
                    //队长猫放第一个；
                    CatGroupContoler.Instance.CopyCatInfo(CatGroupCopy[i, 0], CatGroup[i, 0]);
                    //然后取出猫分组；
                    ListCatSlots = CatGroup[i, 0].MembersSlot.Split(',');
                    //Debug.Log("该组有成员数量：" + ListCatSlots.Length);
                    //Debug.Log(CatGroup[i, 0].CatTypeid + "队长猫的类型：" + CatGroup[i, 0].MembersSlot);
                    break;
                }
                //如果执行到这里，则是全队无队长的情况：
                CatGroup[i, 0].CatTypeid = -1;
                CatGroup[i, 0].Attribute = "null";
                CatGroup[i, 0].CatCaptainTypeid = -1;
            }
            //刷新队长猫；
            CatGroup[i, 0].Refresh(); 
            //从第2个开始判定，因为队长猫必然有显示； 
            for (int j = 1; j < CatGroup.GetLength(1); j++)
            {
                //队长猫有信息；
                if (CatGroup[i, 0].CatTypeid != -1 && CatGroupCopy[i, j] != null)
                {
                    //有队长猫，刷新猫信息；
                    CatGroup[i, j].Attribute = null;
                    CatGroupContoler.Instance.CopyCatInfo(CatGroupCopy[i, j], CatGroup[i, j]);
                    //显示出来；
                    CatGroup[i, j].transform.localScale = Vector3.one;
                    CatGroup[i, j].Refresh();
                    //取出现在的猫属性；
                    string atr = CatGroup[i, j].Attribute;
                    //在猫组中进行查找；
                    for (int k = 0; k < ListCatSlots.Length; k++)
                    {
                        if (string.IsNullOrEmpty(ListCatSlots[k])) continue;
                        if (ListCatSlots[k] == atr)
                        {
                            //每个找到的猫都将他剔除；
                            ListCatSlots[k] = null;
                            break;
                        }
                    }
                }
                //无队长猫；或者此处无猫值；
                else
                {
                    //无猫信息，则关闭之；
                    CatGroup[i, j].transform.localScale = Vector3.zero;
                    //查看是否有将空格占满；
                    for (int k = 0; k < ListCatSlots.Length; k++)
                    {
                        if (string.IsNullOrEmpty(ListCatSlots[k])) continue;
                        //此时还有空位，则将属性进行赋值；
                        CatGroup[i, j].Attribute = ListCatSlots[k];
                        //每个找到的猫都将他剔除；
                        //Debug.Log("当前展示过的猫属性：" + ListCatSlots[k]);
                        ListCatSlots[k] = null;
                        //显示空位；
                        CatGroup[i, j].transform.localScale = Vector3.one;
                        CatGroup[i, j].CatTypeid = -1;
                        CatGroup[i, j].Refresh();
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 刷新训练营的猫信息；
    /// </summary>
    public void RefreshCampMessage()
    {
        for (int i = 0; i < CatCamp.Length; i++)
        {
            if (CatCampCopy[i] != null)
            {
                //有猫信息则显示猫信息；
                CatGroupContoler.Instance.CopyCatInfo(CatCampCopy[i], CatCamp[i]);
            }
            else
            {
                CatCamp[i].CatTypeid = -1;
                CatCamp[i].Attribute = "null";
            }
            CatCamp[i].Refresh();
        }
    }

    /// <summary>
    /// 交换CatInfoCopy的两个数，按照下标进行交换；
    /// </summary>
    /// <param name="CatGroupID">第几组</param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    void SwitchCatGroupCopy(int CatGroupID,int a, int b)
    {
        CatInfoCopy tempCatInfo = CatGroupCopy[CatGroupID, a];
        CatGroupCopy[CatGroupID, a] = CatGroupCopy[CatGroupID, b];
        CatGroupCopy[CatGroupID, b] = tempCatInfo;
    }

    /// <summary>
    /// 点击向左按钮执行；
    /// </summary>
    void GoLeft()
    {
        //删除所有猫；
        //CatModelContorl.NeedRefreshAll(NowShowPanelIndex+1);
        //刷新一次；
        Refresh();
        //关闭之前的面板；
        ListAllPanels[NowShowPanelIndex].transform.localScale = Vector3.zero;
        //开新的面板；
        NowShowPanelIndex--;
        NowShowPanelIndex = Mathf.Max(0, NowShowPanelIndex);
        ListAllPanels[NowShowPanelIndex].transform.localScale = Vector3.one;
        //更改组名；
        CatGroupMessageText.text = GetPanelText();
        //Debug.Log("执行上一页：" + NowShowPanelIndex);
    }
    /// <summary>
    /// 点击向右按钮执行；
    /// </summary>
    void GoRight()
    {
        //删除所有猫；
        //CatModelContorl.NeedRefreshAll(NowShowPanelIndex+1);
        //刷新一次；
        Refresh();
        //关闭之前的面板；
        ListAllPanels[NowShowPanelIndex].transform.localScale = Vector3.zero;
        //开新的面板；
        NowShowPanelIndex++;
        NowShowPanelIndex = Mathf.Min(ListAllPanels.Count - 1, NowShowPanelIndex);
        ListAllPanels[NowShowPanelIndex].transform.localScale = Vector3.one;
        //更改组名；
        CatGroupMessageText.text = GetPanelText();
        //Debug.Log("执行下一页："+NowShowPanelIndex);
    }

    /// <summary>
    /// 根据面板下标返回当前面板的名字；
    /// 1~3组分别为：猫爪组，猫毛组和猫？组，然后是训练营和解雇猫
    /// </summary>
    /// <returns></returns>
    string GetPanelText()
    {
        string Mes = "猫爪组";
        switch (NowShowPanelIndex)
        {
            case 0:
                Mes = "猫爪组";
                break;
            case 1:
                Mes = "猫毛组";
                break;
            case 2:
                Mes = "猫？组";
                break;
            case 3:
                Mes = "训练营";
                break;
            case 4:
                Mes = "解雇猫";
                break;
        }
        return Mes;
    }

    /// <summary>
    /// 获取所有组件的方法；
    /// </summary>
    public void GetAllCompoenents()
    {
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        //获取所有组件：
        LeftButton.onClick.AddListener(GoLeft);
        RightButton.onClick.AddListener(GoRight);
        //获得所有猫分组；
        //接下来将分组保存；
        for (int i = 0; i < CatGroup.GetLength(0); i++)
        {
            CatInfo[] TempCat = ListAllPanels[i].GetComponentsInChildren<CatInfo>();
            for (int j = 0; j < CatGroup.GetLength(1); j++)
            {
                CatGroup[i, j] = TempCat[j];
            }
        }
        CatInfo[] TempCats = ListAllPanels[3].GetComponentsInChildren<CatInfo>();
        for (int i = 0; i < TempCats.Length; i++)
        {
            CatCamp[i] = TempCats[i];
        }
    }

}
