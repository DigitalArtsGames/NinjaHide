using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLevelsManager : MonoBehaviour
{
    public List<GameObject> levelComponents;
    public LevelManager levelManager;

    public delegate void DrawDelegate();
    public event DrawDelegate drawEvent;

    public delegate void GlowDelegate();
    public event GlowDelegate glowEvent;

    public bool hasActivated;

    public void Awake()
    {
        DrawLevels(); 
        drawEvent += DrawLevels;

        glowEvent += StartGlow;
    }

    public void Update()
    {
        CheckLevelsCount();
    }

    public void CheckLevelsCount()
    {
        var levelGroupStart = levelManager.currentLevelIndex - (levelManager.currentLevelIndex % 5);
        if (levelComponents.Count == levelGroupStart)
        {
            if (drawEvent != null)
            {
                drawEvent();
                glowEvent();
                hasActivated = true;
            }
        }
    }

    public void DrawLevels()
    {
        var levelGroupStart = levelManager.currentLevelIndex - (levelManager.currentLevelIndex % 5);

        for (int j = 0; j < levelComponents.Count; j++)
        {
            levelComponents[j].GetComponent<LevelComponent>().SetText((levelGroupStart + j + 1).ToString());
        }

        //glowEvent();
        drawEvent -= DrawLevels;
    }

    public GameObject GetLevelComponentByIndex(string index)
    {
        for (int i = 0; i < levelComponents.Count; i++)
        {
            if (levelComponents[i].GetComponent<LevelComponent>().TextMeshProUGUI.text == index)
            {
                return levelComponents[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        glowEvent();
    }

    private void OnDisable()
    {
        StopGlow();
    }

    public void StartGlow()
    {
        if (levelComponents.Count != 0 && GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()) != null)
        {
            LeanTween.scale(GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()), new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setLoopPingPong();
        }
    }

    private void StopGlow()
    {
        if (levelComponents.Count != 0 && GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()) != null)
        {
            LeanTween.cancel(GetLevelComponentByIndex((levelManager.currentLevelIndex + 1).ToString()));
        }
    }
}
