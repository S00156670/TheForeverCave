using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWeapon {

    List<BaseStat> Stats { get; set; }

    void PerformAttack();

    // throw weapon? (right click) 
    void PerformSpecialAttack();

}
