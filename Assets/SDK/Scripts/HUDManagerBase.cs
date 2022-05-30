using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using MetalGamesSDK;
using TMPro;

public class HUDManagerBase : Singleton<HUDManager>
{
    #region References

    public CanvasGroup alertGroup;

    public RectTransform wrongPathRct;
    public TextMeshProUGUI alerttext;

    [ShowInInspector] public ReferenceStruct References;
    [SerializeField, Space(20)] protected List<GameObject> ActiveScreens = new List<GameObject>();

    [Button]
    public void GetRefeences()
    {
        References.WelcomeScreen = transform.FindDeepChild<RectTransform>("welcome");
        References.InGamePanel = transform.FindDeepChild<RectTransform>("in_game");
        References.LevelCompletePanel = transform.FindDeepChild<RectTransform>("levelcomplete");
        References.LevelFailPanel = transform.FindDeepChild<RectTransform>("levelfail");
        References.StartBtn = transform.FindDeepChild<RectTransform>("startbutton");
        References.SettingsPanel = transform.FindDeepChild<RectTransform>("settings");
        References.BgPanel = transform.FindDeepChild<RectTransform>("bgpanel");
        References.CurrencyTxt = transform.FindDeepChild<TextMeshProUGUI>("currencytxt");
        References.CurrencyImg = transform.FindDeepChild<RectTransform>("currencyimg");
        References.LevelTxt = transform.FindDeepChild<TextMeshProUGUI>("leveltxt");
        GameConfig.Instance.HUD.CurrencyImgScaleFactor = References.CurrencyImg.localScale.x;
    }

    #endregion

    #region Methods

    public void UpdateLevelTxt()
    {
        References.LevelTxt.text = "Level " + StorageManager.Instance.CurrentLevel.ToString();
    }

    public void UpdateCurrencyTxt()
    {
        References.CurrencyTxt.text = StorageManager.Instance.Wallet.Currency.ToString();
    }

    private void OnCurrencyChanged(int Currency)
    {
        References.CurrencyTxt.text = Currency.ToString();
        PunchRect(References.CurrencyImg, 1.18f, 0.01f, 2, 0, GameConfig.Instance.HUD.CurrencyImgScaleFactor);
    }

    public void ShowLevelCompletePanel()
    {
        ShowScreen(References.LevelCompletePanel);
    }

    #endregion

    #region LoopFunctions

    public CanvasGroup ScoreBoard;

    void showScoreCanvas()
    {
        ScoreBoard.DOFade(1, 1.4f);
    }

    public void OnEnable()
    {
        GameManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLevelStarted += OnLevelStarted;
        GameManager.OnCurrencyChanged += OnCurrencyChanged;
        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnLevelFailed += OnLevelFailed;
        PunchRect(References.StartBtn, -1, false);
        
    }

    public void OnDisable()
    {
        GameManager.OnLevelLoaded -= OnLevelLoaded;
        GameManager.OnLevelStarted -= OnLevelStarted;
        GameManager.OnCurrencyChanged -= OnCurrencyChanged;
        GameManager.OnLevelCompleted -= OnLevelCompleted;
        GameManager.OnLevelFailed -= OnLevelFailed;
      
    }


    private void OnLevelStarted()
    {
        DectivateScreenObj(References.WelcomeScreen.gameObject);
        ActivateScreenObj(References.InGamePanel.gameObject);
        // DectivateScreenObj(HUDButtons.Instance.Buttons.SettingsButton.gameObject);
    }

    public Ease alertScaleEase, alertSlideEase;

    public void OnWrongPath()
    {
        DOTween.Kill(transform);
        alerttext.text = "TRESPASSING NOT ALLOWED";
        alertGroup.DOFade(1, 0.3f);
        wrongPathRct.DOLocalMoveX(0, 0.3f).SetEase(alertSlideEase);
        Vector3 scale = wrongPathRct.localScale;
        wrongPathRct.localScale = Vector3.zero;
        wrongPathRct.DOScale(scale, 0.3f).SetEase(alertScaleEase);
        DOVirtual.DelayedCall(0.8f, delegate { hideAlert(); });
    }

    public void OnIncompletePath()
    {
        DOTween.Kill(transform);
        alerttext.text = "INCOMPLETE PATH / DRAW COMPLETE PATH";
        alertGroup.DOFade(1, 0.3f);
        wrongPathRct.DOLocalMoveX(0, 0.3f).SetEase(alertSlideEase);
        Vector3 scale = wrongPathRct.localScale;
        wrongPathRct.localScale = Vector3.zero;
        wrongPathRct.DOScale(scale, 0.3f).SetEase(alertScaleEase);
        DOVirtual.DelayedCall(1.4f, delegate { hideAlert(); });
    }

    void hideAlert()
    {
        alertGroup.DOFade(0, 0.2f);


        wrongPathRct.DOLocalMoveX(600, 0.3f).OnComplete(delegate { wrongPathRct.DOLocalMoveX(-1000, 0); });
    }

    private void OnLevelCompleted()
    {
        ShowLevelCompletePanel();

        TurnOffAllScreens(References.LevelCompletePanel.gameObject);
    }

    private void OnLevelFailed()
    {
        ShowScreen(References.LevelFailPanel);
        TurnOffAllScreens(References.LevelFailPanel.gameObject);
    }


