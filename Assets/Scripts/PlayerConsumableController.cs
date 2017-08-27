using UnityEngine;
using System.Collections;

public class PlayerConsumableController : MonoBehaviour {

    CharachterStats stats;



	// Use this for initialization
	void Start () {
        //stats = GetComponent<CharachterStats>();
        stats = GetComponent<Player>().charachterStats;
    }

    public void ConsumeItem(Item item)
    {
        // generates physical representation of item spawning
        GameObject itemToHandle = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));

        if (item.StatModifier)
        {
            itemToHandle.GetComponent<IConsumable>().Consume(stats);
        }
        else
        {
            itemToHandle.GetComponent<IConsumable>().Consume();
        }

    }

    public void ConsumeItem(Item item, Vector3 pos)
    {
        
        // generates physical representation of item spawning
        GameObject itemToHandle = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug),pos,new Quaternion(0,0,0,0));

        if (item.StatModifier)
        {
            itemToHandle.GetComponent<IConsumable>().Consume(stats);
        }
        else
        {
            itemToHandle.GetComponent<IConsumable>().Consume();
        }

        StartCoroutine(WaitToDestroy(item.ObjectSlug));
    }

    IEnumerator WaitToDestroy(string slug)
    {
        yield return new WaitForSeconds(2);
        //   GameObject.Destroy(c);
        GameObject.Destroy(GameObject.Find(slug+"(Clone)"));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
