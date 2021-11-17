using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    public float speed = 4f;

    [Tooltip("Distance to the mouse when following it. Mov type 2 and 3.")]
    public float offsetToMouse = 1.5f;

    [Tooltip("DO NOT CHANGE DURING EMULATION.\n1 = WASD Movement.\n2 = Follow Mouse.\n3 = Click and Go")]
    [Range(1, 3)]
    public int movementType;

    private Vector2 direction;
    private Vector3 waypoint;
    private bool hasChangedDirection;

    // Start is called before the first frame update
    void Start()
    {
        hasChangedDirection = false;
        if (movementType == 1)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementType == 1)
        {
            horizontalVerticalMovement();
        }
        if (movementType == 3)
        {
            getClickandGoCoords();
        }
    }

    void FixedUpdate()
    {
        if (movementType == 2)
        {
            followMouse();
        }
        if (movementType == 3)
        {
            clickandGoMovement();
        }
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
        Rotate();

        if (direction.magnitude >= offsetToMouse)
        {
            MoveInADirection();
        }
        else
        {
            Stop();
        }
    }

    //===================
    // Click and Go
    //===================
    private void getClickandGoCoords()
    {
        if (Input.GetMouseButtonDown(0))
        {
            waypoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = waypoint - transform.position;
            StartCoroutine(setDirectionChange());
        }
    }


    private void clickandGoMovement()
    {
        if (hasChangedDirection)
        {
            Stop();
        }
        if(Vector2.Distance(waypoint,transform.position) >= offsetToMouse)
        {
            Rotate();
            MoveInADirection();
        }
        else
        {
            Stop();
        }
    }

    IEnumerator setDirectionChange()
    {
        hasChangedDirection = true;
        Debug.Log("hasChangedDirection = true");
        yield return new WaitForFixedUpdate();
        hasChangedDirection = false;
        Debug.Log("hasChangedDirection = flase");

    }
    //===================
    // Genral Methods
    //===================
    private void Rotate()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GetComponent<Rigidbody2D>().MoveRotation(Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void MoveInADirection()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
    }

    private void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
