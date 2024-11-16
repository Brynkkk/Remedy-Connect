using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the Main Camera
    public Camera screenCamera; // Reference to the Screen Camera
    public GameObject switchToScreenButton; // Button to switch to Screen Camera
    public GameObject switchToMainButton; // Button to switch to Main Camera

    // Start is called before the first frame update
    void Start()
    {
        ActivateMainCamera();
    }

    // Activate the Main Camera and its button
    public void ActivateMainCamera()
    {
        mainCamera.gameObject.SetActive(true);
        screenCamera.gameObject.SetActive(false);

        switchToScreenButton.SetActive(true);
        switchToMainButton.SetActive(false);
    }

    // Activate the Screen Camera and its button
    public void ActivateScreenCamera()
    {
        mainCamera.gameObject.SetActive(false);
        screenCamera.gameObject.SetActive(true);

        switchToScreenButton.SetActive(false);
        switchToMainButton.SetActive(true);
    }
}
