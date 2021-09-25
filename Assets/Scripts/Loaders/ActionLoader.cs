using UnityEngine;
using UnityEngine.UI;

public class ActionLoader : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private float lerpSpeed;

    [Range(0, 100)]
    private float fillAmount = 0;

    public float FillAmount { get => fillAmount; set => fillAmount = value; }

    void Update()
    {
        float amount = Map(FillAmount, 100, 1);
        // content.fillAmount = Mathf.Lerp(content.fillAmount, amount, Time.deltaTime * lerpSpeed);
        content.fillAmount = amount;
    }

    private float Map(float value, float max, float min)
    {
        return value * min / max;
    }
}
