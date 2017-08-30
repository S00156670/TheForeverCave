using UnityEngine;
using System.Collections;

public class BallProjectile : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }

    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        Range = 20f;
        Damage = GameObject.Find("Player").GetComponent<Player>().charachterStats.GetStat(BaseStat.BaseStatType.RangedSkill).GetCalculatedStatValue()
                  + GameObject.Find("Player").GetComponent<Player>().charachterStats.GetStat(BaseStat.BaseStatType.MagicSkill).GetCalculatedStatValue();
        // 50f can be replaced later by a speed value
        Debug.Log("ball(Holy) Burning at " + Damage);

        GetComponent<Rigidbody>().AddForce(Direction * 10f);
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) > Range)
        {
            Debug.Log("ball throw max distance");
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
            Debug.Log("ball damage = " + Damage);



        }
        else
        {
            Debug.Log("ball hit not registered as valid enemy");
        }


        Extinguish();
    }

    void Extinguish()
    {
        //GameObject ballDrop = Resources.Load<GameObject>("Charachters/EnemyCube");
        //Instantiate(ballDrop, transform.position, Quaternion.identity);

        Debug.Log("ball extinguished");
        Destroy(gameObject);
    }
}
