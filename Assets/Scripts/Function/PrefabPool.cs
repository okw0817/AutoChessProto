using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    #region Members : Private
    [SerializeField]
    private Transform pool;

    private Dictionary<string, List<GameObject>> activeDic = new Dictionary<string, List<GameObject>>();
    #endregion

    #region Methods : Public
    public void PushPool(string key, GameObject obj)
    {
        if(activeDic.ContainsKey(key))
        {
            obj.transform.SetParent(pool);
            obj.SetActive(false);
            activeDic[key].Add(obj);
        }
        else
        {
            activeDic.Add(key, new List<GameObject>());
            obj.transform.SetParent(pool);
            obj.SetActive(false);
            activeDic[key].Add(obj);
        }
    }

    public GameObject PopPool(string key)
    {
        if (activeDic.ContainsKey(key))
        {
            if (activeDic[key].Count > 0)
            {
                var obj = activeDic[key][0];
                activeDic[key].RemoveAt(0);
                obj.SetActive(true);
                return obj;
            }
            else
                return null;
        }
        else
            return null;
    }
    #endregion
}
