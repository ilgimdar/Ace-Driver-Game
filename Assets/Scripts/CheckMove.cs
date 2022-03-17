using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMove : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    float baseY = 0f;
    float sec;
    float third;
    float xConst = 5000f;
    float divided = 20f;
    float baseX;
    public bool work;
    public GameObject line;
    // Start is called before the first frame update
    void Start()
    {
        baseY = cam.WorldToScreenPoint(transform.position)[1];
        baseX = cam.WorldToScreenPoint(transform.position)[0];
        sec = (baseY * 2) / 3f;
        third = baseY / 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && work)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                line.GetComponent<LineRenderer>().SetPosition(0, cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, +5)));
                line.GetComponent<LineRenderer>().SetPosition(1, cam.ScreenToWorldPoint(t.position));
                float x1 = line.GetComponent<LineRenderer>().GetPosition(0)[0];
                float x = Mathf.Abs(line.GetComponent<LineRenderer>().GetPosition(1)[0] - x1);
                float y = Mathf.Abs(line.GetComponent<LineRenderer>().GetPosition(1)[1] - line.GetComponent<LineRenderer>().GetPosition(0)[1]);
                float cosA = x / Mathf.Sqrt(x * x + y * y);
                float Arcos = 90 - Mathf.Rad2Deg * Mathf.Acos(cosA);
                if(line.GetComponent<LineRenderer>().GetPosition(0)[0] > line.GetComponent<LineRenderer>().GetPosition(1)[0])
                {
                    Arcos *= (-1);
                }
                player.GetComponent<PlayerMovement>().maxAngle =  Arcos;
                    float control = t.position.x - baseX;
                    if(control > 0f)
                    {
                        control -= baseX / divided;
                    }
                    else
                    {
                        control += baseX / divided;
                    }
                    player.GetComponent<PlayerMovement>().xSpeed = ((control) / xConst) / (player.GetComponent<PlayerMovement>().speed * 2f);
                player.GetComponent<PlayerMovement>().maximum = (baseY - t.position.y) / 400f;
            }
            else if(t.phase == TouchPhase.Ended)
            {
                print("ended");
                player.GetComponent<PlayerMovement>().stop = true;
                work = false;
            }
        }
    }

    private void OnMouseDown()
    {
        work = true;
    }

    private void OnMouseUp()
    {
        work = false;
        player.GetComponent<PlayerMovement>().stop = true;
        print("ended");
    }
}
