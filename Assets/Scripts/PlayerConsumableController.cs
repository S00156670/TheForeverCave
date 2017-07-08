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

	// Update is called once per frame
	void Update () {
	
	}
}
