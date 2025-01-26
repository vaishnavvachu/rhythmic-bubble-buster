using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the UI Text for tutorial messages
    public SpikeController spikeController; // Reference to the spike controller
    public GameObject bubbleSpawner; // Reference to the BubbleSpawner
    public GameObject finishMessage; // UI object to display the end of the tutorial

    private int tutorialStep = 0; // Tracks the current step of the tutorial
    private bool isWaitingForAction = false;

    void Start()
    {
        if (spikeController == null || tutorialText == null || bubbleSpawner == null)
        {
            Debug.LogError("TutorialController is missing required references!");
            return;
        }

        // Disable bubble spawner at the start
        bubbleSpawner.SetActive(false);

        // Start the tutorial
        StartTutorial();
    }

    void StartTutorial()
    {
        tutorialStep = 0;
        ShowMessage("Welcome to the game! Use the arrow keys or WASD to move the spike.", 5f);
        StartCoroutine(WaitForMovement());
    }

    void ShowMessage(string message, float duration = 3f)
    {
        tutorialText.text = message;
        tutorialText.gameObject.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(duration));
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialText.gameObject.SetActive(false);
    }

    IEnumerator WaitForMovement()
    {
        isWaitingForAction = true;
        while (true)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                break;
            }
            yield return null;
        }
        isWaitingForAction = false;
        OnMovementComplete();
    }

    void OnMovementComplete()
    {
        tutorialStep++;
        switch (tutorialStep)
        {
            case 1:
                ShowMessage("Great! Now, move closer to the bubbles to pop them.", 5f);
                bubbleSpawner.SetActive(true); // Enable bubble spawner
                break;

            case 2:
                ShowMessage("Pop the bubbles to complete the tutorial.", 5f);
                break;

            default:
                EndTutorial();
                break;
        }
    }

    public void OnBubblePopped()
    {
        if (tutorialStep == 2)
        {
            tutorialStep++;
            ShowMessage("Well done! You've completed the tutorial.", 5f);
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        tutorialText.text = "";
        bubbleSpawner.SetActive(false);
        if (finishMessage != null)
        {
            finishMessage.SetActive(true); // Display end of tutorial message
        }
        // Optional: Load the main game or continue gameplay
    }
}
