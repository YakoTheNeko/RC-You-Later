using UnityEngine;

public class Dirt : MonoBehaviour
{

    public float force = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RemoteControlCarController carScript = other.GetComponent<RemoteControlCarController>();

            //Debug.Log(other.attachedRigidbody.linearVelocity);
            //carScript.boostCar(carScript.getDir(), force);
            carScript.boostCar(carScript.getDir(), force);


        }
    }
}
