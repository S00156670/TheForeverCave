using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharachterPanel : MonoBehaviour {

    // this call makes a 
    [SerializeField]private Text health, level;

    [SerializeField]private Image HealthFill, levelFill;

    [SerializeField]private Player player;

    //Stats
    private List<Text> playerStatTexts = new List<Text>();
    [SerializeField]
    private Text playerStatPrefab;
    [SerializeField]
    private Transform playerStatPannel;

    //// Use this for initialization
    void Start()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += EquipWeapon;

        InitilizeStats();
    }

    void UpdateHealth(int currentHealth, int maxHEalth)
    {
        this.health.text = currentHealth.ToString();
        this.HealthFill.fillAmount = ((float)currentHealth/(float)maxHEalth);

    }

    void InitilizeStats()
    {
        // initilizing stats for char pannel
        for (int i = 0; i < player.charachterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab));
            playerStatTexts[i].transform.SetParent(playerStatPannel);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.charachterStats.stats.Count; i++)
        {
            playerStatTexts[i].text = player.charachterStats.stats[i].StatName
                                    + " : " 
                                    + player.charachterStats.stats[i].GetCalculatedStatValue().ToString();

                //;//.ToString(); //   .transform.SetParent(playerStatPannel);
        }
    }

    void EquipWeapon(Item item)
    {
        Debug.Log(item.ItemName + " :equip  char pannel update");
    }

    //// Update is called once per frame
    //void Update () {

    //}
}
