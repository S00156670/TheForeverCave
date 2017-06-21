using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item  {

    public List<BaseStat> Stats { get; set; }
    // a slug is a term for a simplified object definition , (probaly in the end will also define a mesh)
    public string ObjectSlug { get; set; }

    public Item(List<BaseStat> stats , string objectSlug)
    {
        Stats = stats;
        ObjectSlug = objectSlug;

    }

}
