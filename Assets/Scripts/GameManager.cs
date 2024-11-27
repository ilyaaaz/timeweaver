using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Map Settings")]
    float camHeight = 10f;
    float camWidth;
    public Camera cam;
    //public Vector2 gridSize;

    [Header("Player Settings")]
    public GameObject player;
    public int playerHealth;
    public float playerSpeed;
    public float playerJumpForce;
    public GameObject currentCheckPoint;

    [Header("Time Travel Settings")]
    public float effectAreaRadius;
    public float skillCooldown;
    public float reverseTime; //how many seconds of time is reversed

    [Header("Layer Settings")]
    public LayerMask groundLayer;
    public LayerMask trapLayer;

    [Header("Slime Settings")]
    public float speed;

    //public float playerPullForce; // Pull rope
    /*
    public GameObject hookPrefab;
    public GameObject ropePrefab;
    [HideInInspector] public GameObject currentHook;
    */

    /*
    [Header("Hook Settings")]
    public bool hookOut;
    public bool isHooked
    */

    [Header("Animation Settings")]
    public List<GameObject> reversibleObjects = new List<GameObject>();
    public List<Animator> animators = new List<Animator>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        camWidth = camHeight * 2560 / 1440;
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerOutCam();
        /*
        //check if currentHook exist
        if (!currentHook)
        {
            hookOut = false;
            isHooked = false;
        }
        */
    }

    void CheckPlayerOutCam()
    {
        if (player.transform.position.x >= cam.transform.position.x + camWidth / 2)
        {
            cam.transform.position += new Vector3(camWidth, 0, 0);
        }
        else if (player.transform.position.x <= cam.transform.position.x - camWidth / 2)
        {
            cam.transform.position -= new Vector3(camWidth, 0, 0);
        }
        else if (player.transform.position.y >= cam.transform.position.y + camHeight / 2)
        {
            cam.transform.position += new Vector3(0, camHeight, 0);
        }
        else if (player.transform.position.y <= cam.transform.position.y - camHeight / 2)
        {
            cam.transform.position -= new Vector3(0, camHeight, 0);
        }
    }

    public void ReverseObjects()
    {
        
    }

    //set all the default variables
    void InitializeGame()
    {
        /*
        isHooked = false;
        hookOut = false;
        */
    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
    }

    public void ChangeAnimationSpeed(float speed)
    {
        
        for (int i = 0; i < animators.Count; i++)
        {
            if (speed > 0 && !animators[i].GetBool("LastFrame"))
            {
                animators[i].SetFloat("Speed", speed);
            }
            else if (speed < 0 && !animators[i].GetBool("FirstFrame"))
            {
                animators[i].SetFloat("Speed", speed);
            } else
            {
                animators[i].SetFloat("Speed", 0f);
            }
        }

    }

    public void GameRestart()
    {

    }


}
