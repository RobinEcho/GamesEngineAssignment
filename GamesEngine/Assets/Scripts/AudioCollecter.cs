using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioCollecter : MonoBehaviour
{
    AudioSource audioSource;

    // Calculate Audio Spectrum

    #region AudioConvert

    // rate that how long it will take. it must be 2^n, min64, nax 8192
    public static float[] samples = new float[512];

    // static in case any change by mistake, stored calculated data into frequencyBand
    public static float[] frequencyBand = new float[8];

    // buffer for postprocess
    public static float[] bandBuffer = new float[8];

    #endregion

    #region StoreBand

    // To store band into a changeable array
    float[] freqBandHighest = new float[8];
    public float[] audioBand = new float[8];
    public float[] audioBandBuffer = new float[8];
    
    #endregion

    #region PostProcess of audio

    float[] bufferDecrease = new float[8];
    public float Amplitude, AmplitudeBuffer;
    float AmplitudeHighest;

    #endregion


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }


    // Start is called before the first frame update
    void Start()
    {
        // sign audio and play
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void BandBuffer()
    {
        // post process on the band value to make the change slower which means more smooth, like lerp
        // If frequency band is higher than band buffer than the band buffer become frequency band, otherwise, bandbuffer will decrese by certain speed
        for (int g = 0; g < 8; g++)
        {
            if (frequencyBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = frequencyBand[g];
                // certain value we are going to use
                bufferDecrease[g] = 0.005f;
            }
            if (frequencyBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                // certain value we are going to use
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void GetSpectrumAudioSource()
    {
        // 0 here is channel, defualt as 0
        // last one is Enum, it usually got these mode:
        // Rectangular W[n] = 1.0.
        // Triangle W[n] = TRI(2n / N).
        // Hamming W[n] = 0.54 - (0.46 * COS(n / N)).
        // Hanning W[n] = 0.5 * (1.0 - COS(n / N)).
        // Blackman W[n] = 0.42 - (0.5 * COS(n / N)) + (0.08 * COS(2.0 * n / N)).
        // BlackmanHarris W[n] = 0.35875 - (0.48829 * COS(1.0 * n / N)) + (0.14128 * COS(2.0 * n / N)) - (0.01168 * COS(3.0 * n / N)).
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);



        // To create 8 different zone of 512 samples by calculate
        /*
        *  22050 / 512 - 43hz per sample
        *  
        *  20 - 60hz
        *  60 - 250hz
        *  250 - 500hz
        *  2000 - 4000hz
        *  4000 - 6000hz
        *  6000 - 20000hz
        *  
        *  0 - 2 = 86hz
        *  1 - 4 = 172hz - 87-258
        *  2 - 8 = 344hz - 259-602
        *  3 - 16 = 688hz - 603-1290
        *  4 - 32 = 1376hz - 1291-2666
        *  5 - 64 = 2752hz - 2667-5418
        *  6 - 128 = 5504hz - 5419-10922
        *  7 - 256 = 11008hz - 10923-21930
        *  510
        */

        int count = 0;

        // To specify how many samples are going to be put into every frequency band
        for (int i = 0; i < 8; i++)
        {
            float average = 0;

            // create an int goes to a power, here it increase 2 to 4 to 8 to 16 etc
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }

            // Put values of samples into different frequency band
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            frequencyBand[i] = average * 10;
        }

    }

    void CreateAudioBands()
    {
        // To store the highest value
        for (int i = 0; i < 8; i++)
        {
            // if current frequency band is higher than the highest of the frequency band
            if (frequencyBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = frequencyBand[i];
            }

            // To get the audio band and audio band buffer
            audioBand[i] = (frequencyBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }

    void GetAmplitude()
    {
        // create temporarily float
        float CurrentAmplitude = 0;
        float CurrentAmplitudeBuffer = 0;

        // get sum of all the audio bands
        for (int i = 0; i < 8; i++)
        {
            CurrentAmplitude += audioBand[i];
            CurrentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (CurrentAmplitude > AmplitudeHighest)
        {
            AmplitudeHighest = CurrentAmplitude;
        }

        Amplitude = CurrentAmplitude / AmplitudeHighest;
        AmplitudeBuffer = CurrentAmplitudeBuffer / AmplitudeHighest;
    }
}
