using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Camera))]
public class CameraAspectRatio : MonoBehaviour
{
    public float targetWidth = 48f; // Desired width of the camera view
    public float targetHeight = 27f; // Desired height of the camera view

    private CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain == null)
        {
            Debug.LogError("CinemachineBrain is not attached to the main camera.");
        }
    }

    void Update()
    {
        UpdateCameraSettings();
    }

    void UpdateCameraSettings()
    {
        if (cinemachineBrain == null) return;

        // Get the currently active virtual camera
        CinemachineVirtualCamera activeVirtualCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

        if (activeVirtualCamera != null)
        {
            // Calculate the orthographic size to maintain the target width and height
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float targetAspect = targetWidth / targetHeight;

            if (windowAspect >= targetAspect)
            {
                // Wider aspect ratio, adjust orthographic size based on target height
                activeVirtualCamera.m_Lens.OrthographicSize = targetHeight / 2f;
            }
            else
            {
                // Taller aspect ratio, adjust orthographic size to maintain target width
                float differenceInSize = targetAspect / windowAspect;
                activeVirtualCamera.m_Lens.OrthographicSize = (targetHeight / 2f) * differenceInSize;
            }
        }
    }
}