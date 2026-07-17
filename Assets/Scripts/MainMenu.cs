using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button fallbackButton;

    private MainInputAction _mainInputAction;
    private InputAction _navigateInputAction;

    private void Awake()
    {
        _mainInputAction = new MainInputAction();
    }

    private void OnEnable()
    {
        _navigateInputAction = _mainInputAction.UI.Navigate;
        _navigateInputAction.Enable();

        _navigateInputAction.performed += OnNavigatePositionChanged;
        _navigateInputAction.canceled += OnNavigatePositionChanged;
    }

    private void OnDisable()
    {
        _navigateInputAction.performed -= OnNavigatePositionChanged;
        _navigateInputAction.canceled -= OnNavigatePositionChanged;

        _navigateInputAction.Disable();
    }

    public void Play()
    {
        SceneManager.LoadScene("Field");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnNavigatePositionChanged(InputAction.CallbackContext obj)
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(fallbackButton.gameObject);
        }
    }
}