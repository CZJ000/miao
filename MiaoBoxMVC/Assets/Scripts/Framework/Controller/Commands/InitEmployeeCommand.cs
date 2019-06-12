using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Global;
using SUIFW;
public class InitEmployeeCommand : SimpleCommand
{

    public override void Execute(INotification notification)
    {
        //base.Execute(notification);
        //暂时用Find，没想到好办法
        //GameObject employeeViewObj = GameObject.FindGameObjectWithTag(TagName.EMPLOYEE_VIEW);
        //if (employeeViewObj == null) return;
        //EmployeeMediator employeeMediator = new EmployeeMediator();
        //EmployeeView employeeView = employeeViewObj.GetComponent<EmployeeView>();
        //employeeMediator.ViewComponent = employeeView;

        //Facade.RegisterProxy(new EmployeeProxy());
        //Facade.RegisterMediator(employeeMediator);

        UIBaseBehaviour<SpawnRandomCatMediator>.CreateUI<SpawnRandomCatView>();

       // UIBaseBehaviour<EmployeeMediator>.CreateUI<EmployeeView>();

        

        //employeeView.enabled = true;

    }
}
