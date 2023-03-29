using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] private TMP_Text ValueText;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);

        // Initialize text.
        OnValueChanged(slider.value);
    }

    private void OnValueChanged(float inValue)
    {
        ValueText.text = inValue.ToString();
    }
}
