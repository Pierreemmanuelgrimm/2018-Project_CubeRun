using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkerPickup : PowerUp {

    public float ShrinkScale = -1;
    float minScale = 0.35f;
    float maxScale = 0.85f;
    new void Start()
    {
        base.Start();
        if (ShrinkScale < 0) ShrinkScale = Random.Range(minScale, maxScale);
    }
    public override void ApplyEffect(Collider player)
    {
       player.transform.localScale = new Vector3(ShrinkScale, ShrinkScale, ShrinkScale);
    }
}
