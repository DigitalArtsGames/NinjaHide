using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerScript : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPoolerScript Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        //Словарь всех "бассейнов", переменная стринг отвечает за название бассейна, вторая за сам бассейн
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        GameObject poolerContainer = new GameObject();
        poolerContainer.name = "Pooler Container";
        //Instantiate(poolerContainer);
        //Цикл проходться по всем бассейнам
        foreach (Pool pool in pools)
        {
            //Создает "бассейн" объектов
            Queue<GameObject> objectPool = new Queue<GameObject>();


            //Наполняет бассейн объектами
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, poolerContainer.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            //добавляет бассейн в словарь бассейнов
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag + " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
