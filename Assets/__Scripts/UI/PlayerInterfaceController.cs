using UnityEngine;
using UnityEngine.UI;

public class PlayerInterfaceController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Transform detectButton;
    [SerializeField] private Transform generateButton;
    [SerializeField] private Transform resetButton;

    [Space(20)]
    [SerializeField] private Transform nodeToDetectSlider;

    [Space(20)]
    [SerializeField] private PlaneController planeController;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        planeController.PlaneDetectionDone += OnPlaneDetectionDone;

        detectButton.GetComponent<Button>().onClick.AddListener(playerController.OnDetectButtonPressed);

        Button generate = generateButton.GetComponent<Button>();
        generate.onClick.AddListener(playerController.OnGeneratePuzzlePressed);
        generate.onClick.AddListener(OnGeneratePuzzlePressed);

        Button reset = resetButton.GetComponent<Button>();
        reset.onClick.AddListener(playerController.OnResetPuzzlePressed);
        reset.onClick.AddListener(OnResetPuzzlePressed);

        detectButton.gameObject.SetActive(true);
        nodeToDetectSlider.gameObject.SetActive(true);
        generateButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);

        Slider slider = nodeToDetectSlider.GetComponent<Slider>();
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnNodeToDetectValueChanged);
        }
    }

    private void OnPlaneDetectionDone()
    {
        detectButton.gameObject.SetActive(false);
        nodeToDetectSlider.gameObject.SetActive(false);
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
