using UnityEngine;
using VContainer;

namespace Project.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject hudPanel;
        [SerializeField] private GameObject finishPanel;

        private FinishScreenController _finishController;

        [Inject]
        public void Construct(FinishScreenController finish)
        {
            _finishController = finish;
        }

        private void Start()
        {
            ShowHUD();
        }

        public void ShowHUD()
        {
            hudPanel.SetActive(true);
            finishPanel.SetActive(false);
        }

        public void ShowFinishScreen()
        {
            hudPanel.SetActive(false);
            finishPanel.SetActive(true);
            _finishController.UpdateFinishInfo();
        }
    }
}
