using UnityEngine;

public class SigletoneBase<T> : MonoBehaviour, IInitializer where T: MonoBehaviour
{
    #region Members : Private
    private static T instance;
    #endregion

    #region Members : Property
    public static T Instance
    {
        get
        {
            if (!instance)
            {
                GameObject obj;
                obj = GameObject.Find(typeof(T).Name);

                instance = obj.GetComponent<T>();
            }
            
            return instance;
        }
    }
    #endregion

    #region Methods : Interface
    public virtual void Init()
    {

    }
    #endregion
}
