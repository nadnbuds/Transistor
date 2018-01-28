using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper for pooling objects of a specific game object. This is much faster than instantiating and destroying objects.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    /// <summary>
    /// What object are we pooling?
    /// </summary>
    [SerializeField]
    private GameObject _objToPool = null;
    public GameObject objToPool
    {
        get { return _objToPool; }
        private set { _objToPool = value; }
    }

    /// <summary>
    /// How many copies should we start with?
    /// </summary>
    [SerializeField]
    private int numberOfCopies = 1;

    /// <summary>
    /// Where to store pooled objects?
    /// </summary>
    [SerializeField]
    private Transform _storageArea = null;
    public Transform storageArea
    {
        get { return _storageArea; }
        private set { _storageArea = value; }
    }

    /// <summary>
    /// Number of these objects active
    /// </summary>
    public int numberOfActiveObjects
    {
        get
        {
            int count = 0;
            foreach (GameObject obj in pooledObjects)
            {
                if (obj.activeInHierarchy)
                {
                    count++;
                }
            }
            return count;
        }
    }

    /// <summary>
    /// Reference to all objects that this pool handles
    /// </summary>
    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        if (objToPool != null)
            InitPooler(objToPool, storageArea, numberOfCopies);

    }

    /// <summary>
    /// Initialize new pooler with a new object to pool, as well as where inactive objects are stored
    /// </summary>
    public void InitPooler(GameObject obj, Transform storage, int numb_objects)
    {
        if (obj == null)
        {
            Debug.LogError("Attempting to create pooler with a null obj. Object pool was not initialized.");
            return;
        }

        DestroyPooler();

        storageArea = storage;
        objToPool = obj;
        numberOfCopies = numb_objects;

        for (int i = 0; i < numberOfCopies; ++i)
        {
            GameObject new_obj = AddObjToPool();
            new_obj.SetActive(false);
        }
    }

    /// <summary>
    /// Retrieve a copy of an avaliable object from the pool
    /// </summary>
    public GameObject RetrieveCopy()
    {
        if (objToPool == null)
            return null;

        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        //All objects are currently in use, so create another.
        GameObject new_obj = AddObjToPool();
        new_obj.SetActive(true);
        return new_obj;

    }


    /// <summary>
    /// Deactivate all active objects that this pool handles
    /// </summary>
    public void DeactivateAll()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// Create a new obj, add to storage area and to the list. Return the new obj
    /// </summary>
    private GameObject AddObjToPool()
    {
        GameObject copy = Instantiate(objToPool) as GameObject;
        copy.transform.SetParent(storageArea, false);
        pooledObjects.Add(copy);
        return copy;
    }

    /// <summary>
    /// Destroys all preexisting objects managed by this pool
    /// </summary>
    private void DestroyPooler()
    {
        for (int i = pooledObjects.Count - 1; i >= 0; --i)
        {
            Destroy(pooledObjects[i]);
            pooledObjects.RemoveAt(i);
        }
        storageArea = null;
    }

}
