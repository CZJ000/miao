using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hudparticipant : MonoBehaviour
{
    /// <summary>
    /// 字体hud对象
    /// </summary>
    public GameObject Textprefabs
    {
         set
        {    
            if (HUDRoot.go==null)
            {
                GameObject.Destroy(this);
                return;
            }
           
            
                 
             GameObject go = Instantiate(value, HUDRoot.go.GetComponent<RectTransform>());
            if (go.GetComponent<HUDMiaoText>() == null)
            {
                _HudText = go.AddComponent<HUDMiaoText>();

            }
            else _HudText = go.GetComponent<HUDMiaoText>();
            go.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            



        }

    }              
    /// <summary>
    /// 图片hud对象
    /// </summary>
    public GameObject ImagePrefbas
    {
        set
        {
            if (HUDRoot.go == null)
            {
                GameObject.Destroy(this);
                return;
            }



            GameObject go = Instantiate(value, HUDRoot.go.GetComponent<RectTransform>());
            if (go.GetComponent<HUDMiaoImage>() == null)
            {
                Debug.Log("Get HudMiaoImage");
                _HUdImage = go.AddComponent<HUDMiaoImage>();

            }
            else _HUdImage = go.GetComponent<HUDMiaoImage>();
            go.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);




        }

    }
    /// <summary>
    /// 世界属性图片对象
    /// </summary>
    public GameObject AttributeSprite
    {
        set {
            if (this.gameObject.GetComponent<HUDSpriteForWorld>() == null)
            {
                _HudSpriteForWorld = this.gameObject.AddComponent<HUDSpriteForWorld>();
            }
            else _HudSpriteForWorld = this.gameObject.GetComponent<HUDSpriteForWorld>();
            _AttributeSprite = value;
        }
        get
        {
            return _AttributeSprite;
        }

    }

    private GameObject _AttributeSprite;
    
    

    HUDSpriteForWorld _HudSpriteForWorld = null;
    HUDMiaoText _HudText = null;
    HUDMiaoImage _HUdImage = null;
    public HUDMiaoText HudMiaoText { get { return _HudText; } }
    public HUDMiaoImage HudMiaoImage { get { return _HUdImage; } }
    public HUDSpriteForWorld HudSpriteForWorld { get { return _HudSpriteForWorld; } }
    public void OnDisable()
    {
        Destroy(this);
       
    }

    // Use this for initialization



}
