using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public bool stop;
    public float xSpeed;
    Rigidbody2D rb;
    public float accPower;
    float direction;
    public GameObject button;
    public float maximum;
    float max;
    public float maxAngle = 0f;
    bool drift = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!button.GetComponent<CheckMove>().work && !stop)
        {
            stop = true;
        }
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        if(Mathf.Abs(rb.rotation - maxAngle) > accPower)
        {
            if(Mathf.Abs(rb.rotation - maxAngle) > 30 && !drift && speed > 0.7f) {
                drift = true;
                print("NICE");
            }
            else
            {
                print(rb.rotation + " " + maxAngle);
            }
            if (rb.rotation < maxAngle)
            {
                rb.rotation += accPower;
                if (drift)
                {
                    rb.rotation += accPower;
                }
            }
            else
            {
                rb.rotation -= accPower;
                if (drift)
                {
                    rb.rotation -= accPower;
                }
            }
        }
        else
        {
            drift = false;
            rb.rotation = maxAngle;
        }
        max = maximum * 5f;
        if (drift)
        {
            max *= 1.1f;
        }
        float lastPar = 0f;
        if (speed > 0f && rb.velocity.magnitude < max)
        {
            lastPar = 10f;
            if (max - rb.velocity.magnitude > 0.5f)
            {
                lastPar = 30f;
                if (max - rb.velocity.magnitude > 1f)
                {
                    lastPar = 20f;
                }
            }
            rb.AddRelativeForce(Vector2.up * speed * lastPar);
        }
        else if(speed == 0f)
        {
            rb.velocity = Vector2.zero;
        }
        float mult = 0.5f;
        if (drift)
        {
            mult = -100f;
        }
        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * xSpeed * mult);
       
    }

    private void Update()
    {
        if(speed < maximum)
        {
            speed += 0.02f;
            if(speed >= maximum)
            {
                speed = maximum;
            }
        }
        if(speed > maximum)
        {
            speed -= 0.02f;
            if(speed <= maximum)
            {
                speed = maximum;
            }
        }
        if (stop && speed > 0f)
        {
            maximum = 0f;
            stop = false;
        }
    }

    bool sameZn(float a, float b)
    {
        return (a == 0f && b == 0f) || (a > 0f && b > 0f) || (a < 0f && b < 0f);
    }
}
