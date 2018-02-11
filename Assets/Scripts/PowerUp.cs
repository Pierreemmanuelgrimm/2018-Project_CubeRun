using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public GameObject pickupEffect;
    MeshRenderer GFX;
    public void Start()
    {
        GFX = this.transform.GetChild(0).GetComponent<MeshRenderer>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }
    IEnumerator Pickup(Collider player)
    {
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        effect.transform.parent = this.transform;

        GFX.enabled = false;
        GetComponent<Collider>().enabled = false;

        ApplyEffect(player); 

        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);

    }
    public virtual void ApplyEffect(Collider player)
    {

    }
}
