using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenProjectile : MonoBehaviour {


    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }

    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        Range = 20f;
        Damage = GameObject.Find("Player").GetComponent<Player>().charachterStats.GetStat(BaseStat.BaseStatType.RangedSkill).GetCalculatedStatValue() * 3;
        GetComponent<Rigidbody>().AddForce(Direction * 10f);
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) > Range)
        {
            Debug.Log("shuriken throw max distance");
            Extinguish();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("ball hit");

        if (other.transform.tag == "Enemy")
        {
            //        other.transform.GetComponent<IEnemy>().TakeDamage(Damage);
            other.transform.GetComponent<IEnemy>().TakeDamage(Damage, global::Damage.DamageType.Divine);
            Debug.Log("shuriken damage = " + Damage);
        }
        else
        {
            Debug.Log("shuriken hit not registered as valid enemy");
        }


        Extinguish();
    }

    void Extinguish()
    {
        //GameObject ballDrop = Resources.Load<GameObject>("Charachters/EnemyCube");
        //Instantiate(ballDrop, transform.position, Quaternion.identity);

        Debug.Log("shuriken used");
        Destroy(gameObject);
    }
}
