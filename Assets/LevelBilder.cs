using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBilder : MonoBehaviour
{
    public LevelData levelData;

    private List<Room> spawnedRooms = new List<Room>();


    private Room[] rooms;
    private List<Room> spawnedRooms = new List<Room>();
    public void Start()
    {
        rooms = levelData.rooms;
        rooms = levelData.rooms;
        spawnedRooms.Add(rooms[Random.Range(0, rooms.Length)]);
        for (int i = 0; i < rooms.Length; i++)
        {
            SpawnRandomRoom();
        }
    }

    public void SpawnRandomRoom()
    {
        Room newRoom = Instantiate(rooms[Random.Range(0, rooms.Length)]);
        newRoom.transform.position = spawnedRooms[spawnedRooms.Count - 1].end.position - newRoom.begin.localPosition;
        spawnedRooms.Add(newRoom);
    }
}
