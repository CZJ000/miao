using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatModelContorl : MonoBehaviour
{
    /// <summary>
    /// 静态列表，用于存储所有的猫模型；
    /// </summary>
    static List<CatModelContorl> StaticListAllCatModels = new List<CatModelContorl>();
    

    public static void NeedRefreshAll(int _groupID)
    {
        Debug.Log("删除猫分组"+_groupID);
        List<CatModelContorl> TempList = new List<CatModelContorl>();
        foreach (CatModelContorl i in StaticListAllCatModels)
        {
            Debug.Log("查看一下猫分组：" + i.CatGroupID + "名字：" + i.gameObject.name);
            if (i.CatGroupID == _groupID)
            {
                TempList.Add(i);
            }
        }
        Debug.Log("总共删除："+TempList.Count);
        for (int i = 0; i < TempList.Count; i++)
        {
            StaticListAllCatModels.Remove(TempList[i]);
            Destroy(TempList[i]);
        }
    }

    public int CatGroupID = -1;
    
    // Use this for initialization
    void Start()
    {
        
    }

    /// <summary>
    /// 保存进列表；
    /// </summary>
    public void SetToList()
    {
        StaticListAllCatModels.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        Debug.Log("disable");
    }

    public void DestroyThis()
    {
        Debug.Log("开始删除"+gameObject.name);
        Destroy(gameObject);
    }
}
