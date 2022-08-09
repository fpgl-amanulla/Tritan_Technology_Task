using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Button btnSpeedUp;
    [SerializeField] private Button btnSpeedDown;
    [SerializeField] private Button btnReset;

    [SerializeField] private TextMeshProUGUI txtCollectableCount;

    [SerializeField] private GameObject appleIconPrefab;
    [SerializeField] private Transform appleIconDesTr;

    public static UnityAction<float> onClickSpeedBtn;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ScoreManager.onUpdateCollectable += UpdateResourceBar;
        PlayerController.onChangePlayerSpeed += OnChangedPlayerSpeed;
        UpdateResourceBar(0);
    }

    private void Start()
    {
        btnSpeedUp.onClick.AddListener(SpeedUp);
        btnSpeedDown.onClick.AddListener(SpeedDown);
        btnReset.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }

    private void OnDestroy()
    {
        ScoreManager.onUpdateCollectable -= UpdateResourceBar;
        PlayerController.onChangePlayerSpeed -= OnChangedPlayerSpeed;
    }

    private void OnChangedPlayerSpeed(float value, Vector2 speedClamValue)
    {
        if (value >= speedClamValue.y)
            btnSpeedUp.transform.GetComponentInChildren<TextMeshProUGUI>().text = "MaxSpeed";
        else if (value <= speedClamValue.x)
            btnSpeedDown.transform.GetComponentInChildren<TextMeshProUGUI>().text = "MinSpeed";
        else
        {
            btnSpeedUp.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Speed++";
            btnSpeedDown.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Speed--";
        }
    }

    private void SpeedDown() => onClickSpeedBtn?.Invoke(-.5f);

    private void SpeedUp() => onClickSpeedBtn?.Invoke(+.5f);


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