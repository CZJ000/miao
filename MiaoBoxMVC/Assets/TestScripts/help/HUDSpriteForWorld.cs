using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSpriteForWorld : MonoBehaviour {


    private Dictionary<Transform, GameObject> MAttributeList = new Dictionary<Transform, GameObject>();
    

    /// <summary>
    /// 产生属性图标
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="offsetpos"></param>
    /// <param name="offsetrot"></param>
    /// <param name="prefabs"></param>
    public void CreatOrShowAttributSprite(Transform trans,Vector3 offsetpos , GameObject prefabs,string Attribute)
    {  
        if (MAttributeList.ContainsKey(trans))
        {
            MAttributeList[trans].gameObject.SetActive(true);
            return;
        }
        GameObject go = Instantiate(prefabs, trans, false);
        go.transform.position = trans.position + offsetpos;
        go.GetComponentInChildren<Image>().color = MiaoBoxTool.SwitchColor(Attribute);
       // go.transform.localEulerAngles = offsetrot;
        MAttributeList.Add(trans, go);
    }
    
    /// <summary>
    /// 产生信号图标
    /// </summary>
    /// <param name="trans"></param>
    public void FadeAttribute(Transform trans)
    {
        if(MAttributeList.ContainsKey(trans))
        {
            MAttributeList[trans].gameObject.SetActive(false);
        }

    }
     
}
