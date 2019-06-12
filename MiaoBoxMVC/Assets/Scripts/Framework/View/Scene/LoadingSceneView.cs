using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Global;

public class LoadingSceneView : MonoBehaviour {
    public GameObject Title;
    public Slider Slider;
    public Image BG;
    private AsyncOperation async;
    private float   progress=0;

    private int TextIndex = 0;
   
	// Use this for initialization
	void Start () {
        StartCoroutine("LoadScene");
	}
	
	// Update is called once per frame
	void Update () {

        progress = async.progress;
        if (async.progress>=0.9f)
        {
            progress = 1.0f;
        }
        if (progress != Slider.GetComponent<Slider>().value)
        {
            Slider .value = Mathf.Lerp(Slider .value, progress, Time.deltaTime * 5f);
            if (Mathf.Abs(Slider.value-progress)<=0.01f)
               {
                Slider.value = progress;
               }
        }

        DrawAnimation();
      //  BG. material.mainTextureOffset = new Vector2(Random.Range(0, 1), Random.Range(0, 1)); 
        if ((int )Slider.value*100==100&&progress==1.0f)
        {
            async.allowSceneActivation = true;
        } 

    }

    IEnumerator LoadScene()
    {
      
        async = (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).asyncLoadscene();
        async.allowSceneActivation = false;
        yield return async;
    }

    private void DrawAnimation()
    {
        TextIndex++;
        TextIndex %= 4;
        switch(TextIndex)
        {
            case 0: Title.GetComponent<Text>().text = "loding miao  miao  .. "; break;
            case 1: Title.GetComponent<Text>().text = "loding miao  miao  ..... "; break;
            case 2: Title.GetComponent<Text>().text = "loding miao  miao  ....... "; break;
            case 3: Title.GetComponent<Text>().text = "loding miao  miao  ........."; break;
        }
        
    }
}
