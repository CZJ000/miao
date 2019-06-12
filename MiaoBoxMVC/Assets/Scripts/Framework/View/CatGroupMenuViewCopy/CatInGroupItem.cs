using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
public class CatInGroupItem : MonoBehaviour
{

    public CatInGroupInfoVO catInGroupInfo;


    public Vector2 posi;


  
    Color attribute;

    public Image Bg;

    public Image captainLogo;

    public Image attributeLogo;

    public Transform catTransform;

    public Text tipText;

    public bool empty=false;

    public int groupId;

    Color purple = new Color(241/255.0f,0,1,1);
    Color captainColor = new Color(1,132/255.0f,0);
    Color memberColor = new Color(1, 1, 0);



    // Use this for initialization
    void Start()
    {

       
      
      
    }

    public void Init()
    {
        if (catInGroupInfo != null)
        {


            if (catInGroupInfo.Type == 1)
            {
                captainLogo.enabled = true;
                Bg.color = captainColor;
            }
            else
            {
                captainLogo.enabled = false;
                Bg.color = memberColor;
            }

            switch (catInGroupInfo.Attribute)
            {
                case "p":
                    attribute = purple;
                    break;
                case "w":
                    attribute = Color.white;
                    break;
                case "g":
                    attribute = Color.green;
                    break;
                case "b":
                    attribute = Color.blue;
                    break;
                case "r":
                    attribute = Color.red;
                    break;
            }
            attributeLogo.color = attribute;
            if (empty)
            {
                tipText.gameObject.SetActive(true);
            }
            else
            {
                tipText.gameObject.SetActive(false);
            }

        }
        else
        {


            Bg.color = memberColor;
            attributeLogo.enabled=false;
            captainLogo.enabled = false;

        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
