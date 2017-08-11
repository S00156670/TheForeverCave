using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class DungeonManager : MonoBehaviour {


    //  public Plane ground;

 //   private MeshFilter filter;
 //   private MeshRenderer render;
 //   private BoxCollider collider;

    public GameObject ground { get; set; }

  //  public float[,] levelMap;

    private List<Vector2> walkableArea;



    private int levelStage;

    public float sectionSize = 50;

    private int levelSize;

    private Vector2 levelStart;
    private Vector2 levelEnd;

    private List<Vector2> waypoints;


    ////public static DungeonManager instance { get; set; }

    private MeshFilter filter;
    private MeshRenderer renderer;
    // private BoxCollider collider;

    Player player;
    

    bool inCave = false;
    public Vector3 startPos;
    //  Vector2 gridpoint;

    // it will probably be simpler to take an object which already has a nav mesh, modify and then update than to generate full nav mesh on the fly

    // Use this for initialization

    public DungeonSpawner currentSpawn;

    void Awake ()
    {
        levelStage = 0;

        currentSpawn = new DungeonSpawner();


        GenerateCave();

        player = GameObject.Find("Player").GetComponent<Player>();
       
        //     ////// singelton
        //     ////if (instance != null && instance != this)
        //     ////{
        //     ////    Destroy(gameObject);
        //     ////    // there can only be only one
        //     ////}
        //     ////else
        //     ////{
        //     ////    instance = this;
        //     ////}



        //     // advice?
        //     //https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html
        //     //       GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //     //     ground = plane;


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
            GenerateCave();
            }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // go to waypoint island
            UnityEngine.AI.NavMeshAgent navAgent;
            navAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();

            //navAgent.transform.position = new Vector3(12.6f, 4, 32.1f);
            //navAgent.SetDestination(new Vector3(12.6f, 4, 32.1f));

            navAgent.Warp(new Vector3(12.6f, 4, 32.1f));
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UnityEngine.AI.NavMeshAgent navAgent;

            navAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (!inCave)
            {// go to cave start

                Vector3 caveStart = this.transform.position + new Vector3((-sectionSize * 0.5f + sectionSize * (levelStart.x)),
                                                                            0.1f,
                                                                        (-sectionSize * 0.5f + sectionSize * (levelStart.y)));
                Debug.Log("cave start translate at " + caveStart.x + "," + caveStart.y + "," + caveStart.z );

                //         player.transform.position = caveStart;

                //navAgent.transform.position = caveStart;
                //navAgent.SetDestination(caveStart);
                navAgent.Warp(caveStart);

                //    navAgent.SetDestination(caveStart);
                //       navAgent.destination = caveStart;
                // player.GetComponent<NavMeshAgent>().SetDestination(caveStart);

                inCave = true;

            }
            else
            {
                // send palyer back to campsite
                //navAgent.transform.position = new Vector3(-6,4,1);
                //navAgent.SetDestination(new Vector3(-6, 4, 1));
                navAgent.Warp(new Vector3(-6, 4, 1));
                inCave = false;
            }


        }


        // check for victory condition here?


    }

    private void GenerateCave()
    {

        currentSpawn = new DungeonSpawner();
        currentSpawn.SpawnPoints = new List<Vector3>();

        //// plan level

        levelStage++;

        if (levelStage > 5)
            levelStage = 1;

        levelSize = (levelStage * 16) + 32;

        // decide pathing
        GenerateLevelMap();

        Debug.Log("level size is : " + levelSize);

        filter = GetComponent<MeshFilter>();
        // generate cave geometry
        filter.mesh = GenerateMesh();

        // re-orientate to line up with off-mesh-link
        transform.position = new Vector3(transform.position.x, transform.position.y, 32 - startPos.z);

        // spawn enemies and treasure chests
        // Spawn()
        Debug.Log("SPAWN ENEMY ATTEMPT");
        currentSpawn.SpawnEnemies();

        // might need to postpone this so that all enemies have had a chance to fall into place
        currentSpawn.TrimEnemies();
    }

    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        // set vertex positions

        var verticies = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int x = 0; x < levelSize + 1; x++)
        {
            for (int y = 0; y < levelSize + 1; y++)
            {
                // could put an if{}else{} here for altitude based on weather or not vertex is in path list

                bool path = false;

                if (walkableArea != null)// this check is just for while debugging, in final build there should always be a walkable area
                {


                foreach (Vector2 pathCheck  in walkableArea)
                {
                    if (pathCheck == new Vector2(x,y))
                    {
                        path = true;
                    }
                }


                }


                if (path)
                {
                    verticies.Add(new Vector3(-sectionSize * 0.5f + sectionSize * (x /*/ ((float)levelSize)*/),
                                                0,
                                             -sectionSize * 0.5f + sectionSize * (y /*/ ((float)levelSize)*/)));

                    if (levelStart.x == x && levelStart.y == y)
                    {
                        startPos = (new Vector3(-sectionSize * 0.5f + sectionSize * (x),
                                                0,
                                             -sectionSize * 0.5f + sectionSize * (y)));
                    }

                    SpawnlistChance
                        (
                        new Vector3(-sectionSize * 0.5f + sectionSize * (x),
                                    2,
                                    -sectionSize * 0.5f + sectionSize * (y))  
                       + new Vector3(transform.position.x, transform.position.y, 32 - startPos.z)
                        );

                }
                else
                {
                    verticies.Add(new Vector3(-sectionSize * 0.5f + sectionSize * (x ),
                            sectionSize * 2,
                         -sectionSize * 0.5f + sectionSize * (y )));



                }


                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)levelSize ,y / (float)levelSize));



            }
        }


        //mesh.SetVertices(new List<Vector3>
        //{
        //    new Vector3(- sectionSize * 0.5f,0,- sectionSize * 0.5f),
        //    new Vector3( sectionSize * 0.5f,0,- sectionSize * 0.5f),
        //    new Vector3( sectionSize * 0.5f,0, sectionSize * 0.5f),
        //    new Vector3(- sectionSize * 0.5f,0, sectionSize * 0.5f)
        //});

        
        //// group vertexes into triangles
        var triangles = new List<int>();
        // (levelSize + 1 ) is number of verts in a line
        for (int i = 0; i < ((levelSize + 1) * (levelSize + 1 ) - (levelSize + 1)); i++)
   //         for (int i = 0; i < ((levelSize - 1) * (levelSize - 1) / 2); i++)
        {
            if ((i + 1) % (levelSize + 1) == 0)
            {
                continue;
            }


            triangles.AddRange(new List<int>()
            {
                    i + 1 + levelSize + 1,i + levelSize + 1, i,
                    i, i + 1, i + levelSize + 1 + 1
            });
        }

        mesh.SetVertices(verticies);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles,0);



        //// quad data
        //mesh.SetTriangles(new List<int>()
        //    {
        //    3,1,0,
        //    3,2,1
        //    }, 
        //    0); // no submesh

        // set normal for each vertex
        //mesh.SetNormals(new List<Vector3>
        //{
        //    Vector3.up,
        //    Vector3.up,
        //    Vector3.up,
        //    Vector3.up
        //});

        //mesh.SetUVs(0, new List<Vector2>()
        //{
        //    new Vector2(0,0),
        //    new Vector2(1,0),
        //    new Vector2(1,1),
        //    new Vector2(0,1),
        //});

        return mesh;
    }

    private void SpawnlistChance(Vector3 point)
    {
        if (UnityEngine.Random.Range(0, 100/*levelSize*/) >= 95)
        {
            // spawn loction
            currentSpawn.SpawnPoints.Add(point);

            //currentSpawn.SpawnPoints.Add(new Vector3(
            //                -sectionSize * 0.5f + sectionSize * (point.x),
            //                sectionSize * 2,
            //                -sectionSize * 0.5f + sectionSize * (point.y))
            //                );

            Debug.Log("Adding Spawn(ENEMY)");

        }

    }

    private void GenerateLevelMap()
    {
        Debug.Log("attempting level map");

        int rand;

        walkableArea = new List<Vector2>();

   //     levelMap = new float[levelSize,levelSize];

        // enter = 1
        //levelMap[0, Random.Range(0, levelMap.GetLength.)] = 1;

        rand = UnityEngine.Random.Range(2, levelSize - 2);
        levelStart = new Vector2(2,rand);
        //        levelMap[0, rand] = 1;
        walkableArea.Add(levelStart);

        Debug.Log("start position set : " + levelStart.x + " , " + levelStart.y);

        //     levelStart = new Vector2(0, UnityEngine.Random.Range(0, levelSize));
        //      MapVector(levelStart) = 0;



        ////// pathing waypoint = 4
        //////        levelMap[UnityEngine.Random.Range(5, levelSize - 5), UnityEngine.Random.Range(5, levelSize - 5)] = 4;
        ////rand = UnityEngine.Random.Range(8, levelSize - 8);
        ////Vector2 way = new Vector2();
        ////way.x = rand;
        ////rand = UnityEngine.Random.Range(8, levelSize - 8);
        ////way.y = rand;
        //////    levelMap[Convert.ToInt32(way.x), Convert.ToInt32(way.y)] = 4;
        ////// will need to be able to generate multiple waypoints
        ////walkableArea.Add(way);
        ////Debug.Log("waypoint position set : " + way.x + " , " + way.y);


        // adding waypoints
        for (int i = 0; i < levelStage; i++)
        {   
            Vector2 waypoint;
            rand = UnityEngine.Random.Range((16 * i) + 8, (16 * i) + 24);
            waypoint.x = rand;
            waypoint.y = UnityEngine.Random.Range(10, levelSize - 10);
            walkableArea.Add(waypoint);
            Debug.Log("waypoint position set : " + waypoint.x + " , " + waypoint.y);
        }


        // exit = 2
        //      levelMap[levelMap.GetLength, Random.Range(0, levelMap.GetLength)] = 2;
        //    levelMap[levelSize, UnityEngine.Random.Range(0, levelSize)] = 2;
        rand = UnityEngine.Random.Range(2, levelSize - 2);
        //   Debug.Log("Levelsize is" + levelSize);
        levelEnd = new Vector2(/*(levelStage * 16)*/ levelSize - 2, rand);
        walkableArea.Add(levelEnd);
        Debug.Log("end position set : " + levelEnd.x + " , " + levelEnd.y);
        //       levelMap[levelSize - 1, rand] = 2;



        for (int i = 0; i < (levelStage + 1); i++)
        {
            AddPath(walkableArea[i],walkableArea[i+1]);
        }



        if (levelStage == 1)
        {
            AddDiversion(walkableArea[1]);
            GenerateRoom((walkableArea[1]),4);
            currentSpawn.TreasurePoints.Add(walkableArea[1]);
        }
        else
        {
            for (int i = levelStage + 10; i < walkableArea.Count - 2; i+= 4)// bad way to loop. seperate the count maybe?
            {
                //UnityEngine.Random.Range(0, 10);
                if (UnityEngine.Random.Range(0, 50) <= 2)
                {
                    AddDiversion(walkableArea[i]);
                }
            }
        }
        ////// connection tunnels
        ////// path = 3
        ////AddPath(levelStart, way);
        ////AddPath(way, levelEnd);







        // spread a bit of extraspace for levelStart and levelEnd
        WidenAround(levelStart);
        WidenAround(levelEnd);
        // to make space for boss fight
        GenerateRoom(levelEnd,6);
    }

    private void WidenAround(Vector2 point)
    {
        foreach (Vector2 j in NeighbouringSections(Convert.ToInt32(point.x), Convert.ToInt32(point.y)))
        {
            if (CheckPath(j))
                walkableArea.Add(j);

            foreach (Vector2 k in NeighbouringSections(Convert.ToInt32(j.x), Convert.ToInt32(j.y)))
            {
                if (CheckPath(k))
                    walkableArea.Add(k);
                //is 3 times too much?
                foreach (Vector2 l in NeighbouringSections(Convert.ToInt32(k.x), Convert.ToInt32(k.y)))
                {
                    if (CheckPath(l))
                        walkableArea.Add(l);
                }
            }
        }
    }

    private void GenerateRoom(Vector2 point, float size)
    {
        //// in case of odd sizes
        //     double halfSize = Math.Round((size / 2), 0);

        Vector2 startCorner = point - new Vector2(size,size);
        Vector2 endCorner = point + new Vector2(size, size);

        Vector2 floorPoint = startCorner;

        while (floorPoint.x <= endCorner.x && floorPoint.y <= endCorner.y)
        {
            // make sure point is within the map
            if (floorPoint.x < levelSize && floorPoint.x > 0 && floorPoint.y < levelSize && floorPoint.y > 0)
                if (CheckPath(floorPoint))
                    walkableArea.Add(floorPoint);
            
            if (floorPoint.x == endCorner.x)
            {
                floorPoint.x = startCorner.x;
                floorPoint.y++;
            }
            else
                floorPoint.x++;
        }

    }

    private void AddDiversion(Vector2 pathSection)
    {
        Debug.Log("ADDING A DETOUR TO MAP");
        Vector2 detour;
        detour.x = pathSection.x;

        double midpoint = (levelSize / 2);
        midpoint = Math.Round(midpoint,0);
        Debug.Log("Level midpoint" + midpoint);

        //      if (pathSection.y < (levelSize / 2))
        if (pathSection.y < midpoint)
        {
            //   detour.y = UnityEngine.Random.Range(pathSection.y, levelSize);
            double DetY = UnityEngine.Random.Range(pathSection.y,( levelSize - 1));
            DetY = Math.Round(DetY, 0);
            detour.y = Convert.ToSingle(DetY);
        }
        else
        {
            //   detour.y = UnityEngine.Random.Range(0, pathSection.y);
            double DetY = UnityEngine.Random.Range(1, pathSection.y);
            DetY = Math.Round(DetY, 0);
            detour.y = Convert.ToSingle(DetY);
        }

        //    if (detour != null)
        //    {
            walkableArea.Add(detour);

        AddPath(pathSection,detour);
      
            Debug.Log("detour added to walkable area : " + detour.x + "_" + detour.y + "_");

        if (UnityEngine.Random.Range(0, 10) > 8)
        {
            GenerateRoom(detour, 5);
        }

        if (UnityEngine.Random.Range(0, 10) > 5)
        {
            currentSpawn.TreasurePoints.Add(detour);
        }


        //    }

    }




    //private float  MapVector(Vector2 position)
    //{
    //    int x = Convert.ToInt32(position.x);
    //    int y = Convert.ToInt32(position.y);

    //    return levelMap[x,y];
    //}



    private void AddPath(Vector2 pathStart, Vector2 pathEnd)
    {
        Debug.Log("looking for path" );

        foreach (Vector2 pathSection in GetPath( pathStart,  pathEnd))
        {
            //  levelMap[Convert.ToInt32(pathSection.x), Convert.ToInt32(pathSection.y)] = 3;
            if (CheckPath(pathSection))
            {
                walkableArea.Add(pathSection);
                Debug.Log("path point added to walkable area : " + pathSection.x + " , " + pathSection.y);
            }

        }
    }

    public List<Vector2> GetPath(Vector2 pathStart, Vector2 pathEnd)
    {
        List<Vector2> path = new List<Vector2>();
        Vector2 pathSection = pathStart;

        // must make more random
        while (pathSection != pathEnd)
        {
            Debug.Log("generating path section");


            if (pathSection.y < pathEnd.y)
            {
                // going left
                pathSection.y++;
            }
            else
            {
                // going right
                pathSection.y--;
            }

            // turn

            if (pathSection.y == pathEnd.y)
            {
                if (pathSection.x < pathEnd.x)
                {
                    // going up
                    pathSection.x++;
                }
                else
                {
                    // going down
                    pathSection.x--;
                }

                if (UnityEngine.Random.Range(0, 5/*levelSize*/) == 1)
                {
                    // possible detour loction
                }

            }

            if (pathSection != pathEnd && pathSection != pathStart)
            {            // maybe should just add path straight to walkable area from here, benefit- 1 less list

                path.Add(pathSection);
                Debug.Log("path section generated at : " + pathSection.x + " , " + pathSection.y);

      //          List<Vector2> surroudings = NeighbouringSections(Convert.ToInt32(pathSection.x), Convert.ToInt32(pathSection.y));

                //widening path
                foreach (Vector2 neighbour in NeighbouringSections(Convert.ToInt32(pathSection.x), Convert.ToInt32(pathSection.y)))
                {
                    if (CheckPath(neighbour))
                        path.Add(neighbour);
                }

            }


        }

        return path;
    }

    //public bool Check(int checkX, int checkY , int value)
    //{
    //    if (levelMap[checkX, checkY] == value)
    //        return true;
    //    else
    //        return false;
    //}

    //public bool CheckBeside(int checkX, int checkY, int value)
    //{

    //    //Check(checkX - 1, checkY, value);

    //    //Check(checkX + 1, checkY, value);

    //    //Check(checkX, checkY - 1, value);

    //    //Check(checkX, checkY + 1, value);

    //    // good for culling with if value = 0 and checkbeside(xy0) == true then will not need to be part of level

    //    //if (Check(checkX - 1, checkY, value) &&
    //    //    Check(checkX + 1, checkY, value) &&
    //    //    Check(checkX, checkY - 1, value) &&
    //    //    Check(checkX, checkY + 1, value))
    //    //    return true;
    //    //else
    //    //    return false;

    //    bool check = true;
    //    foreach (Vector2 item in NeighbouringSections( checkX, checkY))
    //    {
    //        if (Check( checkX,  checkY,  value) == false)
    //        {
    //            check = false;
    //        }
    //    }
    //    return check;
    //}

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


    public bool CheckPath(Vector2 checkPoint)
    {
        // to stop path points from being repeated in list
        bool checkPathResult = true;

        foreach (Vector2 item in walkableArea)
        {
            if (item == checkPoint)
                checkPathResult = false;
        }

        return checkPathResult;

    }


}
