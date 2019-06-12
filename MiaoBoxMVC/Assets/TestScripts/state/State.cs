using UnityEngine;
using System.Collections;

public class State<T> : MonoBehaviour
{
    public virtual  void Enter(T Obj)
    {

    }  

    public virtual  void Execute(T Obj)
    {

    }

    public virtual void Exit(T Obj)
    {

    }

}
