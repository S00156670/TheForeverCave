using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour {

    public static DungeonSpawner instance { get; set; }
    public List<Vector3> SpawnPoints { get; set; }
 //   DungeonManager dungeon { get; set; }


    // Use this for initialization
    void Start () {

        if (instance != null && instance != this)
        {
            // there can only be only one at a time
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        SpawnPoints = new List<Vector3>();
    }

    public void SpawnEnemies()
    {
      
      GameObject enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");

        foreach (Vector3 p in SpawnPoints)
        {
            // drop prefab in location from here

            Instantiate(enemyToSpawn, p, Quaternion.identity);

            Debug.Log("Spawn(Enemy)- X:" + p.x + " Y:" + p.y  +" Z:" + (p.z + 1));
        }

    }



}
