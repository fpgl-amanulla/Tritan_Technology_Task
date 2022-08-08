using UnityEngine.Events;

public class ScoreManager
{
    private static ScoreManager instance;
    public static ScoreManager Instance => instance ??= new ScoreManager();

    public static UnityAction<int> onUpdateCollectable;
    private int _collectableCount = 0;
    private int _valueToAdd;

    public int GetCollectableCount() => _collectableCount;

    public void AddCollectable(int value)
    {
        _valueToAdd = value;
        UIManager.Instance.ShowCollectUIAnimation(OnCompleteAnimation);
    }

    private void OnCompleteAnimation()
    {
        _collectableCount += _valueToAdd;
        onUpdateCollectable?.Invoke(_collectableCount);
    }
}