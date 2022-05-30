using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    Enemy target;
    [SerializeField] Image healthImage;

    private void OnDestroy() {
        target.OnHealthChanged -= UpdateHealthBar;
    }



    private void Start()
    {
        transform.SetParent(GameManager.Instance.EnemyHealthBarParent);
        gameObject.SetActive(false);
    }

    void UpdateHealthBar()
    {
        if (!gameObject.activeSelf) 
            gameObject.SetActive(true);

        if (target)
            healthImage.fillAmount = target.Health / target.MaxHealth;
    }

    public void SetTarget(Enemy e)
    {
        target = e;
        e.OnHealthChanged += UpdateHealthBar;
    }
}
