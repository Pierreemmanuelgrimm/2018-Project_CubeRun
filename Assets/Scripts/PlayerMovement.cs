using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    public Rigidbody rb;
    public GameObject AccelPartEff;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    public float maxEnergy = 1000f;
    public float Energy;
    public float lossRate = 150f;

    bool inputLeft = false;
    bool inputRight = false;

    public void AddEnergy(float e)
    {
        Energy += e;
        if (Energy > maxEnergy) Energy = maxEnergy;
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Energy = maxEnergy;
    }
    // Fixed Update are used to mess with physics
    void FixedUpdate () {

        // FRONT FORCE
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        // DETECT INPUT
        inputLeft = false;
        inputRight = false;
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                inputLeft = true;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                inputRight = true;
            }
        }
        if (Input.GetKey("a")) inputLeft = true;
        if (Input.GetKey("d")) inputRight = true;

        // ADD FORCES
        if (inputRight && !inputLeft) {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            AccelPartEff.SetActive(false);
        }
        if (inputLeft && !inputRight)
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            AccelPartEff.SetActive(false);
        }
        if(inputLeft && inputRight && Energy > 0f)
        {
            rb.AddForce(0, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
            AccelPartEff.SetActive(true);
            Energy -= lossRate * Time.deltaTime;
            if (Energy < 0) Energy = 0;
        }

        // LOSE GAME
        if(rb.position.y < LevelManager.instance.deathHeight || float.IsNaN(rb.position.z))
        {
            GameManager.instance.EndGame();
        }

    }

}
