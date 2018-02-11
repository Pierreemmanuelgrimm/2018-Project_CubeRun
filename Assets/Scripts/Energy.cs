using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    public Slider slider;
    public PlayerMovement player;	
	// Update is called once per frame
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = PlayerMovement.instance.maxEnergy;
    }
	void Update ()
    {
        slider.value = PlayerMovement.instance.Energy;
	}
}
