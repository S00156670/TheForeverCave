using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    public string currentWeaponSlug = "";

    Transform spawnProjectile;

    IWeapon equippfedWeaponData;

    CharachterStats charachterStats;

    // Use this for initialization
    void Start()
    {
        spawnProjectile = transform.FindChild("ProjectileSpawn");

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
            // handy for sheathe/take out ??

            //   GetComponent<CharachterStats>
            // remove buffs from previous weapon
            charachterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);

            //put weapon to inventory here or fron i

            // remove previous weapon from players hand
            Destroy(playerHand.transform.GetChild(0).gameObject);


        // hand is now empty :)
        }

        currentWeaponSlug = itemToEquip.ObjectSlug;

        // instance of weapon generated at players hand
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>
                            ("Weapons/" + itemToEquip.ObjectSlug), 
                            playerHand.transform.position, 
                            playerHand.transform.rotation);

        // extract weapon info
        equippfedWeaponData = EquippedWeapon.GetComponent<IWeapon>();

        // set stats of equipped weapon
        // should be remembered so you know what to remove when you unequip
        //     EquippedWeapon.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
        equippfedWeaponData.Stats = itemToEquip.Stats;


        // set projectile spawn point if weapon has the IProjectileWeapon signature 
        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        {
        EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }

        // make weapon a child of hand
        EquippedWeapon.transform.SetParent(playerHand.transform);

        // add relevent buffs
        charachterStats.AddStatBonus(itemToEquip.Stats);

        Debug.Log(equippfedWeaponData.Stats[0].BaseValue);

        foreach (var stat in equippfedWeaponData.Stats)
        {
           // print full debug of base and adjusted with stat names

        }

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
