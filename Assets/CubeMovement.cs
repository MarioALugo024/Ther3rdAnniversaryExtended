using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    public GameObject Actor;
    public float speed = 1;
    private Vector3 pos;
    public Material mat;
    private int loopCount = 1;
    public bool onGround = true;
    public float distFromGround = 0.6f;
    public bool isAlive = true;
    public bool turn = false;
    private GameObject[] collection;
    public GameObject restart;
    public GameObject checkpoint;
    public GameObject ref_pos;
    public bool hasStarted = false;
    public GameObject startButton;
    public GameObject ref_pos1;
    public GameObject checkpoint2;
    public AudioSource music;
    public float checkpoint_time;
    public float checkpoint2_time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (isAlive)
            {
                collection = GameObject.FindGameObjectsWithTag("pawn");
                onGround = isGrounded();
                pos = Actor.transform.position;
                Actor.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (onGround == true)
                {
                    GameObject Actor2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Actor2.transform.position = pos;
                    Actor2.GetComponent<MeshRenderer>().material = mat;
                    Actor2.GetComponent<BoxCollider>().isTrigger = true;
                    Actor2.tag = "pawn";
                    if (turn == false)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (loopCount % 2 != 0)
                            {
                                Actor.transform.eulerAngles = new Vector3(0, 90, 0);
                                loopCount++;
                            }
                            else
                            {
                                Actor.transform.eulerAngles = new Vector3(0, 0, 0);
                                loopCount++;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Actor.transform.eulerAngles = new Vector3(0, -90, 0);
                            loopCount++;
                        }
                    }
                }
            }
        }
    }
    public bool isGrounded()
    {
        return Physics.Raycast(Actor.transform.position, Vector3.down, distFromGround);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            isAlive = false;
            GameObject Prime = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject Prime1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject Prime2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Prime.transform.position = pos;
            Prime1.transform.position = pos;
            Prime2.transform.position = pos;
            Prime.AddComponent<Rigidbody>().velocity = Random.onUnitSphere * 50;
            Prime1.AddComponent<Rigidbody>().velocity = Random.onUnitSphere * 50;
            Prime2.AddComponent<Rigidbody>().velocity = Random.onUnitSphere * 100;
            Prime.GetComponent<MeshRenderer>().material = mat;
            Prime1.GetComponent<MeshRenderer>().material = mat;
            Prime2.GetComponent<MeshRenderer>().material = mat;
            Prime.tag = "pawn";
            Prime1.tag = "pawn";
            Prime2.tag = "pawn";
            restart.SetActive(true);
            checkpoint.SetActive(true);
            checkpoint2.SetActive(true);
            music.Stop();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            turn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            turn = false;
        }
    }
    public void _Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void _checkpoint()
    {
        hasStarted = false;
        startButton.SetActive(true);
        isAlive = true;
        Actor.transform.position = ref_pos.transform.position;
        Actor.transform.eulerAngles = new Vector3(0, 0, 0);
        restart.SetActive(false);
        checkpoint.SetActive(false);
        checkpoint2.SetActive(false);
        foreach(GameObject l in collection)
        {
            Destroy(l);
        }
        music.time = checkpoint_time;
    }
    public void start()
    {
        startButton.SetActive(false);
        hasStarted = true;
        music.Play();
    }
    public void _checkpoint2()
    {
        hasStarted = false;
        startButton.SetActive(true);
        isAlive = true;
        checkpoint2.SetActive(false);
        Actor.transform.position = ref_pos1.transform.position;
        Actor.transform.eulerAngles = new Vector3(0, 0, 0);
        restart.SetActive(false);
        checkpoint.SetActive(false);
        foreach(GameObject l in collection)
        {
            Destroy(l);
        }
        music.time = checkpoint2_time;
    }
}