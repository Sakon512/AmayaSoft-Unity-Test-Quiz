using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField]
    private Text _question;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button bigRestartButton;
    [SerializeField]
    private Image fadeRestartImg;
    [SerializeField]
    private Image fadeWinImg;

    private DOTweenEffects DOTEffects;
    private GameTable gameTable;

    public Text GetQuestionField()
    {
        return _question;
    }

    public void SetQuestionText(string str)
    {
        _question.text = str;
    }

    public Button GetRestartButton()
    {
        return restartButton;
    }

    public Button GetBigRestartButton()
    {
        return bigRestartButton;
    }

    public Image GetFadeRestartImg()
    {
        return fadeRestartImg;
    }

    public Image GetFadeWinImg()
    {
        return fadeWinImg;
    }

    public void ButtonPopUp(Button button)                         
    {
        button.gameObject.SetActive(true);
        button.transform.localScale = Vector2.zero;
        DOTEffects.RisingBounceEffect(button.transform);
    }

    public void RestartFadeIn()                                   
    {
        GetFadeRestartImg().gameObject.SetActive(true);
        DOTEffects.ImageFadeIn(GetFadeRestartImg());
    }

    public void SetImageAlpha(Image image, int alpha)                
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public void SetTextAlpha(Text text, int alpha)                      
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public void ResetTableUI()                           
    {
        SetImageAlpha(GetFadeWinImg(), 0);
        SetTextAlpha(GetQuestionField(), 0);
        GetRestartButton().gameObject.SetActive(false);
        GetBigRestartButton().gameObject.SetActive(false);
        GetFadeWinImg().gameObject.SetActive(false);
        gameTable.ClearLvlTable();
        DOTEffects.ImageFadeOut(GetFadeRestartImg());
    }

    private void Awake()
    {
        DOTEffects = GetComponent<DOTweenEffects>();
        gameTable = GetComponent<GameTable>();
    }
}
