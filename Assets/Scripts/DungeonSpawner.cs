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

    public void SpawnEnemies(int dungeonLevel,Vector3 endPos)
    {
 
        RemoveEnemies();

        GameObject enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
        GameObject levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");

        switch (dungeonLevel)
        {
            case 1:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
 //               enemyToSpawn.GetComponent<Enemy>().charachterStats = new CharachterStats(dungeonLevel * 2, 1, 4, 7, 5, 4, 2);
                levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");

                break;
            case 2:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
  //              enemyToSpawn.GetComponent<Enemy>().charachterStats = new CharachterStats(dungeonLevel * 2, 1, 4, 7, 5, 4, 2);
                levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");

                break;
            case 3:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
                levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");
                break;
            case 4:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
                levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");
                break;
            case 5:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
                levelBoss = Resources.Load<GameObject>("Charachters/Level5/EnemyBoss");
                break;
            default:
                enemyToSpawn = Resources.Load<GameObject>("Charachters/EnemyCube");
                levelBoss = Resources.Load<GameObject>("Charachters/EnemyBoss");
                break;
        }

        enemyToSpawn.GetComponent<Enemy>().charachterStats = new CharachterStats(dungeonLevel * 2, 1, 4, 7, 5, 4, 2);
        levelBoss.GetComponent<Enemy>().charachterStats = new CharachterStats(dungeonLevel * 3, 1, 4, 7, 5, 4, 2);

        Debug.Log("Cave enemy");
        foreach ( BaseStat s in enemyToSpawn.GetComponent<Enemy>().charachterStats.stats)
        {
            Debug.Log(" : " + s.StatName + " : " + s.BaseValue);
        }
        Debug.Log("Cave Boss");
        foreach (BaseStat s in levelBoss.GetComponent<Enemy>().charachterStats.stats)
        {
            Debug.Log(" : " + s.StatName + " : " + s.BaseValue);
        }

        foreach (Vector3 p in SpawnPoints)
        {
            // drop prefab in location from here
       //     Enemies.Add(Instantiate(enemyToSpawn, p, Quaternion.identity));
            Instantiate(enemyToSpawn, p, Quaternion.identity);

            Debug.Log("Spawn(Enemy)- X:" + p.x + " Y:" + p.y  +" Z:" + p.z);
        }
        Instantiate(levelBoss, endPos, Quaternion.identity);

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
        // foreach (Enemy e in FindObjectsOfType(typeof(Enemy)) as Enemy[])
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (e.transform.position.y > (floorLevel + 2))
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
    public void RemovePickups()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Interactible Object"))
        {
            if (p.name == "PickUp(Clone)")
            { Destroy(p); }
        }
    }


}
