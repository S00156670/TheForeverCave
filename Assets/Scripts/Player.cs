using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public CharachterStats charachterStats;

    public int currentHealth;
    public int maxHealth;

    public LevelExperienceHelper PlayerLevel { get; set; }


    // Use this for initialization
    void Awake () {

        PlayerLevel = GetComponent<LevelExperienceHelper>();


        //        charachterStats = new CharachterStats(4, 10, 2);
        charachterStats = new CharachterStats(4, 5, 2, 5, 4, 2, 3);

        maxHealth = (charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10)
            + charachterStats.GetStat(BaseStat.BaseStatType.Vitality).GetCalculatedStatValue();

        this.currentHealth = this.maxHealth;

        // not setting in the window at first
        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    //    InvokeRepeating("Regen", 5f, 5f);

        Debug.Log("Player is created and awakened");
    }

    void Regen()
    {
        Debug.Log("Regen : " + charachterStats.GetStat(BaseStat.BaseStatType.Vitality).GetCalculatedStatValue());
        currentHealth += charachterStats.GetStat(BaseStat.BaseStatType.Vitality).GetCalculatedStatValue();

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);
    }

    public void TakeDamage(int amount)
    {
        amount -=  charachterStats.GetStat(BaseStat.BaseStatType.Toughness).GetCalculatedStatValue();

        if (amount > 0)
        currentHealth -= amount;

        Debug.Log(amount + " damage recieved by player");
        Debug.Log(currentHealth + " is current player health");

        if (currentHealth <= 0)
            Die();

        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    public void Die()
    {
        Debug.Log("Player is dead , returning to campsite");
        GameObject.Find("DungeonGenerator").GetComponent<DungeonManager>().ExitFail();

        Debug.Log("Player is dead , resetting health");
        this.currentHealth = maxHealth;
        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    private void Update()
    {

        if (currentHealth > maxHealth)
        {
        currentHealth = maxHealth;
        UIEventHandler.PlayerHealthChanged(currentHealth, maxHealth);
        }

    }


    public void LevelUpStats(int level)// might be better to just add a listener onto expHelper
    {

     //   rewardsWaiting += currentLevel;

        for (int i = 0; i < level; i++)
        {
            int luckyStat = UnityEngine.Random.Range(0, charachterStats.stats.Count);
            charachterStats.stats[luckyStat].BaseValue ++;
            UIEventHandler.StatsChanged();
            Debug.Log("PLAYER RECIEVED BASE STAT INCREACE:" + charachterStats.stats[luckyStat].StatName);
        }

        maxHealth = (charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10)
        + charachterStats.GetStat(BaseStat.BaseStatType.Vitality).GetCalculatedStatValue();

        currentHealth = maxHealth;

        UIEventHandler.PlayerHealthChanged(currentHealth,maxHealth);

    }
}
