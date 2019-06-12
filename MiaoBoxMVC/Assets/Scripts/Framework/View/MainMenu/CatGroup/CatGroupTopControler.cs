using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 猫分组顶部分组的控制器，单例类；
/// </summary>
public class CatGroupTopControler : MonoBehaviour
{

    public static CatGroupTopControler Instance;
    /// <summary>
    /// 是否含有信息；
    /// </summary>
    private bool IsHasInfo = false;

    #region 组件

    public Text TextCatName;

    public Text TextLevel;

    public Text TextEnergy;

    public Text TextSpecailMessage;

    public Text TextIntroduce;

    public Image CatImage;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FirstShow()
    {
        if (IsHasInfo) return;
        IsHasInfo = true;
        transform.localScale = Vector3.one;
    }

    #region 数据读取

    public string CatName
    {
        get
        {
            return TextCatName.text;
        }
        set
        {
            FirstShow();
            TextCatName.text = value;
        }
    }

    public int Level
    {
        get
        {
            return int.Parse(TextLevel.text);
        }
        set
        {
            FirstShow();
            TextLevel.text = value.ToString();
        }
    }

    public int Energy
    {
        get
        {
            return int.Parse(TextEnergy.text);
        }
        set
        {
            FirstShow();
            TextEnergy.text = value.ToString();
        }
    }

    public string SpecailMessage
    {
        get
        {
            return TextSpecailMessage.text;
        }
        set
        {
            FirstShow();
            TextSpecailMessage.text = value;
        }
    }

    public string Introduce
    {
        get
        {
            return TextIntroduce.text;
        }
        set
        {
            FirstShow();  
            TextIntroduce.text = value;
        }
    }

    #endregion

}
