using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        public GameObject content;

        [Space]
    
        public bool isShown;

        [Space]
    
        public UnityEvent onPanelShown;
        public UnityEvent onPanelHidden;
 
        [HideInInspector] public Panel previousPanel;

        public virtual void Show()
        {
            isShown = true;
        
            content.SetActive(true);
        
            onPanelShown?.Invoke();
        }
    
        public void Show(Panel returnToPanel)
        {
            previousPanel = returnToPanel;
            Show();
        }
    
        public virtual void Hide()
        {
            content.SetActive(false);

            StartCoroutine(INEX.LateAction(() => isShown = false));

            onPanelHidden?.Invoke();
        }
    
        public void Show(float delay) => Invoke(nameof(Show), delay);

        public void Hide(float delay) => Invoke(nameof(Hide), delay);
    
        public void ReturnToPreviousPanel()
        {
            Hide();
            previousPanel.Show();
        }
    }
}
