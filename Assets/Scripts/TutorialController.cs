using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the UI Text for tutorial messages
    public SpikeController spikeController; // Reference to the spike controller
    public BubbleSpawner bubbleSpawner; // Reference to the BubbleSpawner
    public GameObject finishMessage; // UI object to display the end of the tutorial
    public PoolManager poolManager;

    private int tutorialStep = 0; // Tracks the current step of the tutorial
    private bool isWaitingForAction = false;
    

    void Start()
    {
        if (spikeController == null || tutorialText == null || bubbleSpawner == null || poolManager == null)
        {
            Debug.LogError("TutorialController is missing required references!");
            return;
        }

        // Disable bubble spawner at the start
        bubbleSpawner.gameObject.SetActive(false);

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
                tutorialStep++;
                ShowMessage("Great! Now, move closer to the bubbles to pop them.", 5f);
                bubbleSpawner.gameObject.SetActive(true); // Enable bubble spawner
                break;

            case 2:
                ShowMessage("Pop the bubbles to complete the tutorial.", 5f);
                break;
            case 3:
                ShowMessage("Now, collect the power-up bubble to change the spike's color.", 5f);
                SpawnPowerUpBubble(); // Trigger bubble spawning, which can include power-ups
                break;

            case 4:
                ShowMessage("Press Space to swap between collected colors.", 5f);
                break;

            default:
                EndTutorial();
                break;
        }
    }
    private void SpawnPowerUpBubble()
    {
        // Temporarily bypass level-based conditions for power-up spawning
        bubbleSpawner.SpawnPowerUpAtPosition(bubbleSpawner.spawnPoints[Random.Range(0, bubbleSpawner.spawnPoints.Length)].position, BubbleColor.Blue);
    }

    public void OnBubblePopped()
    {
        Debug.Log("OnBubblePopped called!"); 
        if (tutorialStep == 3)
        {
            tutorialStep++;
            ShowMessage("Well done! You've completed the tutorial.", 5f);
            //EndTutorial();
        }
        else if (tutorialStep == 2)
        {
            // Once the player collects the power-up bubble, proceed to the next step
            tutorialStep++;
            ShowMessage("Good job! You've collected the power-up bubble.", 5f);
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        tutorialText.text = "";
        bubbleSpawner.gameObject.SetActive(false);
        if (finishMessage != null)
        {
            finishMessage.SetActive(true); // Display end of tutorial message
        }
        // Optional: Load the main game or continue gameplay
    }
}
