using UnityEngine;
using System.Collections;

public class DungeonManager : MonoBehaviour {


    //  public Plane ground;

    private MeshFilter filter;
    private MeshRenderer render;
    private BoxCollider collider;

    public GameObject ground { get; set; }

    public int[,] levelMap;

    Vector2 gridpoint;

    // it will probably be simpler to take an object which already has a nav mesh, modify and then update than to generate full nav mesh on the fly

    // Use this for initialization
    void Start () {
        // advice?
        //https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html
        //       GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //     ground = plane;


        // plan level

        levelMap = new int[20,20];
        // set all to zero
        foreach (int x in levelMap)
        { x = 0; }

        // enter = 1
        levelMap[0, Random.Range(0, levelMap.GetLength)] = 1;
        // exit = 2
        levelMap[levelMap.GetLength, Random.Range(0, levelMap.GetLength)] = 2;
        // pathing waypoint = 4
        levelMap[Random.Range(5, levelMap.GetLength - 5), Random.Range(5, levelMap.GetLength - 5)] = 4;
        // path = 3




        // enter and exit
        // in between


        // edit mesh
        // update nav
        // spawn


    }

    // Update is called once per frame
    void Update () {
	
        // check for enter/exit

	}

    public bool Check(Vector2 checkPos, int value)
    {
        if (levelMap[checkPos.x, checkPos.y] == value)
            return true;
        else
            return false;
    }

    public void CheckBeside(Vector2 checkPos, int value)
    {

        Check(new Vector2(checkPos.x - 1,checkPos.y), value);
        Check(new Vector2(checkPos.x + 1, checkPos.y), value);
        Check(new Vector2(checkPos.x, checkPos.y - 1), value);
        Check(new Vector2(checkPos.x, checkPos.y + 1), value);
        
    }


}
