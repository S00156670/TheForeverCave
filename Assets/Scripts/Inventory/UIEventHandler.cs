using UnityEngine;
using System.Collections;

public class UIEventHandler : MonoBehaviour {

    // these events basicly just relay a signal from the main code to the UI that something has happened

    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandeler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandeler OnPlayerHealthChanged;

    public delegate void StatsEventHandeler();
    public static event StatsEventHandeler OnStatsChanged;

    public delegate void PlayerLevelEventHandeler();
    public static event PlayerLevelEventHandeler OnPlayerLevelChange;


    public static void ItemAddedToInventory(Item item)
    {
        OnItemAddedToInventory(item);
    }


    public static void ItemEquipped(Item item)
    {
        OnItemEquipped(item);
    }


    public static void PlayerHealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged(currentHealth,maxHealth);
    }

    public static void StatsChanged(Item item)
    {
        OnStatsChanged();
    }

    public static void PlayerLevelChange()
    {
        OnPlayerLevelChange();
    }
    //// Use this for initialization
    //void Start () {

    //}


}
