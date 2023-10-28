using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public CameraShake camerashake;
    public UIManager uimanager;

    public GameObject cam;
    public GameObject vectorback;
    public GameObject vectorforward;

    private Rigidbody rb;
    private Touch touch;
    [Range(20,40)]
    public int speedModifier;
    public int forwardSpeed;

    private bool speedballforward = false;
    private bool firstTouchControl = false;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(Variables.firstTouch == 1 & speedballforward == false)
        {
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorback.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorforward.transform.position += new Vector3(0, 0, forwardSpeed* Time.deltaTime);
            
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (firstTouchControl == false)
                    {
                        Variables.firstTouch = 1;
                        uimanager.FirstTouch();
                        firstTouchControl = true;
                    }
                    
                }
                
            }
            
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                                         transform.position.y,
                                         touch.deltaPosition.y * speedModifier * Time.deltaTime);
                   
                    if (firstTouchControl == false)
                    {
                        Variables.firstTouch = 1;
                        uimanager.FirstTouch();
                        firstTouchControl = true;
                    }

                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    public GameObject[] FractureItems;

    public void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Obstacles"))
        {
            camerashake.CameraShakesCall();
            uimanager.StartCoroutine("WhiteEffect");
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            foreach (GameObject item in FractureItems)
            {
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
            StartCoroutine("TimeScaleControl");

            
        }
    }

    public IEnumerator TimeScaleControl()
    {
        speedballforward = true;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(0.6f);
        uimanager.RestartButtonActive();
        rb.velocity = Vector3.zero;
    }

}
