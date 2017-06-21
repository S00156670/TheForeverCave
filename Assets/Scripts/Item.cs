using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item  {

    public List<BaseStat> Stats { get; set; }
    public string ObjectSlug { get; set; }

    public Item(List<BaseStat> stats , string objectSlug)
    {
        Stats = stats;
        ObjectSlug = objectSlug;

    }

}
