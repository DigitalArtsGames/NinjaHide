using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBilder : MonoBehaviour
{
    public LevelManager levelManager;
    public PlayerScript playerPrefab;

    private LevelData levelData;
    public GameObject splineManagerPrefab;
    
    private List<Room> spawnedRooms = new List<Room>();
    private Room[] rooms;

    public delegate void SpawnLevelDelegate();
    public event SpawnLevelDelegate spawnLevelEvent;

    private SplineManager splineManager;
    private GameObject levelParent;

    public void Start()
    {
        spawnLevelEvent += SpawnLevel;
    }

    public void Update()
    {
        CheckCurrentLevel();
    }

    public void CheckCurrentLevel()
    {
        if(levelManager.currentLevel != null)
        {
            if(levelData == null)
            {
                levelData = levelManager.currentLevel;
                spawnLevelEvent();
            }
        }
    }

    public void SpawnLevel()
    {
        levelParent = new GameObject();
        levelParent.name = "Level";
        rooms = levelData.rooms;
        spawnedRooms.Add(rooms[Random.Range(0, rooms.Length)]);
        for (int i = 0; i < rooms.Length; i++)
        {
            SpawnRandomRoom();
        }
        GameObject player = Instantiate(playerPrefab.gameObject);
        player.transform.position = spawnedRooms[0].begin.position;
        SetSplineManager();
        player.GetComponent<SplineWalker>().splineManager = splineManager;
    }

    public void SpawnRandomRoom()
    {
        Room newRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], levelParent.transform);
        newRoom.transform.position = spawnedRooms[spawnedRooms.Count - 1].end.position - newRoom.begin.localPosition;
        spawnedRooms.Add(newRoom);
    }

    public void SetSplineManager()
    {
        splineManagerPrefab = Instantiate(splineManagerPrefab, levelParent.transform);
        splineManager = splineManagerPrefab.GetComponent<SplineManager>();

        for (int i = 0; i < rooms.Length; i++)
        {
            splineManager.splines[i] = rooms[i].playerSpline;
        }

        splineManager.playerSplineWalker = playerPrefab.GetComponent<SplineWalker>();
    }
}
