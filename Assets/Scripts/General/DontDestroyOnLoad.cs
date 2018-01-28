using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        
        Scene dont_destroy = SceneManager.GetSceneByName("DontDestroyOnLoad");

        if (dont_destroy.isLoaded)
        {
            GameObject[] goArray = dont_destroy.GetRootGameObjects();
            if (goArray.Length > 0)
            {
                foreach (GameObject go in goArray)
                {
                    print(go.name + " " + gameObject.name);
                    if (go.name == gameObject.name)
                        DestroyImmediate(gameObject);
                }
            }
        }
        
        DontDestroyOnLoad(this);
    }
}
