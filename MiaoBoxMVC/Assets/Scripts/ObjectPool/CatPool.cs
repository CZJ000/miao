/********************************************************************
	created:	2016/09/08
	created:	8:9:2016   22:09
	filename: 	F:\Users\Administrator\Projects\MiaoBox\MiaoBoxMVC\Assets\Scripts\ObjectPool\CatPool.cs
	file path:	F:\Users\Administrator\Projects\MiaoBox\MiaoBoxMVC\Assets\Scripts\ObjectPool
	file base:	CatPool
	file ext:	cs
	author:		Zhou Jingren
	
	purpose:	cat的对象池 inactive时自动回收
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 创建所有猫种类的对象池
/// 每0.1秒会刷新激活组（暂时以隐藏代表对象消失，而不是销毁）
/// </summary>
public class CatPool : MonoBehaviour
{
    private Dictionary<int, ObjectPool> mEffectPools;
    private float mLastDestructTime;
    public static CatPool mInstance = null;

    private List<int> mCurrentCats = new List<int>(); //做好prefab的猫id 临时存储

    public static CatPool GetInstance()
    {
        if (mInstance == null)
            mInstance = GameObject.Find("CatPool").GetComponent<CatPool>();
        return mInstance;
    }
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        mEffectPools = new Dictionary<int, ObjectPool>();
        string catPath = "Characters/";
        Object[] prefabs = Resources.LoadAll(catPath, typeof(GameObject));
        foreach (var prefab in prefabs)
        {
            GameObject prefabObj = prefab as GameObject;

          

            string prefabObjID = prefabObj.name.Substring(3);
           
            int catID = int.Parse(prefabObjID);
            mCurrentCats.Add(catID);

            mEffectPools.Add(catID, new ObjectPool());
            mEffectPools[catID].Init(prefabObj.name, prefabObj, 0, 0);
            mEffectPools[catID].doNotDestruct = true;
            mEffectPools[catID].SetRoot(gameObject);
        }
    }

    void Update()
    {
        Loop(Time.deltaTime);
    }

    public int GetRandomID()
    {
        int randomID = Random.Range(0, mCurrentCats.Count - 1);
        return mCurrentCats[randomID];
    }

    public int GetCustomerRandomID()
    {
        return Random.Range(2, 4);
    }

	public int GetEmployeeRandomID()
	{
		return Random.Range(2, 5);
	}

    public ObjectPool GetCatPool(int catId)
    {
        if (mEffectPools.ContainsKey(catId))
        {
            return mEffectPools[catId];
        }
        Debug.LogError("资源里没有找到ID为" + catId + "猫！");
        return null;
    }
    public void Loop(float deltaTime)
    {
        if (Time.time - mLastDestructTime > 0.1f)
        {
            mLastDestructTime = Time.time;
            foreach (ObjectPool p in mEffectPools.Values)
            {
                if (null != p)
                    p.AutoDestruct();
            }
        }
    }
    public void DestructAll()
    {
        foreach (ObjectPool pool in mEffectPools.Values)
        {
            pool.DestructAll();
        }
    }
    public void DestructSinglePool(int PoolValue)
    {
        if (mEffectPools.ContainsKey(PoolValue))
        {
            mEffectPools[PoolValue].DestructAll();
        }
       
    }
}
