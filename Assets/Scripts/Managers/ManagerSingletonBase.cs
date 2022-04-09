using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingletonBase<T> : MonoBehaviour where T: Component
{    
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject coreGameObject = new GameObject(typeof(T).Name);
                instance = coreGameObject.AddComponent<T>();
            }

            return instance;
        }
    }
 
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
              DestroyImmediate(this.gameObject);
        else
        {
            instance = GetComponent<T>();;
            DontDestroyOnLoad(instance);
        }
    }
}
