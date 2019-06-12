using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 在猫控制面板的最下面的控制器，单例
/// </summary>
public class CatGroupBottomControler : MonoBehaviour
{
    
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CatGroupBottomControler Instance;
    /// <summary>
    /// 是否找齐了所有组件，并添加各种事件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 所有的猫数据的按钮；
    /// </summary>
    private List<CatInfoCopy> ListAllCatInfoCopy = new List<CatInfoCopy>();
    /// <summary>
    /// 所有的猫信息，挂载到猫的显示框上；
    /// </summary>
    private List<CatInfo> ListAllCatInfo = new List<CatInfo>();
    /// <summary>
    /// 当前开始的下标；
    /// </summary>
    private int _NowStartIndex = 0;
    /// <summary>
    /// 当前开始的下标；
    /// </summary>
    public int NowStartIndex
    {
        get
        {
            return _NowStartIndex;
        }

        set
        {
            //下标向后越界；
            if (value >= ListAllCatInfoCopy.Count )
            {
                value =  value - ListAllCatInfoCopy.Count;
            }
            //下标向前越界；
            if (value < 0)
            {
                value = ListAllCatInfoCopy.Count + value;
            }
            _NowStartIndex = value;
        }
    }

    #region 组件

    /// <summary>
    /// 向左的按钮；
    /// </summary>
    public Button LeftButton;
    /// <summary>
    /// 向右的按钮；
    /// </summary>
    public Button RightButton;
    /// <summary>
    /// 排序方法按钮；
    /// </summary>
    public Button SortButton;
    /// <summary>
    /// 排列的文本；
    /// </summary>
    public Text SortText;
    /// <summary>
    /// 猫分组的容器；
    /// </summary>
    public Transform CatInfoContent;

