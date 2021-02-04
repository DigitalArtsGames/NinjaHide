using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelViewManager : MonoBehaviour
{
    [SerializeField] private int viewLevelsCount;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private LevelManager levelManager;

    public delegate void DrawDelegate();
    public event DrawDelegate drawEvent;

    public bool isEnded;

    private List<GameObject> childs;

    void Awake()
    {
        DrawLevels();
        drawEvent += DrawLevels;
        childs = GetChilds();
        
    }

    public void Start()
    {
    }

    private void Update()
    {
        CheckLevelsCount();
    }

    public void SetChilds()
    {
        childs = GetChilds();
        drawEvent -= SetChilds;
    }

    public void CheckLevelsCount()
    {
        var levelGroupStart = levelManager.currentLevelIndex - (levelManager.currentLevelIndex % 5);
        if (viewLevelsCount == levelGroupStart)
        {
            if(drawEvent != null)
            {
                drawEvent();
            }
        }
    }

    public void DrawLevels()
    {
        if(GetChilds() != null)
        {
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }
        }

        var levelGroupStart = levelManager.currentLevelIndex - (levelManager.currentLevelIndex % 5);

        for (int j = 0; j < viewLevelsCount; j++)
        {
            //if (levelManager.levelData.rooms.Length <= viewLevelsCount)
            //{
            levelPrefab.GetComponent<LevelComponent>().SetText((levelGroupStart + j + 1).ToString());

            Instantiate(levelPrefab, transform);
            //}

        }
        drawEvent -= DrawLevels;
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

    public GameObject GetLevelComponentByIndex(string index)
    {
        for (int i = 0; i < childs.Count; i++)
        {
            if(childs[i].GetComponent<LevelComponent>().TextMeshProUGUI.text == index)
            {
                return childs[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        drawEvent += SetChilds;
        if (childs.Count != 0 || GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()) != null)
        {
            LeanTween.scale(GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()), new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setLoopPingPong();
        }
    }

    private void OnDisable()
    {
        if (childs.Count != 0 || GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()) != null)
        {
            LeanTween.cancel(GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()));
        }
    }

}
