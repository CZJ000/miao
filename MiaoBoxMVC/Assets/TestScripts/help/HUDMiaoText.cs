using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class HUDMiaoText : MonoBehaviour {

     
    public Vector3 offset {
        set
        {

            _offset = value;
        }

    }
    private Vector3 _offset=Vector3.zero;
    private RectTransform mrecttrans;
    private Text mtext;
    public Transform target;

    private float targetscale = 1f;
    private bool isUseScaleAnimation = false;
    private Color color;
        
    void Awake()
    {

        mrecttrans = this.GetComponent<RectTransform>();
      
         
    }

    

    public void AddText(string mes ,Color color,Transform trans ,float fadetime)
    {

        isUseScaleAnimation = true;
        mtext = this.GetComponent<Text>();   
        mtext.text = mes;
        mtext.fontSize =20;
        target = trans;
        mtext.color = color;
     
        StartCoroutine(Fade(fadetime, color));
         
       

    }
    
     void FixedUpdate()
    {

        if (target != null)
          {
            Vector3 temp = Camera.main.WorldToScreenPoint(target.position+_offset)-new Vector3(Screen.width/2,Screen.height/2,0f) ;          
            mrecttrans.anchoredPosition3D = temp;
       }
        if (isUseScaleAnimation)
        {
            isUseScaleAnimation = false;
            StartCoroutine(  MiaoBoxTool.ScaleChange(mtext.gameObject, 10f, targetscale));         
        }
            
    }
    IEnumerator Fade(float time,Color color)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(MiaoBoxTool.FadeText(mtext, 0.5f, color));
        yield return new WaitForSeconds(0.5f);
        mtext.rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

       // mtext.color =color;
    }


   
    
}
