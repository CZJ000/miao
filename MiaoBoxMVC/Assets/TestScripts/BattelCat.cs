using UnityEngine;
using System.Collections;
using Global;

public class BattelCat : MonoBehaviour
{



    private StateMachine<BattelCat> StateMachine;

    private BattleCatInfo cat;
    private AttackPatten attackpatten=AttackPatten.Normal;

    void Start()
    {
        cat = this.GetComponent<BattleCatInfo>();
        StateMachine = new StateMachine<BattelCat>(this);
        StateMachine.SetCurrentState(CatIdle._instance);
        StateMachine.SetGlobalState(GlobalCatState._instance);

    }



    public StateMachine<BattelCat> GetMs()
    {
        return StateMachine;
    }

    public void OnDisable()
    {
        Destroy(this);
    }

    
    

    public BattelCat SetAttackObjIndex(int index,RoleType type)
    {    

         
        if (type==RoleType.Player)
        {
            if ((CreatPoint.Instance.getaiprefabs().Count - 1 < index))
            {
                index = CreatPoint.Instance.getaiprefabs().Count - 1;
            }
           
                return CreatPoint.Instance.getaiprefabs()[index].GetComponent<BattelCat>();
            
           

        }
        else if (type==RoleType.Enemy)
        {
            if ((CreatPoint.Instance.getprefabs().Count - 1 < index))
            {
                index = CreatPoint.Instance.getprefabs().Count - 1;
            }

            return CreatPoint.Instance.getprefabs()[index].GetComponent<BattelCat>();

        }

        return null;





    }

   

    public void  SetAttackPatten(AttackPatten type)
    {
        attackpatten = type;
    }
    public AttackPatten GetAttackPatten()
    {
         
        
        return attackpatten;
    }

}
