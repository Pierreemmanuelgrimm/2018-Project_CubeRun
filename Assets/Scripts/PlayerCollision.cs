using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
   

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        if(collisionInfo.collider.tag == "Ground")
        {
            //movement.groundLevel = collisionInfo.collider.transform.position.y;
        }
    }
}
