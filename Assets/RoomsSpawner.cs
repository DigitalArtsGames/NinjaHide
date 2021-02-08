using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomsSpawner : MonoBehaviour
{

    List<Room> spawnedChunks = new List<Room>();
    [SerializeField] private Transform firstRoomPointSpawner;

    public void SpawnRoomsRandomly(Room[] rooms)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i] = Instantiate(rooms[Random.Range(0, rooms.Length)]);
            if (spawnedChunks.Count == 0)
            {
                rooms[i].transform.position = firstRoomPointSpawner.position - rooms[i].begin.localPosition;
            }
            else
            {
                rooms[i].transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - rooms[i].begin.localPosition;
            }
            spawnedChunks.Add(rooms[i]);
        }
    }

    public void ClearRooms(Room[] rooms)
    {
        spawnedChunks.Clear();
        List<GameObject> sl = GameObject.FindGameObjectsWithTag("Room").ToList();
        
        rooms.ToList().Clear();
        
        for (int i = 0; i < sl.Count; i++)
        {
            Destroy(sl[i]);
        }
    }
}
