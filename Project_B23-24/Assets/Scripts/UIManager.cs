using UnityEngine;
using UnityEngine.UI;
// TextMeshPro
using TMPro;

public class UIManager : MonoBehaviour
{
    // UI Image
    public Image UINpcPortraitImage;
    // UI Text Data
    public GameObject UITalkPanel;
    public TMP_Text UITalkText;

    void Awake()
    {
        UITalkPanel.SetActive(false);
    }

    void Update()
    {
        
    }
}
