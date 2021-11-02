using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    public float speed = 4f;

    [Tooltip("Distance to the mouse when following it.")]
    public float offsetToMouse = 1.5f;

    [Tooltip("0 = WASD Movement.\n1 = Follow Mouse.")]
    [Range(0, 1)]
    public int movementType;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        if(movementType == 0)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementType == 0)
        {
            //To enables to change movement type dinamicaly.
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 

            //Actual Movement
            horizontalVerticalMovement();
        }
    }

    void FixedUpdate()
    {
        if (movementType == 1)
            followMouse();   
    }

    //===================
    // WASD Movement
    //===================
    private void horizontalVerticalMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime * horizontal), transform.position.y + (speed * Time.deltaTime * vertical));
    }

    //===================
    // Follow Mouse
    //===================
    private void followMouse()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        followMouseRotate();
        followMouseMovement();
    }

    private void followMouseRotate()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GetComponent<Rigidbody2D>().MoveRotation(Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void followMouseMovement()
    {
        if (direction.magnitude >= offsetToMouse)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
