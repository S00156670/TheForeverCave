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
        TreasurePoints = new List<Vector3>();
    }

    public void SpawnEnemies()
    {
 
        RemoveEnemies();

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
  //          Instantiate(chest, p, Quaternion.identity);
  // must build chest prefab
            Debug.Log("Spawn(Chest)- X:" + p.x + " Y:" + p.y + " Z:" + p.z);
        }

    }

    public void TrimEnemies(float floorLevel)
    {
        foreach (Enemy e in FindObjectsOfType(typeof(Enemy)) as Enemy[])
        {
            if (e.transform.position.y > (floorLevel + 1))
            {
                Destroy(e);
            }
        }
    }


    public void RemoveEnemies()
    {
        // // leaves dud cubes
        //foreach (Enemy e in FindObjectsOfType(typeof(Enemy)) as Enemy[])
        //{
        //    Destroy(e);
        //}
 
        
        //       GameObject.FindGameObjectsWithTag("Enemy")
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(e);
        }
    }



}
