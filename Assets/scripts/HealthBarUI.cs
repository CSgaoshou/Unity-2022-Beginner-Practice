using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private CharacterStats mystats;
    private RectTransform myTransform;
    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        mystats = GetComponentInParent<CharacterStats>();

        entity.onFlipped += FlipUI;
        mystats.onHealthChanged += UpdateHealthUI;

        StartCoroutine(DelayedHealthUpdate());
    }

    private IEnumerator DelayedHealthUpdate()
    {
        yield return null;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = mystats.maxHealth.GetValue();
        slider.value = mystats.CurrentHealth;
    }



    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        mystats.onHealthChanged -= UpdateHealthUI;
    }

}
