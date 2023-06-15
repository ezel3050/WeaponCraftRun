using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToResumePanel : Panel
{
    // private void Update()
    // {
    //     if (!isShown) return;
    //     
    //     if (InputManager.InteractionDown) Hide();
    // }

    public override void Show()
    {
        if (isShown) return;
        
        base.Show();

        Managers.GameManager.Instance.StopGame();
    }

    public override void Hide()
    {
        if (!isShown) return;
        
        base.Hide();
        
        Managers.GameManager.Instance.ResumeGame();
    }
}
