using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonClicker : MonoBehaviour
{
    public Button Button;
    public TextMeshProUGUI Text;
    public int Count;

    private void Start()
    {
        Button.onClick.AddListener(ButtonClicked);
    }
    void ButtonClicked()
    {
        Count++;
        Text.text = "Clicks: " + Count; 
    }
}
