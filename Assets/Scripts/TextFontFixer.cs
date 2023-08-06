using TMPro;
using UnityEngine;

public class TextFontFixer : MonoBehaviour
{
    public TMP_FontAsset theFont;
    void Start()
    {
        return;
        this.CallWithDelay(() =>
        {
            foreach(TextMeshProUGUI textMeshProUGUI in FindObjectsOfType<TextMeshProUGUI>())
            {
                if(textMeshProUGUI.font == null)
                {
                    textMeshProUGUI.font = theFont;
                }
            }
            
        }, 2);
    }
}
