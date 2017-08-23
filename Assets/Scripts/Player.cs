using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public CharachterStats charachterStats;

    public int currentHealth;
    public int maxHealth;

    public LevelExperienceHelper PlayerLevel { get; set; }
    public int currentLevel;
 //   public int rewardsWaiting;

    // Use this for initialization
    void Awake () {

        PlayerLevel = GetComponent<LevelExperienceHelper>();
        currentLevel = 0; // PlayerLevel.Level;
        //      rewardsWaiting = 0;

        //        charachterStats = new CharachterStats(4, 10, 2);
        charachterStats = new CharachterStats(4, 5, 2, 5, 4, 2, 3);

        maxHealth = (charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10)
            + charachterStats.GetStat(BaseStat.BaseStatType.Vitality).GetCalculatedStatValue();

        this.currentHealth = this.maxHealth;

        // not setting in the window at first
        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

        Debug.Log("Player is created and awakened");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log(amount + " damage recieved by player");
        Debug.Log(currentHealth + " is current player health");

        if (currentHealth <= 0)
            Die();

        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    private void Die()
    {
        Debug.Log("Player is dead , resetting health");
        this.currentHealth = maxHealth;

        // reset gameplay position from here
        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != PlayerLevel.Level)
        {
            Debug.Log("PLAYER LEVEL UP");
            LevelUpStats();
        }
	}

    void LevelUpStats()// might be better to just add a listener onto expHelper
    {
        currentLevel++;
     //   rewardsWaiting += currentLevel;

        for (int i = 0; i < currentLevel; i++)
        {
            int luckyStat = UnityEngine.Random.Range(0, charachterStats.stats.Count);
            charachterStats.stats[luckyStat].BaseValue ++;
            UIEventHandler.StatsChanged();
            Debug.Log("PLAYER RECIEVED BASE STAT INCREACE");
        }

    }
}
