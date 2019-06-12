using UnityEngine;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

class AddEmployeeModelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        EmployeeProxy employeeProxy = Facade.RetrieveProxy(EmployeeProxy.NAME) as EmployeeProxy;
		UserInfoProxy user = AppFacade.GetInstance().RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
		BuildBlueprintProxy porxy = AppFacade.GetInstance().RetrieveProxy(BuildBlueprintProxy.NAME) as BuildBlueprintProxy;

		HashSet<int> catTypes = new HashSet<int>();
		for (int i = 1; i <= 4; i++)
		{
			int facility = user.GetFacility(i);
			if(facility != 0)
			{
                List<int> catTypeList = porxy.GetRecruittypes(facility);

                foreach(int catType in catTypeList)
                {
                    catTypes.Add(catType);
                }              
			}
		}

		if(catTypes.Count == 0)
		{
			employeeProxy.AddEmployeeModel(CatPool.GetInstance().GetEmployeeRandomID());
		}
		else
		{
			int randomType = Random.Range(0, catTypes.Count);
			int[] cattypesArray = new int[catTypes.Count];
			catTypes.CopyTo(cattypesArray);
			employeeProxy.AddEmployeeModel(cattypesArray[randomType]);
		}
    }
}

