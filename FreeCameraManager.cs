using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraManager : MonoBehaviour
{
    public float axiSpeed = 100f;
    public float zoomSpeed = 400f;
    public float scrollSensibility = 1f;
    public float minZoom = 10;
    public float maxZoom = 100;

    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        Zoom();
        Rotation();
    }

    private void AxisMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        if(vertical != 0) // w/s or Up/down movement
        {
            float angle = Mathf.Deg2Rad * transform.localEulerAngles.y;
            Vector3 movement = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * vertical * axiSpeed * Time.deltaTime;
            transform.Translate(movement,Space.World);

        }
        if (horizontal != 0) // a/d or left/right movement
        {
            float angle = Mathf.Deg2Rad * (transform.localEulerAngles.y + 90); // The perpendicular angle
            Vector3 movement = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * horizontal * axiSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

    }

    private void Zoom()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        if(mouseScroll != 0)
        {
            float zoomLevel = mouseScroll * scrollSensibility;
            Vector3 zooMovement = transform.forward * zoomLevel * zoomSpeed * Time.deltaTime;

            bool insideScope = (transform.position.y < maxZoom && transform.position.y > minZoom);
            bool increasingToScope = (transform.position.y < minZoom && zooMovement.y > 0);
            bool decreasingToScope = (transform.position.y > maxZoom && zooMovement.y < 0);
            if (insideScope || increasingToScope || decreasingToScope)
            {
                transform.Translate(zooMovement,Space.World);
            }
        }
    }
    private void Rotation()
    {
        //TO DO
    }

}