    private void OnLevelLoaded()
    {
        TurnOffAllScreens();

        ActivateScreenObj(References.WelcomeScreen.gameObject);
        //   ActivateScreenObj(References.InGamePanel.gameObject);
        ActivateScreenObj(HUDButtons.Instance.Buttons.SettingsButton.gameObject);


        UpdateLevelTxt();
        UpdateCurrencyTxt();
    }

    #endregion

    #region Utilities

    public void PunchRect(RectTransform i_Rect, int i_Loop = -1, bool i_AddtoActiveScreen = true)
    {
        i_Rect.DOScale(i_Rect.localScale * GameConfig.Instance.HUD.PunchScale, GameConfig.Instance.HUD.PunchTime)
            .SetEase(GameConfig.Instance.HUD.PunchEase).SetDelay(1).SetLoops(i_Loop, LoopType.Yoyo);
        if (!ActiveScreens.Contains(i_Rect.gameObject) && i_AddtoActiveScreen)
            ActiveScreens.Add(i_Rect.gameObject);
    }

    public void PunchRect(RectTransform i_Rect, float i_ScaleFactor, float i_Time, int i_Loop = -1, float i_Delay = 1,
        float i_OriginalScaleFactor = 1, bool i_AddtoActiveScreen = true)
    {
        i_Rect.localScale = Vector3.one * i_OriginalScaleFactor;
        i_Rect.DOScale(i_Rect.localScale * i_ScaleFactor, i_Time)
            .SetEase(GameConfig.Instance.HUD.PunchEase).SetDelay(i_Delay).SetLoops(i_Loop, LoopType.Yoyo);
        if (!ActiveScreens.Contains(i_Rect.gameObject) && i_AddtoActiveScreen)
            ActiveScreens.Add(i_Rect.gameObject);
    }


    public delegate void Method();
    //public void ShowScreen(RectTransform i_Screen, float i_Scale, float i_ShowTime)
    //{

    //}
    //public void ShowScreen(RectTransform i_Screen, float i_Scale)
    //{
    //}
    public void ShowScreen(RectTransform i_Screen, Vector3 Scale, float i_ScaleTime)
    {
        i_Screen.localScale = Vector3.zero;
        i_Screen.gameObject.SetActive(true);
        i_Screen.DOScale(Scale, i_ScaleTime).SetUpdate(true)
            .SetEase(GameConfig.Instance.HUD.Tween).OnComplete(() => { });

        if (!ActiveScreens.Contains(i_Screen.gameObject))
            ActiveScreens.Add(i_Screen.gameObject);
    }

    /// <summary>
    ///Scale And Show RectrancformScreen
    /// </summary>
    public void ShowScreen(RectTransform i_Screen, Method i_Functintocall = null)
    {
        i_Screen.localScale = Vector3.zero;
        i_Screen.gameObject.SetActive(true);
        i_Screen.DOScale(GameConfig.Instance.HUD.Scale, GameConfig.Instance.HUD.ScaleTime).SetUpdate(true)
            .SetEase(GameConfig.Instance.HUD.Tween).OnComplete(() =>
            {
                if (i_Functintocall != null)
                    i_Functintocall();
            });

        if (!ActiveScreens.Contains(i_Screen.gameObject))
            ActiveScreens.Add(i_Screen.gameObject);
    }

    /// <summary>
    ///Scale And Hide RectrancformScreen
    /// </summary>
    public void HideScreen(RectTransform i_Screen, Method i_Functintocall = null)
    {
        i_Screen.DOScale(Vector3.zero, GameConfig.Instance.HUD.ScaleTime).SetUpdate(true)
            .SetEase(GameConfig.Instance.HUD.Tween).OnComplete(() =>
            {
                if (i_Functintocall != null)
                    i_Functintocall();
            });


        if (ActiveScreens.Contains(i_Screen.gameObject))
            ActiveScreens.Remove(i_Screen.gameObject);
    }

    public void ActivateScreenObj(GameObject i_Screen)
    {
        i_Screen.gameObject.SetActive(true);
        if (!ActiveScreens.Contains(i_Screen.gameObject))
            ActiveScreens.Add(i_Screen.gameObject);
    }

    public void DectivateScreenObj(GameObject i_Screen)
    {
        i_Screen.SetActive(false);
        if (ActiveScreens.Contains(i_Screen.gameObject))
            ActiveScreens.Remove(i_Screen.gameObject);
    }

    public void TurnOffAllScreens(GameObject i_ExcludedScreen = null)
    {
        List<GameObject> screens = new List<GameObject>(ActiveScreens);
        foreach (GameObject GM in screens)
        {
            if (GM != i_ExcludedScreen)
            {
                DectivateScreenObj(GM);
            }
        }
    }

    #endregion
}

[System.Serializable]
public class ReferenceStruct
{
    [Title("RectTransforms")] [SerializeField, ReadOnly]
    public RectTransform WelcomeScreen;

    [SerializeField, ReadOnly] public RectTransform InGamePanel;
    [SerializeField, ReadOnly] public RectTransform LevelCompletePanel;
    [SerializeField, ReadOnly] public RectTransform LevelFailPanel;
    [SerializeField, ReadOnly] public RectTransform StartBtn;
    [SerializeField, ReadOnly] public RectTransform SettingsPanel;
    [SerializeField, ReadOnly] public RectTransform BgPanel;
    [SerializeField, ReadOnly] public RectTransform CurrencyImg;

    [Title("TMP")] [SerializeField, ReadOnly]
    public TextMeshProUGUI LevelTxt;

    [SerializeField, ReadOnly] public TextMeshProUGUI CurrencyTxt;
}