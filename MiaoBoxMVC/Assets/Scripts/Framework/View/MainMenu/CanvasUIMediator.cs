using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// UI控制器的静态单例类；
/// 控制的UI：
/// </summary>
public class CanvasUIMediator : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CanvasUIMediator Instance;
    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {
            return transform.localScale != Vector3.zero;
        }
        set
        {
            transform.localScale = value ? Vector3.one : Vector3.zero;
        }
    }
    /// <summary>
    /// 是否获得过所有组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 事件数据；
    /// </summary>
    PointerEventData eventData;

    #region 组件

    RectTransform rectTransform;
    
    public EventSystem eventSystem;

    public GraphicRaycaster graphicRayCaster;

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
        GetAllCompoenets();
    }

    /// <summary>
    /// Update每帧调用一次
    /// </summary>
    void Update()
    {

    }


    /// <summary>
    /// 检测方法，检测当前点击是否在UI上；
    /// </summary>
    public bool IsInterceptFromUI
    {
        get
        {
            //参数准备；
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
            //检测当前点击的位置有多少个UI；
            List<RaycastResult> listUICast = new List<RaycastResult>();
            graphicRayCaster.Raycast(eventData, listUICast);
            //返回检测结果
            return listUICast.Count > 0;
        }
    }

    /// <summary>
    /// 获取所有组件的方法；
    /// </summary>
    public void GetAllCompoenets()
    {
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        //获取组件；
        rectTransform = GetComponent<RectTransform>();
        eventData = new PointerEventData(eventSystem);
    }

}
