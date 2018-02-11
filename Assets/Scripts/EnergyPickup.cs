using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : PowerUp {

    public float newEnergy = 500f;
    new void Start () {
        base.Start();
	}
    public override void ApplyEffect(Collider player)
    {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.AddEnergy(newEnergy);
    }

}