    #endregion

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        StartGetAllCompoenets();
        
    }

    /// <summary>
    /// 清空猫分组信息；
    /// </summary>
    public void ClearCatInfoList()
    {
        ListAllCatInfoCopy.Clear();
    }

    /// <summary>
    /// 添加毛信息；
    /// </summary>
    /// <param name="cic"></param>
    public void AddCatInfoCopy(CatInfoCopy cic)
    {
        ListAllCatInfoCopy.Add(cic);
    }

    /// <summary>
    /// 刷新；
    /// </summary>
    public void Refresh()
    {
        List<CatInfoCopy> ListCatsNeedShow;
        //设置根据当前排列方式需要显示的猫分组；
        if (IsOnlyLeader)
        {
            //如果是队长，需要重新进行一遍计算；
            SortMethos_OnlyLeader();
            ListCatsNeedShow = ListOnlyLeaderCat;
        }
        else
        {
            ListCatsNeedShow = ListAllCatInfoCopy;
        }
        //从当前下标取值；
        for (int i = 0; i < ListAllCatInfo.Count; i++)
        {
            //计算出正确的下标；
            int nowIndex = i + NowStartIndex;
            //当有足够数量（超过4个）的猫用来显示的时候；
            if (ListCatsNeedShow.Count > ListAllCatInfo.Count)
            {
                {
                    while (nowIndex >= ListCatsNeedShow.Count)
                    {
                        nowIndex = nowIndex - ListCatsNeedShow.Count;
                    }
                    //赋值；
                    CatGroupContoler.Instance.CopyCatInfo(ListCatsNeedShow[nowIndex], ListAllCatInfo[i]);
                    //刷新；
                    ListAllCatInfo[i].Refresh();
                }
            }
            else
            {
                if (i < ListCatsNeedShow.Count)
                {
                    //赋值；
                    CatGroupContoler.Instance.CopyCatInfo(ListCatsNeedShow[i], ListAllCatInfo[i]);
                    //刷新；
                    ListAllCatInfo[i].Refresh();
                }
                else
                {
                    //赋值；
                    ListAllCatInfo[i].CatTypeid = -1;
                    ListAllCatInfo[i].Attribute = "null";
                    //刷新；
                    ListAllCatInfo[i].Refresh();
                }
            }
            Debug.Log("底部区域已刷新" + ListCatsNeedShow.Count);
        }
    }






    /// <summary>
    /// 排列方法的名称；
    /// </summary>
    string[] SortMehtosName = { "按照属性", "按照Power值", "只显示队长" };
    /// <summary>
    /// 当前的排列方法；
    /// </summary>
    private int NowSortMethodIndex = 0;
    /// <summary>
    /// 是否只显示队长；
    /// </summary>
    private bool IsOnlyLeader = false;
    /// <summary>
    /// 只包含队长的猫信息；
    /// </summary>
    private List<CatInfoCopy> ListOnlyLeaderCat = new List<CatInfoCopy>();

    /// <summary>
    /// 执行下一个排序方法；
    /// </summary>
    void NextSort()
    {
        //设定下标从0~2循环；
        NowSortMethodIndex++;
        if (NowSortMethodIndex >= SortMehtosName.Length) NowSortMethodIndex = 0;
        //开始排序；
        IsOnlyLeader = false;
        if (NowSortMethodIndex == 0)
        {
            //执行排序方法：按照属性值排序；
            SortMethod_ByAttribute();
        }
        else if (NowSortMethodIndex == 1)
        {
            //执行排序方法：按Power值排序；
            SortMethos_ByPowerPoint();
        }
        else if (NowSortMethodIndex == 2)
        {
            //执行排序方法：只显示队长；
            SortMethos_OnlyLeader();
        }
        //刷新；
        Refresh();
        //排序完毕后修改文本；
        SortText.text = SortMehtosName[NowSortMethodIndex];
    }

    /// <summary>
    /// 按照属性方法重新排列；
    /// 采用桶排序方法；
    /// </summary>
    void SortMethod_ByAttribute()
    {
        //属性一共有5种属性；
        List<CatInfoCopy>[] ArrayCatsInfo = new List<CatInfoCopy>[5];
        //赋初始值；
        for (int i = 0; i < ArrayCatsInfo.Length; i++)
        {
            ArrayCatsInfo[i] = new List<CatInfoCopy>();
        }
        //开始排序；
        for (int i = 0; i < ListAllCatInfoCopy.Count; i++)
        {
            switch (ListAllCatInfoCopy[i].Attribute)
            {
                //白
                case "w":
                    ArrayCatsInfo[0].Add(ListAllCatInfoCopy[i]);
                    break;
                //紫
                case "p":
                    ArrayCatsInfo[1].Add(ListAllCatInfoCopy[i]);
                    break;
                //红
                case "r":
                    ArrayCatsInfo[2].Add(ListAllCatInfoCopy[i]);
                    break;
                //绿
                case "g":
                    ArrayCatsInfo[3].Add(ListAllCatInfoCopy[i]);
                    break;
                //蓝
                case "b":
                    ArrayCatsInfo[4].Add(ListAllCatInfoCopy[i]);
                    break;
                //如果以上皆不是
                default:
                    ArrayCatsInfo[4].Add(ListAllCatInfoCopy[i]);
                    break;
            }
        }
        //清空原来列表；
        ListAllCatInfoCopy.Clear();
        //重新添加猫分组；
        for (int i = 0; i < ArrayCatsInfo.Length; i++)
        {
            for (int j = 0; j < ArrayCatsInfo[i].Count; j++)
            {
                ListAllCatInfoCopy.Add(ArrayCatsInfo[i][j]);
            }
        }
    }

    /// <summary>
    /// 按Power值排序；
    /// 采用快速排序方法；
    /// </summary>
    void SortMethos_ByPowerPoint()
    {
        //小于等于1个猫就没什么好排序的了；
        if (ListAllCatInfoCopy.Count <= 1) return;
        quicksort(0, ListAllCatInfoCopy.Count - 1);
    }

    /// <summary>
    /// 快速排序方法；
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    void quicksort(int left, int right)
    {
        CatInfoCopy TempCatInfoCopy, temp;
        int i, j;
        if (left > right)
            return;

        temp = ListAllCatInfoCopy[left]; //temp中存的就是基准数 
        i = left;
        j = right;
        while (i != j)
        {
            //顺序很重要，要先从右边开始找 
            while (ListAllCatInfoCopy[j].Power >= temp.Power && i < j)
                j--;
            //再找右边的 
            while (ListAllCatInfoCopy[i].Power <= temp.Power && i < j)
                i++;
            //交换两个数在数组中的位置 
            if (i < j)
            {
                TempCatInfoCopy = ListAllCatInfoCopy[i];
                ListAllCatInfoCopy[i] = ListAllCatInfoCopy[j];
                ListAllCatInfoCopy[j] = TempCatInfoCopy;
            }
        }
        //最终将基准数归位 
        ListAllCatInfoCopy[left] = ListAllCatInfoCopy[i];
        ListAllCatInfoCopy[i] = temp;

        quicksort(left, i - 1);//继续处理左边的，这里是一个递归的过程 
        quicksort(i + 1, right);//继续处理右边的 ，这里是一个递归的过程 
    }

    /// <summary>
    /// 只显示队长；
    /// </summary>
    void SortMethos_OnlyLeader()
    {
        IsOnlyLeader = true;
        //将所有的队长猫取出来并保存；
        ListOnlyLeaderCat.Clear();
        for (int i = 0; i < ListAllCatInfoCopy.Count; i++)
        {
            if (ListAllCatInfoCopy[i].CatCaptainTypeid == 1)
            {
                ListOnlyLeaderCat.Add(ListAllCatInfoCopy[i]);
            }
        }
    }

    /// <summary>
    /// 获取所有组件的方法；
    /// </summary>
    public void StartGetAllCompoenets()
    {
        //该方法只执行一次；
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        //获得4个底部猫信息显示框；
        CatInfo[] cis = CatInfoContent. GetComponentsInChildren<CatInfo>();
        for (int i = 0; i < cis.Length; i++)
        {
            ListAllCatInfo.Add(cis[i]);
        }
        Debug.Log("获取猫分组："+cis.Length);
        //按钮点击事件；
        LeftButton.onClick.AddListener(delegate ()
        {
            NowStartIndex -= 4;
            Refresh();
        });
        RightButton.onClick.AddListener(delegate ()
        {
            NowStartIndex += 4;
            Refresh();
        });
        //底部猫的排列方法；
        SortButton.onClick.AddListener(delegate ()
        {
            NextSort();
        });
    }
}
