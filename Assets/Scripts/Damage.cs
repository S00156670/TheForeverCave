using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage  {

    public int Amount;
    public enum DamageType { Divine, Heathen, Fire, Ice, Earth, Wind, Spirit, Physical };
    public DamageType Type;

    public Damage(int amount, DamageType type)
    {
        Amount = amount;
        Type = type;
    }
}
