using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelViewManager : MonoBehaviour
{
    [SerializeField] private int viewLevelsCount;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private LevelManager levelManager;

    private List<GameObject> childs;

    void Awake()
    {
        DrawLevels();
        childs = GetChilds();

    }


    private void Update()
    {

    }

    public void DrawLevels()
    {

        for (int j = 0; j < levelManager.levelData.rooms.Length; j++)
        {
            if (levelManager.levelData.rooms.Length <= viewLevelsCount)
            {
                levelPrefab.GetComponent<LevelComponent>().SetText((j + 1).ToString());

                Instantiate(levelPrefab, transform);
            }
        }
    }

    public List<GameObject> GetChilds()
    {
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childs.Add(gameObject.transform.GetChild(i).gameObject);
        }
        return childs;
    }



    private void OnEnable()
    {
        if(childs.Count != 0)
        {
            LeanTween.scale(childs[levelManager.currentLevelIndex], new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setLoopPingPong();
        }
    }

    private void OnDisable()
    {
        if (childs.Count != 0)
        {
            LeanTween.cancel(childs[levelManager.currentLevelIndex]);
        }
    }

}
