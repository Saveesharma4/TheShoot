using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Queue<GameObject> poolObjects = new Queue<GameObject>();

    public GameObject _poolObject; 

    public ObjectPool(GameObject poolObject, int initialPoolSize, Transform container = null)
    {

        _poolObject = poolObject;


        if (container == null)
        {
            GameObject containerObject = new GameObject(poolObject.name +"PoolContainer");
            container = containerObject.transform;
        }

        for(int i =0; i<initialPoolSize; i++)
        {
            GameObject gameObject = GameObject.Instantiate(poolObject); 
            gameObject.SetActive(false);
            gameObject.transform.SetParent(container); 
            poolObjects.Enqueue(gameObject);
        }
    }


    public GameObject GetGameObject(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GetGameObject(); 
        gameObject.transform.position = position;   
        gameObject.transform.rotation = rotation;
        return gameObject;
    }

    public GameObject GetGameObject()
    {

        if (poolObjects.Count > 0)
        {
            GameObject gameObject =  poolObjects.Dequeue();   
            gameObject.SetActive(true); 
            return gameObject;
        }
        else
        {
            return GameObject.Instantiate(_poolObject); 
        }

    }


    public void RePoolGameObject(GameObject gameObject)
    {
        if(gameObject == null)
        {
            return;
        }

        gameObject.SetActive(false);
        poolObjects.Enqueue(gameObject);        
    }
}
