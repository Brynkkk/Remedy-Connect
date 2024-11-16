using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPopup : MonoBehaviour
{
    public GameObject overlay;          // Semi-transparent overlay
    public GameObject popup;            // Popup panel
    public Image popupImage;            // Image inside the popup
    public Sprite[] encyclopediaPages;  // Array of encyclopedia images
    private int currentIndex = 0;       // Tracks the current image index

    public Button leftArrow;
    public Button rightArrow;
    public Button closeButton;

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
}
