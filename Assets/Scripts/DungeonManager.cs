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

    public Vector2 levelStart;
    public Vector2 levelEnd;

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

        levelSize = levelStage + 12;

        levelMap = new float[levelSize,levelSize];

        // set all to zero
        //foreach (int x in levelMap)
        //{ x = 0; }
        for (int x = 0; x < levelSize - 1; x++)
        {
            for (int y = 0; y < levelSize - 1; y++)
            {
                levelMap[x, y] = 0;
            }
        }

        // enter = 1
        //levelMap[0, Random.Range(0, levelMap.GetLength.)] = 1;

        rand = UnityEngine.Random.Range(0, levelSize - 1);
        levelStart = new Vector2(0,rand);
        levelMap[0, rand] = 1;

        //     levelStart = new Vector2(0, UnityEngine.Random.Range(0, levelSize));
        //      MapVector(levelStart) = 0;


        // exit = 2
        //      levelMap[levelMap.GetLength, Random.Range(0, levelMap.GetLength)] = 2;
        //    levelMap[levelSize, UnityEngine.Random.Range(0, levelSize)] = 2;
        rand = UnityEngine.Random.Range(0, levelSize - 1);
        levelEnd = new Vector2(levelSize - 1, rand);
        levelMap[levelSize - 1, rand] = 2;

        // pathing waypoint = 4
        //        levelMap[UnityEngine.Random.Range(5, levelSize - 5), UnityEngine.Random.Range(5, levelSize - 5)] = 4;
        rand = UnityEngine.Random.Range(5, levelSize - 5);
        Vector2 way = new Vector2();
        way.x = rand;
        rand = UnityEngine.Random.Range(5, levelSize - 5);
        way.y = rand;

        levelMap[Convert.ToInt32(way.x), Convert.ToInt32(way.y)] = 4;

        // connection tunnels
        // path = 3
        AddPath(levelStart, way);
        AddPath(way, levelEnd);

        // spread a bit for levelStart and levelEnd
        ////foreach (Vector2 item in NeighbouringSections(Convert.ToInt32(levelStart.x), Convert.ToInt32(levelStart.y)))
        ////{
        //////        if (levelMap[Convert.ToInt32(item.x), Convert.ToInt32(item.y)] != null)
        //////        {
        ////    levelMap[Convert.ToInt32(item.x), Convert.ToInt32(item.y)] = 3;
        //////      }
        ////}
        ////foreach (Vector2 item in NeighbouringSections(Convert.ToInt32(levelStart.x), Convert.ToInt32(levelStart.y)))
        ////{
        ////    levelMap[Convert.ToInt32(item.x), Convert.ToInt32(item.y)] = 3;
        ////}


        // spawn
    }

    private float  MapVector(Vector2 position)
    {
        int x = Convert.ToInt32(position.x);
        int y = Convert.ToInt32(position.y);

        return levelMap[x,y];
    }

    // Update is called once per frame
    void Update ()
    {
	        // check for enter/exit
	}


    private void AddPath(Vector2 pathStart, Vector2 pathEnd)
    {
        foreach (Vector2 pathSection in GetPath( pathStart,  pathEnd))
        {
            levelMap[Convert.ToInt32(pathSection.x), Convert.ToInt32(pathSection.y)] = 3;
        }
    }

    public List<Vector2> GetPath(Vector2 pathStart, Vector2 pathEnd)
    {
        List<Vector2> path = new List<Vector2>();
        Vector2 pathSection = pathStart;

        // make more random

        while (pathSection != pathEnd)
        {
            if (pathSection.y < pathEnd.y)
            {
                // going up
                pathSection.y++;
            }
            else
            {
                // going down
                pathSection.y--;
            }

            // turn

            if (pathSection.x == pathEnd.x)
            {
                if (pathSection.x < pathEnd.x)
                {
                    // going right
                    pathSection.x++;
                }
                else
                {
                    // going left
                    pathSection.x--;
                }
            }

            // add random dead ends

            path.Add(pathSection);

        }

        return path;
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


        List<Vector2> neighboures = new List<Vector2>();

        if (checkX > 0)
        {
        neighboures.Add(new Vector2(checkX - 1, checkY));
        }

        if (checkX < levelSize)
        {
        neighboures.Add(new Vector2(checkX + 1, checkY));
        }

        if (checkY > 0)
        {
            neighboures.Add(new Vector2(checkX, checkY - 1));
        }

        if (checkY < levelSize)
        {
            neighboures.Add(new Vector2(checkX, checkY + 1));
        }

        return neighboures;

    }


}
