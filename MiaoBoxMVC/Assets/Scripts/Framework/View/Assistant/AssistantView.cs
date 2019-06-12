using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class AssistantView : MonoBehaviour {

    public Transform assistantPos;
	private GameObject assistantObj;

    public void Start()
    {
        AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
        AppFacade.getInstance.SendNotification(NotiConst.SET_ASSISTANT);
    }

    public void SetAssistant(int catID)
    {
		if (assistantObj != null) {
            Destroy(assistantObj);
			assistantObj = null;
		}
		if (catID == -1)
			return;
        Debug.Log("SetAssistant");
		assistantObj = CatPool.GetInstance().GetCatPool(catID).CreateObject(assistantPos.position);
        assistantObj.AddComponent<AssistantCtl>();
    }
}
