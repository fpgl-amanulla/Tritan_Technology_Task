using UnityEngine;

public interface ICollectable
{
    public void Collect();
    public void OnSelect();
    public void OnDeSelect();
}

public class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject targetCircle;

    public void Collect()
    {
        ScoreManager.Instance.AddCollectable(1);
        Destroy(this.gameObject);
    }

    public void OnSelect()
    {
        if (targetCircle) targetCircle.SetActive(true);
    }

    public void OnDeSelect()
    {
        if (targetCircle) targetCircle.SetActive(false);
    }
}