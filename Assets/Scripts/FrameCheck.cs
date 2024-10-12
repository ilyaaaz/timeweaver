using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCheck : MonoBehaviour
{
    Animator ani;
    AnimatorStateInfo stateInfo;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        float currentTime = Mathf.Round(stateInfo.normalizedTime % 1.0f * 10f) / 10f;
        if (currentTime < 1 && currentTime > 0)
        {
            ani.SetBool("LastFrame", false);
            ani.SetBool("FirstFrame", false);
        }
    }
    public void LastFrame()
    {
        ani.SetBool("LastFrame", true);
    }
    public void FirstFrame()
    {
        ani.SetBool("FirstFrame", true);
    }
}
