using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExperienceHelper : MonoBehaviour {

    public int Level { get; set; }
    public int CurrentExperiene { get; set; }
    public int RequiredExperience { get { return Level * 25; } }

    // Use this for initialization
    void Start()
    {
        Level = 1;
    }

    public void EnemyToExperience(IEnemy enemy)
    {
        GrantExperience(enemy.Experience);
    }


    public void GrantExperience(int amount)
    {
        CurrentExperiene += amount;


    }


}
