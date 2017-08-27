using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }


    Vector3 spawnPosition;

    private void Start()
    {

        spawnPosition = transform.position;
        Range = 20f;
   //     Damage = 8;
        Damage =    GameObject.Find("Player").GetComponent<Player>().charachterStats.GetStat(BaseStat.BaseStatType.RangedSkill).GetCalculatedStatValue()
                  + GameObject.Find("Player").GetComponent<Player>().charachterStats.GetStat(BaseStat.BaseStatType.MagicSkill).GetCalculatedStatValue();
        // 50f can be replaced later by a speed value
        Debug.Log("Fireball Burning at " + Damage);

        GetComponent<Rigidbody>().AddForce(Direction * 10f);
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) > Range)
        {
            Debug.Log("Fireball fizzeling out");
            Extinguish();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Fireball hit");

        if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<IEnemy>().TakeDamage(Damage);
            Debug.Log("Fireball damage = " + Damage);
        }
        else
        {
            Debug.Log("Fireball hit not registered as valid enemy");
        }
        

        Extinguish();
    }

    void Extinguish()
    {
        Debug.Log("Fireball extinguished");
        Destroy(gameObject);
    }
}
