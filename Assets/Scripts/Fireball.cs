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
        Damage = 8;

        // 50f can be replaced later by a speed value
        GetComponent<Rigidbody>().AddForce(Direction * 50f);
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) > Range)
        {
            Extinguish();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<IEnemy>().TakeDamage(Damage);
        }

        Extinguish();
    }

    void Extinguish()
    {
        Destroy(gameObject);
    }
}
