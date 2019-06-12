using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using UnityEngine.UI;
using SUIFW;
public class ClerkAreaView : MonoBehaviour
{

    public GameObject hudtextPrefab;
    public Transform clerksPos;
    public float randomRange = 2;

    public Transform catInUIPos;

    private Dictionary<int, GameObject> clerkObjs = new Dictionary<int, GameObject>();

    //public Text levelUpText;

    GameObject LevelUpclerk3DModel;





   


    // Use this for initialization
    void Start()
    {

        AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
        AppFacade.getInstance.SendNotification(NotiConst.SET_CLERK);
        // closeBtn.onClick.AddListener(OnCloseBtnOn);
    }

    void Disable()
    {
        AppFacade.GetInstance().SendNotification(NotiConst.CAT_GROUP_CLOSE);
    }

    public void SetClerks(List<JsonData> clerkIDs)
    {
        foreach (var obj in clerkObjs)
        {
            Destroy(obj.Value);
        }
        clerkObjs.Clear();
        Debug.Log(111);
        foreach (JsonData clerkID in clerkIDs)
        {
            int id = (int)clerkID["id"];
            int typeid = (int)clerkID["catTypeId"];
            Vector3 deltaDesOffset = new Vector3(Random.Range(-1f, 1f) * randomRange, 0, Random.Range(-1f, 1f) * randomRange).normalized;
            GameObject clerkObj = CatPool.GetInstance().GetCatPool(typeid).CreateObject(clerksPos.position + deltaDesOffset);
            ClerkCtl clerkCtl = clerkObj.AddComponent<ClerkCtl>();
            clerkCtl.hudtextPrefab = hudtextPrefab;
            clerkCtl.clerkInfo.id = id;
            clerkCtl.clerkInfo.catTypeId = typeid;
            clerkCtl.GetComponent<CharacterController>().enabled = true;
            if (!clerkObjs.ContainsKey(id))
            {
                clerkObjs.Add(id, clerkObj);
            }
        }
    }

    public void LevelUp(JsonData content)
    {
        int id = (int)content["id"];
        int exceedpoint = (int)content["exceedpoint"];
        int exceedlimit = (int)content["exceedlimit"];

        ClerkCtl clerkCtl = clerkObjs[id].GetComponent<ClerkCtl>();
        clerkCtl.ShowString(exceedpoint.ToString() + "/" + exceedlimit.ToString());

        Debug.Log("LevelUp :" + content.ToString());
    }

    public void ChangedCat(JsonData content)
    {
        int oldCatId = (int)content["oldCatId"];
        int newCatTypeId = (int)content["newCatTypeId"];
        GameObject clerkObj = clerkObjs[oldCatId];
        Vector3 oldCatPos = clerkObj.transform.position;
        Destroy(clerkObj);
        clerkObjs.Remove(oldCatId);
        clerkObj = CatPool.GetInstance().GetCatPool(newCatTypeId).CreateObject(oldCatPos);
        ClerkCtl clerkCtl = clerkObj.AddComponent<ClerkCtl>();
        clerkCtl.hudtextPrefab = hudtextPrefab;
        clerkCtl.clerkInfo.id = oldCatId;
        clerkCtl.GetComponent<CharacterController>().enabled = true;
        clerkCtl.clerkInfo.catTypeId = newCatTypeId;
        clerkObjs.Add(oldCatId, clerkObj);


        CatGroupMenuMediator catGroupMenuMediator = AppFacade.Instance.RetrieveMediator(CatGroupMenuMediator.NAME) as CatGroupMenuMediator;
        //  catGroupMenuMediator.catGroupMenuView
        int groupPageId = 4;

        JsonData groupId = new JsonData();
        groupId["id"] = groupPageId;
        //显示某一猫组信息；可供刷新用
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);


    }

    public void ShowLevelUpCatInfo(JsonData content)
    {
        string oldCatName = (string)content["oldCatName"];
        string newCatName = (string)content["newCatName"];
        int newCatTypeId = (int)content["newCatTypeId"];

         string s= oldCatName + "研修及格，成为" + newCatName;

        MessageView.GetInstance().ShowMessage(s);
      //  catInUIPos = LevelUpclerk3DModel.transform.parent;
        //Destroy(LevelUpclerk3DModel);
       

        //LevelUpclerk3DModel = CatPool.GetInstance().GetCatPool(newCatTypeId).CreateObject(catInUIPos.position);
        //LevelUpclerk3DModel.AddComponent<EmployeeRandomAnimation>();
        //LevelUpclerk3DModel.layer = 9;
        //LevelUpclerk3DModel.transform.localScale = new Vector3(2, 2, 2);
        //LevelUpclerk3DModel.transform.rotation = catInUIPos.rotation;
        //LevelUpclerk3DModel.transform.parent = catInUIPos;
     
        // StartCoroutine(WaitAndHide(2.0f));
    }

    //IEnumerator WaitAndHide(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    IsInvoke = false;
    //}


 

}
