using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Button btnSpeedUp;
    [SerializeField] private Button btnSpeedDown;

    [SerializeField] private TextMeshProUGUI txtCollectableCount;

    [SerializeField] private GameObject appleIconPrefab;
    [SerializeField] private Transform appleIconDesTr;

    public static UnityAction<float> onClickSpeedBtn;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ScoreManager.onUpdateCollectable += UpdateResourceBar;
        UpdateResourceBar(0);
    }

    private void Start()
    {
        btnSpeedUp.onClick.AddListener(SpeedUp);
        btnSpeedDown.onClick.AddListener(SpeedDown);
    }

    private void SpeedDown() => onClickSpeedBtn?.Invoke(-.5f);

    private void SpeedUp() => onClickSpeedBtn?.Invoke(+.5f);

    private void OnDestroy() => ScoreManager.onUpdateCollectable -= UpdateResourceBar;

    private void UpdateResourceBar(int count)
    {
        txtCollectableCount.text = count.ToString();
    }

    public void ShowCollectUIAnimation(UnityAction onCompleteAnimation)
    {
        GameObject appleIcon = Instantiate(appleIconPrefab, this.transform);
        appleIcon.GetComponent<RectTransform>().DOMove(appleIconDesTr.position, 1.0f).OnComplete(delegate
        {
            onCompleteAnimation?.Invoke();
            Destroy(appleIcon);
        });
    }
}