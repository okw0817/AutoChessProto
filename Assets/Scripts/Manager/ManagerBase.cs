using UnityEngine;
public class ManagerBase<T> : MonoBehaviour, IInitializer where T : MonoBehaviour
{
    #region Members : Private
    private static T instance;
    private bool isInit;
    #endregion

    #region Members : Property
    public static T Instance //Singleton
    {
        get
        {
            if(!instance)
            {
                GameObject obj;
                obj = GameObject.Find(typeof(T).Name);
                if (!obj)
                {
                    obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                else
                    instance = obj.GetComponent<T>();
            }
            return instance;
        }
    }
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        isInit = false;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Methods : Virtual
    public virtual void Init() 
    {
        isInit = true;
    }
    #endregion
}
