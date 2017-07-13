using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {


    //  public Plane ground;

 //   private MeshFilter filter;
 //   private MeshRenderer render;
 //   private BoxCollider collider;

    public GameObject ground { get; set; }

    public float[,] levelMap;

    public int levelSize;

    public int levelStage = 1;

    public Vector2 start;
    public Vector2 end;

    public List<Vector2> waypoints;

  //  Vector2 gridpoint;

    // it will probably be simpler to take an object which already has a nav mesh, modify and then update than to generate full nav mesh on the fly

    // Use this for initialization
    void Start () {
        // advice?
        //https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html
        //       GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //     ground = plane;


        //// plan level

        //levelMap = new int[20,20];
        //// set all to zero
        //foreach (int x in levelMap)
        //{ x = 0; }

        //// enter = 1
        //levelMap[0, Random.Range(0, levelMap.GetLength.)] = 1;
        //// exit = 2
        //levelMap[levelMap.GetLength, Random.Range(0, levelMap.GetLength)] = 2;
        //// pathing waypoint = 4
        //levelMap[Random.Range(5, levelMap.GetLength - 5), Random.Range(5, levelMap.GetLength - 5)] = 4;
        //// path = 3




        // enter and exit
        // in between


        // edit mesh



        GenerateLevelMap();


        // calculate nav mesh
        // spawn

    }

    private void GenerateLevelMap()
    {

        // throw new NotImplementedException();

        int rand;

        levelSize = levelStage * 20;

        levelMap = new float[levelSize,levelSize];

        // set all to zero
        //foreach (int x in levelMap)
        //{ x = 0; }
        for (int x = 0; x < levelSize; x++)
        {
            for (int y = 0; y < levelSize; y++)
            {
                levelMap[x, y] = 0;
            }
        }

        // enter = 1
        //levelMap[0, Random.Range(0, levelMap.GetLength.)] = 1;

        rand = UnityEngine.Random.Range(0, levelSize);
        start = new Vector2(0,rand);
        levelMap[0, rand] = 1;

        //     start = new Vector2(0, UnityEngine.Random.Range(0, levelSize));
        //      MapVector(start) = 0;




        // exit = 2
        //      levelMap[levelMap.GetLength, Random.Range(0, levelMap.GetLength)] = 2;
        //    levelMap[levelSize, UnityEngine.Random.Range(0, levelSize)] = 2;
        rand = UnityEngine.Random.Range(0, levelSize);
        end = new Vector2(levelSize, rand);
        levelMap[levelSize, rand] = 2;

        // pathing waypoint = 4
        //        levelMap[UnityEngine.Random.Range(5, levelSize - 5), UnityEngine.Random.Range(5, levelSize - 5)] = 4;
        rand = UnityEngine.Random.Range(5, levelSize - 5);
        Vector2 way = new Vector2();
        way.x = rand;
        rand = UnityEngine.Random.Range(5, levelSize - 5);
        way.y = rand;

        levelMap[Convert.ToInt32(way.x), Convert.ToInt32(way.y)] = 4;


        // path = 3

        // connection caves


        // spread a bit for start and end
        foreach (Vector2 item in NeighbouringSections(Convert.ToInt32(start.x), Convert.ToInt32(start.y)))
        {
            if (levelMap[Convert.ToInt32(item.x), Convert.ToInt32(item.y)] != null)
            {
            levelMap[Convert.ToInt32(item.x), Convert.ToInt32(item.y)] = 4;
            }



        }



        // add random dead ends

        // spawn

    }

    private float  MapVector(Vector2 position)
    {
        int x = Convert.ToInt32(position.x);
        int y = Convert.ToInt32(position.y);

        return levelMap[x,y];
    }

    // Update is called once per frame
    void Update () {
	
        // check for enter/exit

	}

    public bool Check(int checkX, int checkY , int value)
    {
        if (levelMap[checkX, checkY] == value)
            return true;
        else
            return false;
    }

    public bool CheckBeside(int checkX, int checkY, int value)
    {

        //Check(checkX - 1, checkY, value);

        //Check(checkX + 1, checkY, value);

        //Check(checkX, checkY - 1, value);

        //Check(checkX, checkY + 1, value);

        // good for culling with if value = 0 and checkbeside(xy0) == true then will not need to be part of level

        //if (Check(checkX - 1, checkY, value) &&
        //    Check(checkX + 1, checkY, value) &&
        //    Check(checkX, checkY - 1, value) &&
        //    Check(checkX, checkY + 1, value))
        //    return true;
        //else
        //    return false;

        bool check = true;

        foreach (Vector2 item in NeighbouringSections( checkX, checkY))
        {
            if (Check( checkX,  checkY,  value) == false)
            {
                check = false;
            }
        }
        return check;
    }

    public List<Vector2> NeighbouringSections(int checkX, int checkY)
    {

        //!! SHOULD HAVE A CHECK FOR IF NEIGHBOUR IS WITHIN WORLD BOUNDS BEFORE ADDING

        List<Vector2> neighboures = new List<Vector2>();
        neighboures.Add(new Vector2(checkX - 1, checkY));
        neighboures.Add(new Vector2(checkX + 1, checkY));
        neighboures.Add(new Vector2(checkX, checkY - 1));
        neighboures.Add(new Vector2(checkX, checkY + 1));
        //Check();
        //Check();
        //Check(, value);
        return neighboures;

    }


}
