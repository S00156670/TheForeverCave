﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class DungeonManager : MonoBehaviour {


    public GameObject ground { get; set; }
    private List<Vector2> walkableArea;
    public int levelStage;
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
    bool ChestDropped = false;
    public Vector3 startPos;
    public Vector3 endPos;
    public DungeonSpawner currentSpawn;

    Portal campPortal;
    Portal caveStart;
    Portal caveEnd;

    Enemy boss;


    //AudioSource song;
    //AudioClip currentSong;
    //  public List<Portal> portals;

    void Awake ()
    {
        //song = new AudioSource();//GetComponent<AudioSource>();
        //song.clip = Resources.Load<AudioClip>("Sound/CampSong");
        //song.Play();

        levelStage = 1;
        currentSpawn = new DungeonSpawner();

        campPortal = GameObject.Find("CampPortal").GetComponent<Portal>();
        caveEnd = GameObject.Find("EndPortal").GetComponent<Portal>();
        caveStart = GameObject.Find("StartPortal").GetComponent<Portal>();

        GenerateCave();
        player = GameObject.Find("Player").GetComponent<Player>();

        Debug.Log("Game starting with initial dialogue");
        string[] speach = new string[4];
        speach[0] = "Oh no, the football rolled away!";
        speach[1] = "It went into that cave, ill have a look.";
        speach[2] = "Good thing I can: press i for inventory, Lclick is move, Rclick is attack. That will come in handy";
        speach[3] = "I can now also try to use left and right arrows to rotate camera view";
        DialogueManager.Instance.AddNewDialogue(speach, "Player");

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

    public void ExitFail()
    {
        TravelPortal(caveStart);
        inCave = false;
        caveStart.triggered = false;
        currentSpawn.RemovePickups();
        GenerateCave();
        ChestDropped = true;

        Debug.Log("Back to camp");
        string[] speach = new string[1];
        speach[0] = "Back already? you can't have checked everywhere in that time";
        DialogueManager.Instance.AddNewDialogue(speach, "Sally");
    }

    void TravelPortal(Portal port)
    {
        // prep cave for entry
        UnityEngine.AI.NavMeshAgent navAgent;
        navAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Vector3 caveStart = this.transform.position + new Vector3((-sectionSize * 0.5f + sectionSize * (levelStart.x)),
        //                                                            0.1f,
        //                                                        (-sectionSize * 0.5f + sectionSize * (levelStart.y)));
        navAgent.Warp(port.destination);
        Debug.Log("portal traveled to " + port.destination.x + "," + port.destination.y + "," + port.destination.z);


        DialogueManager.Instance.HUD.SetActive(true);

        int remainingLines = DialogueManager.Instance.dialogueLines.Count - DialogueManager.Instance.dialogueIndex;

        for (int i = 0; i < remainingLines; i++)
        {
            DialogueManager.Instance.ContinueDialogue();
        }

        //if (/*GameObject.Find("").*/GetComponent<DialogueManager>().HUD != null)
        //{
        //    /*GameObject.Find("").*/GetComponent<DialogueManager>().HUD.SetActive(true);
        //}

    }

    void Update()
    {
        // if esc is pressed, exit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //// check portals
        if (!inCave)
        {
            campPortal.CheckDistance(player.transform.position);
            if (campPortal.triggered == true)
            {
                currentSpawn.TrimEnemies(this.transform.position.y);
                TravelPortal(campPortal);
                inCave = true;
                ChestDropped = false;

                if (levelStage == 5)
                {
                    GameObject.Find("Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/FinalSong");
                    GameObject.Find("Music").GetComponent<AudioSource>().Play();
                }
                else
                {
                GameObject.Find("Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/CaveSong");
                GameObject.Find("Music").GetComponent<AudioSource>().Play();
                }

            }
        }
        else
        {   // while in the cave
            caveEnd.CheckDistance(player.transform.position);
            if (boss == null)
            {
                if (ChestDropped == false)
                {
                   // drop reward
                    if (levelStage == 4)
                    {
                        currentSpawn.SpawnBall(endPos + transform.position + new Vector3(0, 1, 0));
                    }
                    else
                    {
                        currentSpawn.SpawnTreasure(endPos + transform.position /*+ new Vector3(0, 1, 0)*/);
                    }

                    if (levelStage == 5)
                    {
                        GameObject.Find("Sally").GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(caveEnd.transform.position);
                    }
                    ChestDropped = true;
                }

                if (caveEnd.triggered == true)
                {
                    TravelPortal(caveEnd);
                    player.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(-9, 4, 4));
                    inCave = false;
                    caveEnd.triggered = false;
                    currentSpawn.RemovePickups();

                    GameObject.Find("Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/CampSong");
                    GameObject.Find("Music").GetComponent<AudioSource>().Play();

                    // refactor as ChangeLevelStage(){}
                    levelStage++;
                    if (levelStage > 5)
                    {
                        GameWin();
                        GameObject.Find("Sally").GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(campPortal.transform.position);
                        GameObject.Find("Sally").GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(-9,3.5f,3));
                        levelStage = 1;
                        // looped for testing purpouses, in fanal game  if (levelStage > 5){YouWin();}
                    }
                    else if (levelStage == 5)
                    {
                        Debug.Log("Next Level is end level");
                        string[] speach = new string[3];
                        speach[0] = "So, you have managed to get your toy back. I'll take something else then....";
                        speach[1] = "..Hypnotize .. hypnoootiize hYpnoTiSe!!";
                        speach[2] = "Your friend is mine and will never leave alive!";
                        DialogueManager.Instance.AddNewDialogue(speach, "Mysterious Voice");

                        GameObject.Find("Sally").GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(campPortal.transform.position);

                        //speach[0] = "What? How? NO!";
                        //speach[1] = "...Juust fantastic.";
                        //speach[2] = "Dad will kill me if we don't make it back together.";
                        //DialogueManager.Instance.AddNewDialogue(speach, "Internal Monologue");
                    }
                    else
                    {
                        Debug.Log("Next Level");
                        string[] speach = new string[3];
                        speach[0] = "Wow, you made it past level " + (levelStage - 1) + ".";
                        speach[1] = "You still havn't found the ball though!";
                        switch (levelStage)
                        {
                            case 2:
                                speach[2] = "Dad made that ball special for us, try looking again.";
                                break;
                            case 3:
                                speach[2] = "I miss the ball. It might look tatty but it's very soft. I was planning to use it as a pillow while we camped out here.";
                                break;
                            case 4:
                                speach[2] = "I just remembered before we came here, mom wrote the passcode for the city gates on the side of that ball. If we dont find it we can't get back in.";
                                break;
                            default:
                                speach[2] = "This is bad";
                                break;
                        }
                        DialogueManager.Instance.AddNewDialogue(speach, "Sally");
                    }


                    GenerateCave();
                    ChestDropped = true;
                }
            }

            caveStart.CheckDistance(player.transform.position);
            if (caveStart.triggered == true)
            {
                TravelPortal(caveStart);
                player.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(-9, 4, 4));
                inCave = false;
                caveStart.triggered = false;
                currentSpawn.RemovePickups();
                GenerateCave();
                ChestDropped = true;

                GameObject.Find("Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/CampSong");
                GameObject.Find("Music").GetComponent<AudioSource>().Play();

                Debug.Log("Back to camp");
                string[] speach = new string[1];
                speach[0] = "Back already? you can't have checked everywhere in that time";
                DialogueManager.Instance.AddNewDialogue(speach, "Sally");
            }
        }
        
        ////// key buttons for game progression to be phased out for smooth gameplay
        ////if (Input.GetKeyDown(KeyCode.LeftArrow))
        ////    {
        ////    GenerateCave();
        ////    }

        ////if (Input.GetKeyDown(KeyCode.DownArrow))
        ////{
        ////    // go to waypoint island
        ////    UnityEngine.AI.NavMeshAgent navAgent;
        ////    navAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
        ////    //navAgent.transform.position = new Vector3(12.6f, 4, 32.1f);
        ////    //navAgent.SetDestination(new Vector3(12.6f, 4, 32.1f));
        ////    navAgent.Warp(new Vector3(12.6f, 4, 32.1f));
        ////}

        ////if (Input.GetKeyDown(KeyCode.RightArrow))
        ////{
        ////    UnityEngine.AI.NavMeshAgent navAgent;
        ////    navAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();

        ////    if (!inCave)
        ////    {
        ////        // go to cave start
        ////        currentSpawn.TrimEnemies(this.transform.position.y);
        ////        Vector3 caveStart = this.transform.position + new Vector3((-sectionSize * 0.5f + sectionSize * (levelStart.x)),
        ////                                                                    0.1f,
        ////                                                                (-sectionSize * 0.5f + sectionSize * (levelStart.y)));
        ////        Debug.Log("cave start translate at " + caveStart.x + "," + caveStart.y + "," + caveStart.z );

        ////        //         player.transform.position = caveStart;
        ////        //navAgent.transform.position = caveStart;
        ////        //navAgent.SetDestination(caveStart);
        ////        navAgent.Warp(caveStart);
        ////        //    navAgent.SetDestination(caveStart);
        ////        //       navAgent.destination = caveStart;
        ////        // player.GetComponent<NavMeshAgent>().SetDestination(caveStart);
        ////        inCave = true;
        ////    }
        ////    else
        ////    {
        ////        // send palyer back to campsite
        ////        //navAgent.transform.position = new Vector3(-6,4,1);
        ////        //navAgent.SetDestination(new Vector3(-6, 4, 1));
        ////        navAgent.Warp(new Vector3(-6, 4, 1));
        ////        inCave = false;
        ////    }
        ////}

    }

    private void GameWin()
    {
        Debug.Log("Game Over");
        string[] endgameText = new string[2];
        endgameText[0] = "You have defeated the game.";
        endgameText[1] = "Congradulations!!";
        DialogueManager.Instance.AddNewDialogue(endgameText, name);
    }

    private void GenerateCave()
    {

        currentSpawn = new DungeonSpawner();
        currentSpawn.SpawnPoints = new List<Vector3>();

        Debug.Log("Generating stage " + levelStage + "cave");

        //// plan level

        //levelStage++;

        //if (levelStage > 5)
        //{
        //    levelStage = 1;
        //    // looped for testing purpouses, in fanal game  if (levelStage > 5){YouWin();}
        //}


        levelSize = (levelStage * 16) + 32;

        // decide pathing
        GenerateLevelMap();
        Debug.Log("level size is : " + levelSize);

        filter = GetComponent<MeshFilter>();
        // generate cave geometry
        filter.mesh = GenerateMesh();

        // re-orientate to line up with off-mesh-link
        // may not be needed now im using nav.warp
        transform.position = new Vector3(transform.position.x, transform.position.y, 32 - startPos.z);
        
        // spawn enemies and treasure chests
        Debug.Log("SPAWN ENEMY ATTEMPT");
        currentSpawn.SpawnEnemies(levelStage,endPos + this.transform.position);


    //////    currentSpawn.SpawnBall(endPos + this.transform.position);//new Vector3(40,150,20));
    ////    if (levelStage == 4)
    ////    {
    //////        currentSpawn.SpawnBall();


    ////    //ItemDatabase x = new ItemDatabase();
    ////    //Item ball = x.GetItem("ball");
    ////    //PickUpItem ballPickUp = new PickUpItem();
    ////    //ballPickUp.ItemToPick = ball;
    ////    //Instantiate(ballPickUp, transform.position, Quaternion.identity);



    //////////      //  Item ball;
    //////////        PickUpItem pickUpItem = new PickUpItem(ItemDatabase.GetItem("ball"));

    ////////////        LootDrop ballDrop = new LootDrop("PickUpBall", 100);
    ////////////        Item ball = ballDrop.GetDrop();


    //////////        PickUpItem ballDrop = Instantiate(pickUpItem, transform.position, Quaternion.identity);
    //////////        ballDrop.ItemToPick = ball;

    ////        //LootDrop ballDrop = new LootDrop("PickUpBall", 100);
    ////        //ItemDatabase.instance.GetItem(ballDrop.ItemSlug);   //.("PickUpBall"));

    ////        //Instantiate(enemyToSpawn, p, Quaternion.identity);


    ////        //Item item = ballDrop.GetDrop();

    ////        //if (item != null)
    ////        //{
    ////        //    PickUpItem instance = Instantiate(pickUpItem, transform.position, Quaternion.identity);
    ////        //    instance.ItemToPick = item;
    ////        //}


    ////        //PickUpItem instance = Instantiate(pickUpItem, transform.position, Quaternion.identity);
    ////        //instance.ItemToPick = item;

    ////    }


        caveEnd.transform.position = endPos + this.transform.position + new Vector3(6,0,-1.2f);

        boss = GameObject.Find("EnemyBoss(Clone)").GetComponent<Enemy>();

        if (levelStage == 5)
        {
            GameObject.Find("Sally").GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(endPos + this.transform.position);
        }


        Debug.Log("Finished stage " + levelStage + "cave");
        // might need to postpone this so that all enemies have had a chance to fall into place
        ////     currentSpawn.TrimEnemies(this.transform.position.y);
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

                    if (levelEnd.x == x && levelEnd.y == y)
                    {
                        endPos = (new Vector3(-sectionSize * 0.5f + sectionSize * (x),
                                                0,
                                             -sectionSize * 0.5f + sectionSize * (y)));




                    }

                    SpawnlistChance
                        (
                            new Vector3(-sectionSize * 0.5f + sectionSize * (x),2,-sectionSize * 0.5f + sectionSize * (y))  
                          + new Vector3(transform.position.x, transform.position.y, 32 - startPos.z));
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

            //currentSpawn.TreasurePoints.Add(
            //    new Vector3(-sectionSize * 0.5f + sectionSize * (walkableArea[1].x),
            //    2,
            //    -sectionSize * 0.5f + sectionSize * (walkableArea[1].y))
            //  +
            //    new Vector3(transform.position.x, transform.position.y, 32 - startPos.z)
            //  );
        }
        else
        {
            for (int i = levelStage + 10; i < walkableArea.Count - 2; i+= 4)
            {
                //UnityEngine.Random.Range(0, 10);
                if (UnityEngine.Random.Range(0, 50) <= 2)
                {
                    AddDiversion(walkableArea[i]);
                }
            }
        }

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
   //         currentSpawn.TreasurePoints.Add(detour);
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
             //   Debug.Log("path point added to walkable area : " + pathSection.x + " , " + pathSection.y);
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
    //        Debug.Log("generating path section");


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
          //      Debug.Log("path section generated at : " + pathSection.x + " , " + pathSection.y);

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
