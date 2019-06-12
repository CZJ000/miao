using UnityEngine;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

class AddCustomerModelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        CustomerProxy customerProxy = Facade.RetrieveProxy(CustomerProxy.NAME) as CustomerProxy;
        UserInfoProxy user = AppFacade.GetInstance().RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
		NeighborInfoProxy neighbor = AppFacade.GetInstance().RetrieveProxy(NeighborInfoProxy.NAME) as NeighborInfoProxy;
        BuildBlueprintProxy porxy = AppFacade.GetInstance().RetrieveProxy(BuildBlueprintProxy.NAME) as BuildBlueprintProxy;

        HashSet<int> catTypes = new HashSet<int>();
        for (int i = 1; i <= 4; i++)
        {
            //拿到不同位置上建筑prefab的id
            int facility = user.GetFacility(i);
            if(facility != 0)
            {
                //拿到prefab对应的可雇佣雇员（即在通道上跑的）信息的类型
                List<int> catTypeList = porxy.GetRecruittypes(facility);

                foreach (int catType in catTypeList)
                {
                    catTypes.Add(catType);
                }
            }

            //邻居的建筑也会影响可雇佣雇员类型
            facility = neighbor.GetNeighbor1Facility(i);
            if (facility != 0)
            {
                List<int> catTypeList = porxy.GetRecruittypes(facility);

                foreach (int catType in catTypeList)
                {
                    catTypes.Add(catType);
                }
            }
            facility = neighbor.GetNeighbor2Facility(i);
            if (facility != 0)
            {
                List<int> catTypeList = porxy.GetRecruittypes(facility);

                foreach (int catType in catTypeList)
                {
                    catTypes.Add(catType);
                }
            }
        }

        if(catTypes.Count == 0)
        {
            customerProxy.AddCustomerModel(CatPool.GetInstance().GetCustomerRandomID());
        }
        else
        {
            int randomType = Random.Range(0, catTypes.Count);
            int[] cattypesArray = new int[catTypes.Count];
            catTypes.CopyTo(cattypesArray);
            customerProxy.AddCustomerModel(cattypesArray[randomType]);
        }

        
    }
}

