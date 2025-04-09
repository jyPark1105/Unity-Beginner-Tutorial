using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Public Class
    // Previous: public TMP_Text UITalkText;
    public UITypeEffect UITalkText;

    // UI Image
    public Image UINpcPortraitImage;
    public Animator UINPCPortraitAnim;
    // Save Previous Sprite
    public Sprite UIPreviousNPCPortrait;
    // UI Text Data
    public Animator UITalkPanel;

    void Awake()
    {
        UITalkPanel.SetBool("isShow", false);
    }

    public void DoEffectNpcPortraitAnimation()
    {
        if (UIPreviousNPCPortrait != UINpcPortraitImage.sprite)
        {
            // "Trigger"
            UINPCPortraitAnim.SetTrigger("doEffect");
            
            UIPreviousNPCPortrait = UINpcPortraitImage.sprite;
        }
    }
}
