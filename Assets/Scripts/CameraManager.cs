using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public List<CinemachineVirtualCamera> virtualCameras; // List of all virtual cameras

    private int currentCameraIndex = -1;

    void Start()
    {
        if (virtualCameras == null || virtualCameras.Count == 0)
        {
            Debug.LogError("No virtual cameras assigned to the Camera Manager.");
            return;
        }

        // Set all cameras to inactive initially by setting low priority
        foreach (var cam in virtualCameras)
        {
            cam.Priority = 0;
        }

        UpdateActiveCamera();
    }

    void Update()
    {
        UpdateActiveCamera();
    }

    void UpdateActiveCamera()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set in Camera Manager.");
            return;
        }

        int newCameraIndex = -1;

        // Loop through all virtual cameras to find which one the player is currently within
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            CinemachineVirtualCamera vCam = virtualCameras[i];

            // Calculate the boundaries of the current virtual camera based on its position
            float cameraX = vCam.transform.position.x;
            float cameraY = vCam.transform.position.y;
            float halfWidth = 40f / 2f;
            float halfHeight = 22f / 2f;

            // Check if the player is within the boundaries of this virtual camera
            if (player.position.x >= cameraX - halfWidth && player.position.x < cameraX + halfWidth &&
                player.position.y >= cameraY - halfHeight && player.position.y < cameraY + halfHeight)
            {
                newCameraIndex = i;
                break;
            }
        }

        // If the player has moved to a different zone, update the camera
        if (newCameraIndex != currentCameraIndex && newCameraIndex >= 0)
        {
            // Deactivate the previous camera
            if (currentCameraIndex >= 0 && currentCameraIndex < virtualCameras.Count)
            {
                virtualCameras[currentCameraIndex].Priority = 0;
            }

            // Activate the new camera
            virtualCameras[newCameraIndex].Priority = 10;

            currentCameraIndex = newCameraIndex;
        }
    }
}
