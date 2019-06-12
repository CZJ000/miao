using UnityEngine;
using UnityEditor;

public class HudTextPool : MonoBehaviour
{

    ObjectPool pool;

    public GameObject prefab;

    public static HudTextPool mInstance = null;
    float mLastDestructTime;

    public static HudTextPool GetInstance()
    {
        if (mInstance == null)
            mInstance = GameObject.Find("HudTextPool").GetComponent<HudTextPool>();
        return mInstance;
    }

    public void Start()
    {
        pool = new ObjectPool();
         GameObject prefabObj = prefab as GameObject;    
        pool.Init(prefabObj.name, prefabObj, 0,0);
        pool.doNotDestruct = true;
        pool.SetRoot(gameObject);
        
    }


    public ObjectPool GetHudTextPool()
    {
        return pool;
    }

    private void Update()
    {
        Loop(Time.deltaTime);
    }

    public void Loop(float deltaTime)
    {
        if (Time.time - mLastDestructTime > 0.1f)
        {
            mLastDestructTime = Time.time;
          
                if (null != pool)
                pool.AutoDestruct();
            
        }
    }
}