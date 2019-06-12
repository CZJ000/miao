using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMiaoImage : MonoBehaviour {

    public Vector3 offset
    {
        set
        {

            _offset = value;

        }

    }
    private Vector3 _offset = Vector3.zero;
    private RectTransform mrecttrans;
    private Image Mimage;
    public Transform target;

    private float targetscale = 0.2f;
    private bool isUseScaleAnimation = false;


    void Awake()
    {

        mrecttrans = this.GetComponent<RectTransform>();


    }



    public void AddImage(string Path, Transform trans, float fadetime,bool isneedFade=false )
    {

        isUseScaleAnimation = true;
        Mimage = this.GetComponent<Image>();


        Mimage.sprite = Resources.Load<Sprite>(Path);
        
        target = trans;
        if (isneedFade)
        Invoke("fade", fadetime);


    }

    void Update()
    {

        if (target != null)
        {

            Vector3 temp = Camera.main.WorldToScreenPoint(target.position + _offset) - new Vector3(Screen.width / 2, Screen.height / 2, 0f);

            mrecttrans.anchoredPosition3D = temp;


        }
        if (isUseScaleAnimation)
        {
            TextAnimation(2f);
        }

    }

    private void TextAnimation(float time)
    {
        if (Mimage.rectTransform.localScale.x < targetscale)
        {

            float temp = Mathf.Lerp(Mimage.rectTransform.localScale.x, targetscale, Time.deltaTime * time);
            Mimage.rectTransform.localScale = new Vector3(temp, temp, temp);
            if (targetscale - temp < 0.1f)
            {
                temp = targetscale;
                Mimage.rectTransform.localScale = new Vector3(temp, temp, temp);
                isUseScaleAnimation = false;

            }
        }

    }

    public  void fade()
    {
        if (isUseScaleAnimation)
        {
            isUseScaleAnimation = false;
        }
        Mimage.rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

    }

   
}
