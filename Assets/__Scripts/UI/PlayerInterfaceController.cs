using UnityEngine;
using UnityEngine.UI;

public class PlayerInterfaceController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button detectButton;
    [SerializeField] private Button generateButton;
    [SerializeField] private Button resetButton;

    [Space(20)]
    [SerializeField] private Slider nodeToDetectSlider;

    [Space(20)]
    [SerializeField] private PlaneController planeController;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        planeController.PlaneDetectionDone += OnPlaneDetectionDone;

        detectButton.onClick.AddListener(playerController.OnDetectButtonPressed);
        detectButton.onClick.AddListener(OnDetectButtonPressed);

        generateButton.onClick.AddListener(playerController.OnGeneratePuzzlePressed);
        generateButton.onClick.AddListener(OnGeneratePuzzlePressed);

        resetButton.onClick.AddListener(playerController.OnResetPuzzlePressed);
        resetButton.onClick.AddListener(OnResetPuzzlePressed);

        detectButton.gameObject.SetActive(true);
        nodeToDetectSlider.gameObject.SetActive(true);
        generateButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);

        nodeToDetectSlider.onValueChanged.AddListener(OnNodeToDetectValueChanged);
        OnNodeToDetectValueChanged(nodeToDetectSlider.value);
    }

    private void OnDetectButtonPressed()
    {
        detectButton.gameObject.SetActive(false);
        nodeToDetectSlider.gameObject.SetActive(false);
    }

    private void OnPlaneDetectionDone()
    {
        generateButton.gameObject.SetActive(true);
    }

    private void OnGeneratePuzzlePressed()
    {
        generateButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
    }

    private void OnResetPuzzlePressed()
    {
        resetButton.gameObject.SetActive(false);
        detectButton.gameObject.SetActive(true);
        nodeToDetectSlider.gameObject.SetActive(true);
    }

    private void OnNodeToDetectValueChanged(float inValue)
    {
        planeController.PlanesToDetect = (int)inValue;
    }
}
