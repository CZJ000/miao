using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HudTextAction : MonoBehaviour
{
    Camera camera;


    GameObject target;
    float startTime;
    public Text text;

    float fadeTime=0.5f;
    float fullApha;

    float deltaY;
    float screenHeight;
    float screenWidth;

    float maxY;

    bool first;
    IEnumerator Fade()
    {




        while (Time.time - startTime <= fadeTime)
        {         
            
                
                text.color = new Color(text.color.r, text.color.g, text.color.b, fullApha - fullApha * ((Time.time - startTime) / fadeTime));

            deltaY = maxY * ((Time.time - startTime) / fadeTime);
             yield return null;              
        }
        transform.gameObject.SetActive(false);



    }

    private void OnEnable()
    {
        deltaY = 0;     
        startTime = Time.time;
        StartCoroutine("Fade");             
    }


    private void Awake()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
         maxY = 1;
        camera = Camera.main;
        fullApha = text.color.a;
    
    }


    public void OnBorn(string str,GameObject target)
    {
        text.text = str;
        this.target = target;
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        FollowTarget();
    }


    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 posi = camera.WorldToScreenPoint(target.transform.position + new Vector3(0, deltaY, 0)) - new Vector3(screenWidth, screenHeight, 0) / 2;
            transform.localPosition = new Vector3(posi.x, posi.y, 0);
        }
       
    }

}
