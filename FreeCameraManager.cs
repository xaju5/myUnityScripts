using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraManager : MonoBehaviour
{
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        ZoomMovement();
    }


    private void AxisMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(vertical != 0) // w/s or Up/down movement
        {
            float angle = Mathf.Deg2Rad * transform.localEulerAngles.y;
            float distanceX = speed * Mathf.Sin(angle) * vertical;
            float distanceZ = speed * Mathf.Cos(angle) * vertical;
            transform.position = new Vector3(transform.position.x + distanceX, transform.position.y , transform.position.z + distanceZ);

        }
        if(horizontal != 0) // a/d or left/right movement
        {
            float angle = Mathf.Deg2Rad * (transform.localEulerAngles.y + 90); // The perpendicular angle
            float distanceX = speed * Mathf.Sin(angle) * horizontal;
            float distanceZ = speed * Mathf.Cos(angle) * horizontal;

            transform.position = new Vector3(transform.position.x + distanceX, transform.position.y, transform.position.z + distanceZ);
        }

    }
    private void ZoomMovement()
    {

    }
}
