using UnityEngine;
using System.Collections;

public interface IConsumable  {

    // keys and other
    void Consume();
    // potions and food
    void Consume(CharachterStats stats);

}
