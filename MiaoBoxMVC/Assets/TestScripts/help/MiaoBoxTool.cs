using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiaoBoxTool  {

    public  static Color SwitchColor(string Attribute)
    {
        switch (Attribute)
        {
            case "w":
                return Color.white; 
            case "r": return Color.red;  
            case "b": return Color.blue; 
            case "p": return Color.magenta;  
            case "g": return Color.green; 
            default: return Color.gray;  
        }

    }
    public static IEnumerator FadeUISprite(Image target, float duration, Color color)
    {
        if (target == null)
            yield break;

        float alpha = target.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            if (target == null)
                yield break;
            Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.color = newColor;
            yield return null;
        }


    }

    
    public static IEnumerator FadeText(Text target, float duration, Color color )
    {
       
        if (target == null)
            yield break;
 
        float alpha = target.color.a;
        float t=0f;
        for ( t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            if (target == null)
                yield break;
            Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.color = newColor;
            yield return null;
        }
        Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
        target.color = finalColor;
    }
    
    public static IEnumerator FadeSprite(SpriteRenderer target, float duration, Color color)
    {
        if (target == null)
            yield break;

        float alpha = target.material.color.a;

        float t = 0f;
        while (t < 1.0f)
        {
            if (target == null)
                yield break;

            Color newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.material.color = newColor;

            t += Time.deltaTime / duration;

            yield return null;

        }
        Color finalColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
        target.material.color = finalColor;
    }

    public static IEnumerator ScaleChange(GameObject go,float Speed,float TargetScale  )
    {


        bool isbig=   Mathf.Max(go.transform.localScale.x, TargetScale) >go.transform.localScale.y ? true : false;
        if (isbig)
        {
           while( go.transform.localScale.x<TargetScale )
            {

                float temp=  Mathf.Lerp(go.transform.localScale.x, TargetScale, Time.deltaTime * Speed);

                go.transform.localScale = new Vector3(temp,temp,temp);
                if (Mathf.Abs(go.transform.localScale.x-TargetScale)<0.05f)
                {

                    go.transform.localScale = new Vector3(TargetScale, TargetScale, TargetScale);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
             

        }else
        {
            while (go.transform.localScale.x > TargetScale)
            {
                float temp = Mathf.Lerp(go.transform.localScale.x, TargetScale, Time.deltaTime * Speed);
                go.transform.localScale = new Vector3(temp, temp, temp);
                if (Mathf.Abs(go.transform.localScale.x - TargetScale) < 0.05f)
                {

                    go.transform.localScale = new Vector3(TargetScale, TargetScale, TargetScale);
                    break;
                }
                yield return new WaitForFixedUpdate();
            }

        }
        
       
         

    }
    
    public static IEnumerator FillImage(Image origin,float target,float duration)
    {
        if (origin==null)
        {
            yield break;
        }
        float currentvalue = origin.fillAmount;
        float t = 0f;
        for (t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            if (origin == null)
                yield break;
            origin.fillAmount = Mathf.SmoothStep(currentvalue,  target, t);
            
            yield return null;
        }
        origin.fillAmount = Mathf.SmoothStep(currentvalue, target, t);
    }
    public static IEnumerator FillShader(Image origin,string shaderpAttrbuteName ,float target, float duration)
    {
        if (origin == null)
        {
            yield break;
        }
        float currentvalue = origin.fillAmount;
        float t = 0f;
        for (t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            if (origin == null)
                yield break;
            origin.material.SetFloat(shaderpAttrbuteName, Mathf.SmoothStep(currentvalue, target, t));

            yield return null;
        }
        origin.material.SetFloat(shaderpAttrbuteName, Mathf.SmoothStep(currentvalue, target, t));
    }
}
