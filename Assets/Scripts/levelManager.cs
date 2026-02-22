using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string menuSceneName = "MenuScene";

    PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Escape.performed += OnEscapePressed;
    }

    void OnDisable()
    {
        inputActions.Player.Escape.performed -= OnEscapePressed;
        inputActions.Disable();
    }

    void OnEscapePressed(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void loadLevel (string levelName){
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    public void ReloadLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
}