using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cristalVaporText;
    [SerializeField] Slider cristalVaporSlider;
    [SerializeField] TextMeshProUGUI fragVaporText;
    [SerializeField] Slider hpBar;


    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void SetCristalVapor(int count)
    {
        cristalVaporText.text = count.ToString();
        cristalVaporSlider.value = count;
    }
    public void SetFragVapor(int count)
    {
        fragVaporText.text = count.ToString();
    }
    public void SetHpBar(float onePercent)
    {
        hpBar.value = onePercent;
    }
}
