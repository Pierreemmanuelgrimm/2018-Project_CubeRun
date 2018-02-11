using UnityEngine;

public class Booster : MonoBehaviour {
    public float boostForce = 5f;
    bool isUsed = false;
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
        Debug.Log("Hi");
		if(collider.tag == "Player" && isUsed == false)
        {
            collider.GetComponent<Rigidbody>().AddForce(0, boostForce, 0, ForceMode.VelocityChange);
            isUsed = true;
        }
	}
}
