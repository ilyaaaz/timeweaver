using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSide : MonoBehaviour, IReversible
{
    public List<BridgeWood> woodParts = new List<BridgeWood>();
    bool reversing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reversing)
        {
            ReverseBack();
        }
    }

    public void ReverseTime()
    {
        reversing = true;
        for (int i = 0; i < woodParts.Count; i++)
        {
            woodParts[i].rb.gravityScale = 0f;
        }
    }

    void ReverseBack()
    {
        for (int i = 0; i < woodParts.Count; i++)
        {
            woodParts[i].transform.position = Vector3.Lerp(woodParts[i].transform.position, woodParts[i].originalPos, 0.1f);
            woodParts[i].transform.rotation = Quaternion.Lerp(woodParts[i].transform.rotation, Quaternion.Euler(woodParts[i].originalRot), 0.1f);

            if (Vector3.Distance(woodParts[i].transform.position, woodParts[i].originalPos) < 0.1f)
            {
                reversing = false;
                woodParts[i].hj.enabled = true;
                woodParts[i].rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                woodParts[i].rb.gravityScale = 1f;
            }
        }
    }

}
