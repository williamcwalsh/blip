using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private UISkillTree uiSkillTree;
    public Ship ship;
    PlayerInputActions inputActions;
    [SerializeField] private Canvas UI;
    private bool showMenu;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        uiSkillTree.SetPlayerSkills(ship.GetPlayerSkills());
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
        if(!showMenu){
            UI.gameObject.SetActive(true);  // show
            showMenu = true;
        }else{
            UI.gameObject.SetActive(false); // hide
            showMenu=false;
        }
    }

    public void loadLevel (string levelName){
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    public void ReloadLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
}