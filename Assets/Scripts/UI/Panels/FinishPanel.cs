using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FinishPanel : UIPanel
{
    [Header("Fail")]
    [SerializeField] private CanvasGroup m_FailCanvas;
    [SerializeField] private int m_FailReward = 100;

    [Header("Success")]
    [SerializeField] private CanvasGroup m_SuccessCanvas;
    [SerializeField] private int m_SuccessReward = 300;
    [Header("Tween")]
    [SerializeField] private float m_RewardTweenDuration = 3.0f;
    [SerializeField] private AnimationCurve m_RewardTweenCurve;
    [SerializeField] private float m_StartTweenDelay = 2.0f;
    private int m_CurrentRewardCoinCount;
    [SerializeField] private TextMeshProUGUI m_CoinCountText;
    [SerializeField] private TextMeshProUGUI m_RewardCoinText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        m_RewardCoinTweenID = GetInstanceID() + "m_RewardCoinTweenID";


        GameManager.Instance.OnLevelCompleted += ShowSuccessPanel;
        GameManager.Instance.OnLevelFailed += ShowFailPanel;
        GameManager.Instance.OnLevelCompleted += ShowPanel;
        GameManager.Instance.OnLevelFailed += ShowPanel;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        m_RewardCoinText.text = GameManager.Instance.PlayerManager.GetTotalCoinCount().ToString();
        m_CoinCountText.text = m_CurrentRewardCoinCount.ToString();


        StartRewardCoinTween();
    }
    private void ShowSuccessPanel()
    {
        m_CurrentRewardCoinCount = m_SuccessReward;
        m_SuccessCanvas.Close();
        m_FailCanvas.Open();
    }
    private void ShowFailPanel()
    {
        m_CurrentRewardCoinCount = m_FailReward;
        m_FailCanvas.Close();
        m_SuccessCanvas.Open();
    }

    private Coroutine m_StartRewardCoinCoroutine;
    private void StartRewardCoinTween()
    {
        if (m_StartRewardCoinCoroutine != null)
        {
            StopCoroutine(m_StartRewardCoinCoroutine);
        }

        m_StartRewardCoinCoroutine = StartCoroutine(RewardCoinCoroutine());
    }
    private IEnumerator RewardCoinCoroutine()
    {
        yield return new WaitForSecondsRealtime(m_StartTweenDelay);
        RewardCoinTween();
    }
    private string m_RewardCoinTweenID;
    private float m_CoinTextLerpValue;
    private int m_StartTotalCoinCount, m_FinishTotalCoinCount;
    private int m_StartRewardCoinCount;
    private int m_TempTotalCoinCount, m_TempRewardCoinCount;
    private void RewardCoinTween()
    {
        DOTween.Kill(m_RewardCoinTweenID);
        m_CoinTextLerpValue = 0.0f;

        m_StartRewardCoinCount = m_CurrentRewardCoinCount;
        m_StartTotalCoinCount = GameManager.Instance.PlayerManager.GetTotalCoinCount();

        m_FinishTotalCoinCount = m_StartRewardCoinCount + m_StartTotalCoinCount;

        DOTween.To(() => m_CoinTextLerpValue, x => m_CoinTextLerpValue = x, 1.0f, m_RewardTweenDuration).
        OnUpdate(() =>
        {
            SetFinishPanelCoins();
        }).
        OnComplete(() =>
        {
            GameManager.Instance.PlayerManager.UpdateCoinCountData(m_FinishTotalCoinCount);
        }).
        SetEase(m_RewardTweenCurve).
        SetId(m_RewardCoinTweenID);
    }

    private void SetFinishPanelCoins()
    {
        m_TempRewardCoinCount = (int)Mathf.Lerp(m_StartRewardCoinCount, 0, m_CoinTextLerpValue);
        m_TempTotalCoinCount = (int)Mathf.Lerp(m_StartTotalCoinCount, m_FinishTotalCoinCount, m_CoinTextLerpValue);

        m_RewardCoinText.text = m_TempRewardCoinCount.ToString();
        m_CoinCountText.text = m_TempTotalCoinCount.ToString();
    }

    public void ContinueButton(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.PlayerManager.UpdateLevelData((GameManager.Instance.PlayerManager.GetLevelNumber() + 1));
        }

        GameManager.Instance.ResetToMainMenu();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelCompleted -= ShowPanel;
        GameManager.Instance.OnLevelCompleted -= ShowSuccessPanel;
        GameManager.Instance.OnLevelFailed -= ShowPanel;
        GameManager.Instance.OnLevelFailed -= ShowFailPanel;
    }
}
