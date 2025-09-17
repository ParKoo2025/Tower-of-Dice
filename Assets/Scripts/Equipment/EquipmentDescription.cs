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
        
        var equipmentPosition = equipment.transform.localPosition;

        if (equipmentPosition.x > 0)
        {
            transform.localPosition = new Vector3(equipmentPosition.x - 239, equipmentPosition.y - 68, equipmentPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(equipmentPosition.x + 239, equipmentPosition.y - 68, equipmentPosition.z);

        }
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
