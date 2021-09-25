using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private float lerpSpeed;

    [Range(0, 100)]
    private float health = 100;

    public float Health { get => health; set => health = value; }
    
    void Update()
    {
        float amount = Map(Health, 100, 1);
        content.fillAmount = Mathf.Lerp(content.fillAmount, amount, Time.deltaTime * lerpSpeed);
    }

    private float Map(float value, float max, float min)
    {
        return value * min / max;
    }
}
