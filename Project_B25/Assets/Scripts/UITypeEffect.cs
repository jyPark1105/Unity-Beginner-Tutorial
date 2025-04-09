using UnityEngine;
using UnityEngine.UI;
// TextMeshPro
using TMPro;
using UnityEngine.Audio;

public class UITypeEffect : MonoBehaviour
{
    // Save Message
    public string targetMsg;

    // Typing Speed
    int CharPerSeconds;
    float interval;

    // String Index
    int index;

    // Message to UI
    TMP_Text UIMsgText;
    // Animation Execution Flag
    public bool isTyping;

    // Sound Effect
    AudioSource audioSource;

    // End Effect
    public GameObject EndCursor;

    void Awake()
    {
        UIMsgText = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        isTyping = false;

        // Typing Speed
        CharPerSeconds = 10;
    }

    public void SetMsg(string msg)
    {
        // Branch Logic with Flag Variable
        if (isTyping)
        {
            // Fill the Rest
            UIMsgText.text = targetMsg;
            // Invoke Cancel
            CancelInvoke("Effecting");
            // End Effect
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        // Initialization
        UIMsgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;

        // Invoke with Typing Speed: [0]
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        // Out of "Effecting Function"
        if (UIMsgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        // Start Animation
        UIMsgText.text += targetMsg[index];
        // Exclude Space & Period
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();
        index++;

        isTyping = true;

        // Invoke with Typing Speed: [1] ~ [targetMsg.Length - 1]
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isTyping = false;
        EndCursor.SetActive(true);
    }
}