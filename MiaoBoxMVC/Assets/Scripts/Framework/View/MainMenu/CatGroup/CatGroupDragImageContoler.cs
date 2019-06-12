using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 猫分组菜单的拖动图片；
/// </summary>
public class CatGroupDragImageContoler : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CatGroupDragImageContoler Instance;
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
    /// 当前的图片；
    /// </summary>
    Image image;
    /// <summary>
    /// 当前需要显示的图片；
    /// </summary>
    public Sprite sprite
    {
        get
        {
            return image.sprite;
        }
        set
        {
            image.sprite = value;
        }
    }
    /// <summary>
    /// 开始拖动时的猫信息；
    /// </summary>
    internal CatInfo StartDragCatInfo;
    /// <summary>
    /// 拖动结束之后的回调方法；
    /// </summary>
    public CallBack AfterDrag;
    /// <summary>
    /// 是否在被拖动；
    /// </summary>
    public bool IsDraging = false;
    /// <summary>
    /// 一半屏幕的大小；
    /// </summary>
    public Vector3 HalfScreen;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        HalfScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f-30, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 执行拖动完成之后的方法；
    /// </summary>
    public void DoMethodAfterDrag()
    {
        //执行回调方法；
        if (AfterDrag != null)
        {
            AfterDrag();
        }
        //将初始记录的猫信息清空；
        StartDragCatInfo = null;
    }
}
