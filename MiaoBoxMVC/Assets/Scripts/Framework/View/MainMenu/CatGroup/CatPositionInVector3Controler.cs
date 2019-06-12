using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI控制器的静态单例类；
/// 控制的UI：
/// </summary>
public class CatPositionInVector3Controler : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CatPositionInVector3Controler Instance;
    /// <summary>
    /// 每个Vector3需要便宜的坐标值；
    /// </summary>
    private static Vector3 ShiftVector3 = new Vector3(0, -0.28202f, -0.1967f);

    #region 组件

    /// <summary>
    /// 所有猫的预制体字典；
    /// </summary>
    public Dictionary<string, GameObject> DicAllCatsPrefabs = new Dictionary<string, GameObject>();

    #endregion


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 用这个方法初始化	
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update每帧调用一次
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// 将屏幕的坐标转换成3D坐标的方法；
    /// </summary>
    /// <param name="v2"></param>
    /// <param name="UI_Camera"></param>
    /// <returns></returns>
    public Vector3 ScreenPositionToWorld(Vector3 v2, Camera UI_Camera)
    {
        Vector3 v3 = v2;
        //然后对其进行一些调整；
        v3 += ShiftVector3;
        //之后返回次值；
        return v3;
    }

}
