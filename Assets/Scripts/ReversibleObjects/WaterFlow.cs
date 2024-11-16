using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour, IReversible
{
    [SerializeField] private float speed = 2f;
    private bool isReversing = false;

    private BuoyancyEffector2D buoyancyEffector;
    private float originalFlowMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Buoyancy Effector 2D component
        buoyancyEffector = GetComponent<BuoyancyEffector2D>();

        if (buoyancyEffector != null)
        {
            // Store the original flow magnitude
            originalFlowMagnitude = buoyancyEffector.flowMagnitude;
        }
        else
        {
            Debug.LogWarning("No BuoyancyEffector2D attached to the object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, you can use Update for other dynamic behaviors
    }

    public void ReverseTime()
    {
        if (buoyancyEffector != null)
        {
            Debug.Log($"{gameObject.name} is reversing flow magnitude.");
            
            // Reverse the flow magnitude
            buoyancyEffector.flowMagnitude = -buoyancyEffector.flowMagnitude;


            isReversing = true;
        }
        else
        {
            Debug.LogWarning("ReverseTime() called, but no BuoyancyEffector2D is attached.");
        }
    }
}