using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : PowerUp {

    private int coinValue = 20;
    public override void ApplyEffect(Collider player)
    {
        GameManager.instance.highscore.AddCoins(20);
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
