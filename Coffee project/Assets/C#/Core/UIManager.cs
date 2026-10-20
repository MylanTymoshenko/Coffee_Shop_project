using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator OrderRequestTextAnimator;
    public TextMeshProUGUI OrderNameText;
    public Image CentrePointImg;

    public Sprite NormalSprite;
    public Sprite InteractSprite;

    public GameObject ObjectGrabbedTips;

    public void ShowObjectTips()
    {
        ObjectGrabbedTips.SetActive(true);
    }
    
    public void HideObjectTips()
    {
        ObjectGrabbedTips.SetActive(false);
    }

    public void ShowInteractImage()
    {
        CentrePointImg.sprite = InteractSprite;
    }

    public void HideInteractImage()
    {
        CentrePointImg.sprite = NormalSprite;
    }


}
