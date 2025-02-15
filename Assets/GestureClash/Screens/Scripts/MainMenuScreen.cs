using UnityEngine;
using UnityEngine.SceneManagement;
namespace GestureClash
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private ActionButtonWithText _playButton;

        #region UnityMethods

        private void OnEnable()
        {
            SetUpGame(); 
        }
        #endregion UnityMethods 

        private void SetUpGame()
        {
            _playButton.InitializeActionButton("PLAY", OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}