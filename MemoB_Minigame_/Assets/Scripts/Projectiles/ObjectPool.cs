using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string,Queue<GameObject>>();
    private GameObject pool;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)//若对象池字典中还没有此预制体或者可用预制体数量为0
        {
            _object=GameObject.Instantiate(prefab);//实例化该物体
            PushObject(_object);//将该预制体入字典内的队列方便管理
            if (pool == null)//如果对象池的GameObject还没有被创建
                pool = new GameObject("ObjectPool");//则创建一个对象池
            GameObject childPool = GameObject.Find(prefab.name + "Pool");//寻找预制体的父物体池
            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");//若没有则新建一个
                childPool.transform.SetParent(pool.transform);//将该池设定在父对象池之下
            }
            _object.transform.SetParent(childPool.transform);//将新的预制体设置在子对象池之下
        }
        _object = objectPool[prefab.name].Dequeue();//从队列中取出对象
        _object.SetActive(true);
        return _object;
    }
    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
