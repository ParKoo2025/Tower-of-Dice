using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDescription : MonoBehaviour
{
    [SerializeField]
    private Image equipmentIcon;
    [SerializeField]
    private Image rarityBackground1;
    [SerializeField]
    private Image rarityBackground2;
    [SerializeField]
    private Image subStat1Image;
    [SerializeField]
    private Image subStat2Image;
    [SerializeField]
    private Image subStat3Image;
    
    [SerializeField]
    private TextMeshProUGUI rarity;
    [SerializeField]
    private TextMeshProUGUI equipmentName;
    [SerializeField]
    private TextMeshProUGUI mainStat;
    [SerializeField]
    private TextMeshProUGUI mainStatValue;
    [SerializeField]
    private TextMeshProUGUI subStat1;
    [SerializeField]
    private TextMeshProUGUI subStat1Value;
    [SerializeField]
    private TextMeshProUGUI subStat2;
    [SerializeField]
    private TextMeshProUGUI subStat2Value;
    [SerializeField]
    private TextMeshProUGUI subStat3;
    [SerializeField]
    private TextMeshProUGUI subStat3Value;
    
    private List<(TextMeshProUGUI, TextMeshProUGUI)> TMPset;
    
    public void ShowEquipmentDescription(Equipment equipment, Vector3 screenPos)
    {
        TMPset = new List<(TextMeshProUGUI, TextMeshProUGUI)>
        {
            (mainStat, mainStatValue),
            (subStat1, subStat1Value),
            (subStat2, subStat2Value),
            (subStat3, subStat3Value)
        };

        equipmentName.text = equipment.EquipmentName.ToString();
        
        equipmentIcon.sprite = equipment.EquipmentSprite;
        rarity.text = equipment.Rarity.ToString();
        SetRarity(equipment.Rarity);
        List<(EStatType, float)> statList = equipment.GetStatList();
        for (int i = 0; i < statList.Count; i++)
        {
            TMPset[i].Item1.text = statList[i].Item1.ToString();
            TMPset[i].Item2.text = statList[i].Item2.ToString();
        }
        Debug.Log(statList.Count);

        for (int i = statList.Count; i < 4; i++)
        {
            TMPset[i].Item1.text = "";
            TMPset[i].Item2.text = "";
        }
        
        // ----- (2) 위치 계산 추가) -----
        var canvas = GetComponentInParent<Canvas>();
        var panel = transform as RectTransform;
        var canvasRect = canvas.transform as RectTransform;

        // 툴팁 크기 최신화 (ContentSizeFitter/AutoLayout 쓰면 강추)
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel);

        // 화면 중앙 기준으로 좌/우 판단
        bool onRightHalf = screenPos.x > Screen.width * 0.5f;

        // 툴팁 피벗을 방향에 맞게 설정 (왼쪽에 띄울 땐 피벗=1,오른쪽에 띄울 땐 피벗=0)
        // → 피벗을 바꾸면 앵커 기준 배치가 직관적
        var pivot = panel.pivot;
        pivot.x = onRightHalf ? 1f : 0f;
        panel.pivot = pivot;

        // 캔버스 종류별 카메라 처리
        Camera uiCam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
            uiCam = canvas.worldCamera;

        // 스크린 → 캔버스 로컬 좌표
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, screenPos, uiCam, out Vector2 localPos);

        // 아이콘과의 간격(여백). 피벗 기준이므로 절반 + 여백 정도 추천
        float gap = 0f;
        float halfW = panel.rect.width * 0.5f;

        // 방향에 따른 오프셋 적용
        if (onRightHalf)
            localPos.x -= (halfW + gap);   // 오른쪽에 있으면 툴팁을 왼쪽에
        else
            localPos.x += (halfW + gap);   // 왼쪽에 있으면 툴팁을 오른쪽에

        // 세로 방향 살짝 띄우기(선택)
        localPos.y += panel.rect.height * 0.1f;

        // 화면(캔버스) 경계 안으로 클램프
        Vector2 minBounds = canvasRect.rect.min + new Vector2(panel.rect.width * 0.5f, panel.rect.height * 0.5f);
        Vector2 maxBounds = canvasRect.rect.max - new Vector2(panel.rect.width * 0.5f, panel.rect.height * 0.5f);
        localPos.x = Mathf.Clamp(localPos.x, minBounds.x, maxBounds.x);
        localPos.y = Mathf.Clamp(localPos.y, minBounds.y, maxBounds.y);

        panel.anchoredPosition = localPos;
    }

    private void SetRarity(ERarity rarity)
    {
        Image[] rarityBackgrounds = {rarityBackground1, rarityBackground2};
        Color targetColor = Color.white;
        
        switch (rarity)
        {
            case ERarity.Common:
                targetColor = new Color(242f/255f, 242f/255f, 218f/255f, 1f);
                subStat1Image.gameObject.SetActive(true);
                subStat2Image.gameObject.SetActive(true);
                subStat3Image.gameObject.SetActive(true);
                break;
            case ERarity.Uncommon:
                targetColor = new Color(124f/255f, 207f/255f, 154f/255f, 1f);
                subStat2Image.gameObject.SetActive(true);
                subStat3Image.gameObject.SetActive(true);
                subStat1Image.gameObject.SetActive(false);
                break;
            case ERarity.Rare:
                targetColor = new Color(73f/255f, 194f/255f, 242f/255f, 1f);
                subStat3Image.gameObject.SetActive(true);
                subStat1Image.gameObject.SetActive(false);
                subStat2Image.gameObject.SetActive(false);
                break;
            case ERarity.Epic:
                targetColor = new Color(226f/255f, 155f/255f, 250f/255f, 1f);
                subStat1Image.gameObject.SetActive(false);
                subStat2Image.gameObject.SetActive(false);
                subStat3Image.gameObject.SetActive(false);
                break;
            case ERarity.Legendary:
                targetColor = new Color(250f/255f, 217f/255f, 55f/255f, 1f);
                subStat1Image.gameObject.SetActive(false);
                subStat2Image.gameObject.SetActive(false);
                subStat3Image.gameObject.SetActive(false);
                break;
            case ERarity.Mythic:
                targetColor = new Color(255f/255f, 112f/255f, 112f/255f, 1f);
                subStat1Image.gameObject.SetActive(false);
                subStat2Image.gameObject.SetActive(false);
                subStat3Image.gameObject.SetActive(false);
                break;
        }
        
        foreach (var bg in rarityBackgrounds)
        {
            bg.color = targetColor;
        }
    }
}
