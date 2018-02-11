using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Vector3 offset;

	// Update is called once per frame
	void Update () {
        if(!float.IsNaN(PlayerMovement.instance.transform.position.x)) transform.position = PlayerMovement.instance.transform.position + offset;
	}
}
