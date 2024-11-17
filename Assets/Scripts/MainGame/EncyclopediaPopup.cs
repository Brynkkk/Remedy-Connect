using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPopup : MonoBehaviour
{
    public GameObject overlay;          // Semi-transparent overlay
    public GameObject popup;            // Popup panel
    public Image popupImage;            // Image inside the popup
    public Sprite[] encyclopediaPages;  // Array of encyclopedia images
    private int currentIndex = 0;       // Tracks the current image index

    public Button leftArrow;            // Button for navigating to the previous image
    public Button rightArrow;           // Button for navigating to the next image
    public Button closeButton;          // Button for closing the popup
    public Button encyclopediaButton;   // Button to open the encyclopedia
    public Button changeScreenButton;   // Button to switch cameras (correct reference now)

    public Camera mainCamera;           // Reference to the Main Camera
    public Camera screenCamera;         // Reference to the Screen Camera

    // Show the popup and overlay, but only if the Main Camera is active
    public void OpenPopup()
    {
        if (mainCamera.isActiveAndEnabled)
        {
            overlay.SetActive(true);
            popup.SetActive(true);
            currentIndex = 0;
            UpdatePopupImage();

            // Disable buttons during popup
            if (changeScreenButton != null)
                changeScreenButton.interactable = false;

            if (encyclopediaButton != null)
                encyclopediaButton.interactable = false;
        }
        else
        {
            Debug.Log("Encyclopedia can only be opened on the Main Camera.");
        }
    }

    // Close the popup and overlay
    public void ClosePopup()
    {
        overlay.SetActive(false);
        popup.SetActive(false);

        // Re-enable buttons after closing popup
        if (changeScreenButton != null)
            changeScreenButton.interactable = true;

        if (encyclopediaButton != null)
            encyclopediaButton.interactable = true;
    }

    // Show the previous image
    public void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdatePopupImage();
        }
    }

    // Show the next image
    public void NextImage()
    {
        if (currentIndex < encyclopediaPages.Length - 1)
        {
            currentIndex++;
            UpdatePopupImage();
        }
    }

    // Update the popup image based on the current index
    private void UpdatePopupImage()
    {
        popupImage.sprite = encyclopediaPages[currentIndex];
        leftArrow.interactable = currentIndex > 0;
        rightArrow.interactable = currentIndex < encyclopediaPages.Length - 1;
    }

    // Dynamically toggle buttons based on active camera
    void Update()
    {
        if (mainCamera.isActiveAndEnabled)
        {
            // Show buttons only on the Main Camera
            if (changeScreenButton != null)
                changeScreenButton.gameObject.SetActive(true);

            if (encyclopediaButton != null)
                encyclopediaButton.gameObject.SetActive(true);
        }
        else if (screenCamera.isActiveAndEnabled)
        {
            // Hide buttons on the Screen Camera
            if (changeScreenButton != null)
                changeScreenButton.gameObject.SetActive(false);

            if (encyclopediaButton != null)
                encyclopediaButton.gameObject.SetActive(false);
        }
    }
}