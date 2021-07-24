using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TTSPlayer : MonoBehaviour
{
    AudioSource ttsAudioSource;

    string currentTTSText;

    string TTSApiKey;
    string RapidApiKey;

    void Awake ()
    {
        ttsAudioSource = GetComponent<AudioSource> ();
        try
        {
            TTSApiKey = Resources.Load<Env>("ENV/TTS_API_KEY").Value;
            RapidApiKey = Resources.Load<Env>("ENV/RAPID_API_KEY").Value;

        }
        catch (Exception e)
        {
            Debug.LogError("Can\'t load the ENV file");
        }
    }

    public void PlayVoice (string incomingText)
    {
        StartCoroutine (PlayVoiceRoutine (incomingText));
    }

    public void SetTTSApiKey(string value)
    {
        if(string.IsNullOrEmpty(TTSApiKey) && !string.IsNullOrEmpty(value))
            TTSApiKey = value;
    }
    public void SetRapidApiKey(string value)
    {
        if(string.IsNullOrEmpty(RapidApiKey) && !string.IsNullOrEmpty(value))
            RapidApiKey = value;
    }

    IEnumerator PlayVoiceRoutine (string incomingText)
    {
        if (string.IsNullOrEmpty(incomingText) || string.IsNullOrEmpty(TTSApiKey) || string.IsNullOrEmpty(RapidApiKey))
        {
            if(string.IsNullOrEmpty(incomingText)) Debug.LogError("Text is empty");
            if(string.IsNullOrEmpty(TTSApiKey)) Debug.LogError("TTS API Key not found");
            if(string.IsNullOrEmpty(RapidApiKey)) Debug.LogError("Rapid API Key not found");
            yield break;
        }

        if (currentTTSText != incomingText)
        {
            currentTTSText = incomingText;

            string url = $"https://voicerss-text-to-speech.p.rapidapi.com/?key={TTSApiKey}&hl=es-es&src={currentTTSText}&f=8khz_8bit_mono&c=wav&r=0";

            UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip (url, AudioType.WAV);

            audioRequest.SetRequestHeader ("x-rapidapi-key", RapidApiKey);
            audioRequest.SetRequestHeader ("x-rapidapi-host", "voicerss-text-to-speech.p.rapidapi.com");
            yield return audioRequest.SendWebRequest ();

            if (audioRequest.isNetworkError || audioRequest.isHttpError)
            {
                Debug.LogError (audioRequest.error);
            }
            else
            {
                ttsAudioSource.clip = DownloadHandlerAudioClip.GetContent (audioRequest);
                ttsAudioSource.Play ();
            }
            audioRequest.Dispose ();
        }
        else
        {
            if (ttsAudioSource.clip != null) ttsAudioSource.Play ();
        }
        yield return null;
    }
}