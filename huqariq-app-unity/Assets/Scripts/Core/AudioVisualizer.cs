using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [Range(1.0f,10000.0f)]
    public float multiplier;
    public int minRange = 0;
    public int maxRange = 64;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private AudioSource audioSource;

    private float prevAvg = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

	void FixedUpdate ()
    {
        if(audioSource.isPlaying)
        {
            float[] spectrum = new float[64];
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
            if (maxRange < minRange)
                maxRange = minRange + 1;

            minRange = Mathf.Clamp(minRange, 0, 63);
            maxRange = Mathf.Clamp(maxRange, 0, 63);
            float avg = 0;

            for (int i = minRange; i < maxRange; i++)
                avg += Mathf.Abs(spectrum[i]);

            avg = avg / (float)Mathf.Abs(maxRange - minRange);

            if (avg - prevAvg > 0.0012f)
                avg = prevAvg + 0.0012f;
            else if (avg - prevAvg < -0.0012f)
                avg = prevAvg - 0.0012f;

            skinnedMeshRenderer.SetBlendShapeWeight(0, avg*multiplier);

            prevAvg = avg;
        }
        else
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, 7.6f);
        }
	}
}
