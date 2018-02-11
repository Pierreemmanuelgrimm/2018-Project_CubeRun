using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationResetPickup : PowerUp {

	new void Start () {
        base.Start();
	}

    public override void ApplyEffect(Collider player)
    {
        player.transform.rotation = new Quaternion(0, 0, 0, player.transform.rotation.w);
        player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    }
}
