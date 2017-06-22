using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    IWeapon equippfedWeaponData;

    CharachterStats charachterStats;

    // Use this for initialization
    void Start()
    {
        charachterStats = GetComponent<CharachterStats>();
    }

    void Update()
    {
        // attack button combos
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    PerformSpecialAttack();
            //}
            //else
            PerformAttack();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformSpecialAttack();
        }
    } 

    //equip 
    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            //// !! should refactor this out to a remove weapon method RemoveWeapon(){}

            //   GetComponent<CharachterStats>
            // remove buffs from previous weapon
            charachterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            // remove previous weapon from players hand
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        // hand is now empty :)

        // instance of weapon generated at players hand
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation);

        // extract weapon info
        equippfedWeaponData = EquippedWeapon.GetComponent<IWeapon>();

        // set stats of equipped weapon
        // should be remembered so you know what to remove when you unequip
        //     EquippedWeapon.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
        equippfedWeaponData.Stats = itemToEquip.Stats;
        EquippedWeapon.transform.SetParent(playerHand.transform);
        charachterStats.AddStatBonus(itemToEquip.Stats);

        Debug.Log(equippfedWeaponData.Stats[0].BaseValue);

    }

    // use 
    public void PerformAttack()
    {
        //   EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
        equippfedWeaponData.PerformAttack();
    }

    public void PerformSpecialAttack()
    {

        equippfedWeaponData.PerformSpecialAttack();
    }

    // return to inventory
    // drop/break





}
