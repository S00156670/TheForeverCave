using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour {

    public static DungeonSpawner instance { get; set; }
    public List<Vector3> SpawnPoints { get; set; }



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
        //SpawnPoints = new List<Vector3>();


    }

    public void SpawnEnemies()
    {
        DungeonManager dungeon = GetComponent<DungeonManager>();

        foreach (Vector3 p in SpawnPoints)
        {
            Enemy e = new Enemy();
            // drop prefab in location from here
        }

    }


	// Update is called once per frame
	void Update () {
		
	}


}
