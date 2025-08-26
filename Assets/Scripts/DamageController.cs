using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    
    private Queue<TMP_Text> _textPool = new Queue<TMP_Text>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            _textPool.Enqueue(InstantiateText());
        }

        CombatManager.Instance.OnDamaged += OnDamaged;
    }

    private TMP_Text InstantiateText()
    {
        var text = Instantiate<TMP_Text>(_damageText, transform);
        text.gameObject.SetActive(false);
        return text;
    }

    private void OnDamaged(Transform damagedTransform, float value)
    {
        TMP_Text text;
        if (_textPool.Count == 0)
        {
            text = InstantiateText();
        }
        else
        {
            text = _textPool.Dequeue();
        }

        text.text = $"{value}";
        
        text.transform.position = damagedTransform.position + Vector3.up * 5;
        text.DOFade(1f, 0f);
        text.gameObject.SetActive(true);
        
        var anim = DOTween.Sequence();
        anim.Append(text.transform.DOMoveY(10f, 1.5f).SetRelative(true));
        anim.Join(text.DOFade(0f, 1.5f));
        anim.OnComplete(() =>
        {
            text.gameObject.SetActive(false);
            _textPool.Enqueue(text);
        });

        anim.Play();
    }
}
