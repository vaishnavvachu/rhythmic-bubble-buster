using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    public AudioSource audioSource; // Assign the audio source in the inspector
    public float sensitivity = 1.5f; // Sensitivity to detect beats
    public float minTimeBetweenBeats = 0.5f; // Minimum time between beats
    private float[] spectrumData = new float[64]; // Array to store spectrum data
    private float lastBeatTime;

    public delegate void OnBeat();
    public static event OnBeat BeatEvent;

    void Update()
    {
        // Get the audio spectrum data
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Check for a peak in the lower frequency range
        float sum = 0;
        for (int i = 0; i < 10; i++) // Focus on lower frequencies
        {
            sum += spectrumData[i];
        }

        if (sum > sensitivity && Time.time - lastBeatTime > minTimeBetweenBeats)
        {
            lastBeatTime = Time.time;

            // Trigger a beat event
            BeatEvent?.Invoke();
        }
    }
}