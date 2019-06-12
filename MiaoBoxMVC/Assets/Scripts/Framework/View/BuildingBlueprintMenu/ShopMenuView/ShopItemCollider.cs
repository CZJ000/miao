using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class ShopItemCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseUpAsButton()
    {
        /////在执行点击事件之前，需要判定是否点击在UI上，如果在UI上则返回不执行下面的程序，防止穿透..；
        if (CanvasUIMediator.Instance.IsInterceptFromUI) return;

        // Debug.Log("asdasd");
        UIManager.GetInstance().ShowUIForms("ShopView");
        UIBaseBehaviour<ShopViewMediator>.CreateUI<ShopView>();
      
      
        //  view.Show();
    }
}
