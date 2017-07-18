using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharachterPanel : MonoBehaviour {

    // this call makes a 
    [SerializeField]private Text health, level;

    [SerializeField]private Image HealthFill, levelFill;

    [SerializeField]private Player player;

    //// Use this for initialization
    void Start()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
    }

    void UpdateHealth(int currentHealth, int maxHEalth)
    {
        this.health.text = currentHealth.ToString();
        this.HealthFill.fillAmount = ((float)currentHealth/(float)maxHEalth);

    }



    //// Update is called once per frame
    //void Update () {

    //}
}
