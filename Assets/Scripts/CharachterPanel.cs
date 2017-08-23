using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharachterPanel : MonoBehaviour {

    // this[] call makes a private field be shown in the inspector
    [SerializeField]private Text health, level;

    [SerializeField]private Image HealthFill, levelFill;

    [SerializeField]private Player player;

    //Stats
    private List<Text> playerStatTexts = new List<Text>();
    [SerializeField]
    private Text playerStatPrefab;
    [SerializeField]
    private Transform playerStatPannel;

    //Equipped Weapon
 //   private List<Text> playerStatTexts = new List<Text>();
    [SerializeField]
    private Sprite defaultWeaponSprite;
    
    private PlayerWeaponController playerWeaponController;
    [SerializeField]
    private Text weaponStatPrefab;
    [SerializeField]
    private Transform weaponStatPannel;
    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Image weaponIcon;
    private List<Text> weaponStatTexts = new List<Text>();


    //// Use this for initialization
    void Start()
    {
        playerWeaponController = player.GetComponent<PlayerWeaponController>();
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;

        InitilizeStats();
    }

    void UpdateHealth(int currentHealth, int maxHEalth)
    {
        this.health.text = currentHealth.ToString();
        this.HealthFill.fillAmount = ((float)currentHealth/(float)maxHEalth);
    }

    void UpdateLevel()
    {
        this.level.text = player.PlayerLevel.Level.ToString();
        this.levelFill.fillAmount = ((float)player.PlayerLevel.CurrentExperiene / (float)player.PlayerLevel.RequiredExperience);
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

    void UpdateEquippedWeapon(Item item)
    {
        weaponIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
        weaponNameText.text = item.ItemName;


        for (int i = 0; i < item.Stats.Count; i++)
        {
            weaponStatTexts.Add(Instantiate(weaponStatPrefab));
            weaponStatTexts[i].transform.SetParent(weaponStatPannel);
            weaponStatTexts[i].text = item.Stats[i].StatName
                        + " : "
                        + item.Stats[i].GetCalculatedStatValue().ToString();
        }

    }



    public  void UnequipWeapon()
    {
        weaponNameText.text = "unarmed";
        weaponIcon.sprite = defaultWeaponSprite;

        //   weaponStatTexts.Clear();
        for (int i = 0; i < weaponStatTexts.Count; i++)
        {
            //   Destroy(weaponStatTexts[i]);
            weaponStatTexts[i].text = "";
        }

        playerWeaponController.UnequipWeapon();

    }


    //// Update is called once per frame
    //void Update () {

    //}
}
