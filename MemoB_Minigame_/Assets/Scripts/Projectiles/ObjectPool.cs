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
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)//��������ֵ��л�û�д�Ԥ������߿���Ԥ��������Ϊ0
        {
            _object=GameObject.Instantiate(prefab);//ʵ����������
            PushObject(_object);//����Ԥ�������ֵ��ڵĶ��з������
            if (pool == null)//�������ص�GameObject��û�б�����
                pool = new GameObject("ObjectPool");//�򴴽�һ�������
            GameObject childPool = GameObject.Find(prefab.name + "Pool");//Ѱ��Ԥ����ĸ������
            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");//��û�����½�һ��
                childPool.transform.SetParent(pool.transform);//���ó��趨�ڸ������֮��
            }
            _object.transform.SetParent(childPool.transform);//���µ�Ԥ�����������Ӷ����֮��
        }
        _object = objectPool[prefab.name].Dequeue();//�Ӷ�����ȡ������
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
