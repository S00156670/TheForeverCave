using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour {

    public static DungeonSpawner instance { get; set; }
    public List<Vector3> SpawnPoints { get; set; }
    public List<Vector3> TreasurePoints { get; set; }
    //   public List<GameObject> Enemies { get; set; }


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
       //     Enemies.Add(Instantiate(enemyToSpawn, p, Quaternion.identity));
            Instantiate(enemyToSpawn, p, Quaternion.identity);

            Debug.Log("Spawn(Enemy)- X:" + p.x + " Y:" + p.y  +" Z:" + p.z);
        }

    }


    public void SpawnTreasure()
    {

        GameObject chest = Resources.Load<GameObject>("Prefabs/Chest");

        foreach (Vector3 p in TreasurePoints)
        {
            // drop prefab in location from here
            //     Enemies.Add(Instantiate(enemyToSpawn, p, Quaternion.identity));
            Instantiate(chest, p, Quaternion.identity);

            Debug.Log("Spawn(Enemy)- X:" + p.x + " Y:" + p.y + " Z:" + p.z);
        }

    }

    public void TrimEnemies()
    {
        //call spawnchance from a more refined subset of "middle-path" sections
        //(Best option, this method not needed)
        // or
        // foreach enemy in scene
        // if e.y is above dungeonFloor.y
        // destroy(e)
    }

}
