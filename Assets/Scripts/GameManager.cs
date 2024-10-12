using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public GameObject player;
    public GameObject hookPrefab;
    public GameObject ropePrefab;
    [HideInInspector] public GameObject currentHook;

    [Header("Hook")]
    public bool hookOut;
    public bool isHooked;

    [Header("Animation")]
    public List<Animator> animators = new List<Animator>();
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        //check if currentHook exist
        if (!currentHook)
        {
            hookOut = false;
            isHooked = false;
        }
    }

    //set all the default variables
    void InitializeGame()
    {
        isHooked = false;
        hookOut = false;
    }

    public Vector3 getMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
    }

    public void ChangeAnimationSpeed(float speed)
    {
        
        for (int i = 0; i < animators.Count; i++)
        {
            if (speed > 0 && !animators[i].GetBool("LastFrame")) // 如果还没有到最后一帧
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


}
