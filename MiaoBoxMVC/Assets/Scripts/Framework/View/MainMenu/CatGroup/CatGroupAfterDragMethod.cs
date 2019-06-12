using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 抽象类，在拖动结束之后执行的方法，鼠标进入时开始写入方法；
/// </summary>
public abstract class CatGroupAfterDragMethod : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 当前的猫信息组；
    /// </summary>
    protected CatInfo catInfo;

    /// <summary>
    /// 鼠标进入后的方法；
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        CatGroupDragImageContoler.Instance.AfterDrag = delegate ()
         {
             AfterDragToThis();
         };
    }

    /// <summary>
    /// 鼠标离开后的方法；
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        CatGroupDragImageContoler.Instance.AfterDrag = null;
    }

    // Use this for initialization
    void Start()
    {
        catInfo = GetComponent<CatInfo>();
        AfterStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 鼠标移动到此之后执行的回调方法；
    /// </summary>
    protected abstract void AfterDragToThis();
    /// <summary>
    /// 启动之后执行；
    /// </summary>
    protected abstract void AfterStart();
}
