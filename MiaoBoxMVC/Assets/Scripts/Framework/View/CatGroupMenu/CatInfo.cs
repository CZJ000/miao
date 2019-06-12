using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CatInfo : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    #region 猫的信息；

    public int Id { get; set; }
    public int CatTypeid { get; set; }
    public int CatCaptainTypeid { get; set; }
    public int GroupId { get; set; }
    public int Keep { get; set; }
    public DateTime AcqDate { get; set; }
    public int Grow { get; set; }

    public int Evo { set; get; }
    public string Name { get; set; }
    public int Level { set; get; }
    public int Iq { set; get; }
    public int Power { set; get; }
    public int React { set; get; }
    public int Skill { set; get; }
    /// <summary>
    /// 猫的属性；
    /// </summary>
    public string Attribute { get; set; }
    public string About { get; set; }
    /// <summary>
    /// 猫的分组情况；
    /// </summary>
    public string MembersSlot { get; set; }
    /// <summary>
    /// 猫的攻击类型；
    /// </summary>
    public int AttackType { get; set; }
    /// <summary>
    /// 猫的技能类型；
    /// </summary>
    public int SkillType { get; set; }
    #endregion;

    /// <summary>
    /// 是否获得过所有的组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 猫的图片；
    /// </summary>
    Image CatImage;
    /// <summary>
    /// 猫的类型图片（那个圆点）；
    /// </summary>
    Image CatTypeIamge;
    /// <summary>
    /// 默认图片；
    /// </summary>
    Sprite DefaultSprite;
    /// <summary>
    /// 按钮；
    /// </summary>
    Button button;
    /// <summary>
    /// 当前的3D猫；
    /// </summary>
    CatModelContorl Now3DCat;
    /// <summary>
    /// 猫预制的路径；
    /// </summary>
    const string CatPrefabPath = "Characters/cat";
    /// <summary>
    /// 特殊的表现形式；
    /// </summary>
    private CatInfoShow thisShow;

    private void Start()
    {
        //获取所有的组件；
        GetAllCompoenets();
    }

    /// <summary>
    /// 获得所有的组件；
    /// </summary>
    public void GetAllCompoenets()
    {
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        //获取猫的图片；
        DefaultSprite = CatGroupContoler.Instance.DefaultCatSprite;
        CatImage = transform.Find("CatImage").GetComponent<Image>();
        CatTypeIamge = transform.Find("CatType").GetComponent<Image>();
        //设置点击回调；
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate()
        {
            //在顶部的信息栏中间更改当前猫的信息；
            CatGroupTopControler.Instance.CatName = Name;
            CatGroupTopControler.Instance.Level = Level;
            CatGroupTopControler.Instance.Introduce = About;
            CatGroupTopControler.Instance.Energy = Power;
            if (CatTypeid >= 0)
            {
                CatGroupTopControler.Instance.CatImage.sprite = CatGroupContoler.Instance.GetCatSpriteFromDic(CatTypeid);
                CatGroupTopControler.Instance.CatImage.transform.localScale = Vector3.one;
            }
            else
            {
                CatGroupTopControler.Instance.CatImage.transform.localScale = Vector3.zero;
            }
        });
        thisShow = GetComponent<CatInfoShow>();
    }

    /// <summary>
    /// 刷新；
    /// </summary>
    public void Refresh()
    {
        //如果此时猫分组面板没有打开则不执行；
        if (!CatGroupContoler.Instance.IsInvoke) return;
        //根据ID找图片；
        if (CatTypeid <= 0)
        {
            CatImage.sprite = DefaultSprite;
        }
        else
        {
            CatImage.sprite = CatGroupContoler.Instance.GetCatSpriteFromDic(CatTypeid);
        }
        CatImage.color = Color.white;
        //刷新猫图片；
        CatTypeIamge.transform.localScale = Vector3.one;
        //根据猫类型选择猫的颜色；
        switch (Attribute)
        {
            //白
            case "w":
                CatTypeIamge.color = Color.white;
                break;
            //紫
            case "p":
                CatTypeIamge.color = new Color(0.5f, 0, 1);
                break;
            //红
            case "r":
                CatTypeIamge.color = Color.red;
                break;
            //绿
            case "g":
                CatTypeIamge.color = Color.green;
                break;
            //蓝
            case "b":
                CatTypeIamge.color = Color.blue;
                break;
            //如果以上皆不是
            default:
                CatTypeIamge.transform.localScale = Vector3.zero;
                break;
        }
        if (thisShow!= null)
        {
            thisShow.ShowMethod(this);
        }
    }

    /// <summary>
    /// 开始拖动；
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //如果此处是个空位置，则不执行拖动方法；
        if (CatTypeid <= 0) return;
        //拖动开始；
        CatGroupDragImageContoler.Instance.IsDraging = true;
        CatGroupDragImageContoler.Instance.IsInvoke = true;
        CatGroupDragImageContoler.Instance.sprite = CatImage.sprite;
        CatGroupDragImageContoler.Instance.StartDragCatInfo = this;
    }

    /// <summary>
    /// 拖动中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //如果此处是个空位置，则不执行拖动方法；
        if (CatTypeid <= 0) return;
        CatGroupDragImageContoler.Instance.transform.localPosition = Input.mousePosition - CatGroupDragImageContoler.Instance.HalfScreen;
    }

    /// <summary>
    /// 拖动结束；
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        //如果此处是个空位置，则不执行拖动方法；
        if (CatTypeid <= 0) return;
        //拖动结束；
        CatGroupDragImageContoler.Instance.IsDraging = false;
        CatGroupDragImageContoler.Instance.IsInvoke = false;
        //执行拖动完成的方法：
        CatGroupDragImageContoler.Instance.DoMethodAfterDrag();
    }
}

/// <summary>
/// 不挂载到对象身上的猫信息类，用于保存信息；
/// </summary>
public class CatInfoCopy
{
    public int Id { get; set; }
    public int CatTypeid { get; set; }
    public int CatCaptainTypeid { get; set; }
    public int GroupId { get; set; }
    public int Keep { get; set; }
    public DateTime AcqDate { get; set; }
    public int Grow { get; set; }

    public int Evo { set; get; }
    public string Name { get; set; }
    public int Level { set; get; }
    public int Iq { set; get; }
    public int Power { set; get; }
    public int React { set; get; }
    public int Skill { set; get; }
    /// <summary>
    /// 猫的属性；
    /// </summary>
    public string Attribute { get; set; }
    public string About { get; set; }
    /// <summary>
    /// 猫的分组情况；
    /// </summary>
    public string MembersSlot { get; set; }
    /// <summary>
    /// 猫的攻击类型；
    /// </summary>
    public int AttackType { get; set; }
    /// <summary>
    /// 猫的技能类型；
    /// </summary>
    public int SkillType { get; set; }
}


