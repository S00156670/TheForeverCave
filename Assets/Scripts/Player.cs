using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public CharachterStats charachterStats;



	// Use this for initialization
	void Start () {

        charachterStats = new CharachterStats(5,5,5);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
