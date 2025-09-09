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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowEquipmentDescription(Equipment equipment, Vector3 position)
    {
        equipmentIcon.sprite = equipment.EquipmentSprite;
        rarity.text = equipment.Rarity.ToString();
        SetRarity(equipment.Rarity);
        
        
    }

    private void SetRarity(ERarity rarity)
    {
        Image[] rarityBackgrounds = {rarityBackground1, rarityBackground2};
        Color targetColor = Color.white;
        
        switch (rarity)
        {
            case ERarity.Common:
                targetColor = new Color(242f/255f, 242f/255f, 218f/255f, 1f);
                subStat1Image.enabled = true;
                subStat2Image.enabled = true;
                subStat3Image.enabled = true;
                break;
            case ERarity.Uncommon:
                targetColor = new Color(124f/255f, 207f/255f, 154f/255f, 1f);
                subStat2Image.enabled = true;
                subStat3Image.enabled = true;
                break;
            case ERarity.Rare:
                targetColor = new Color(73f/255f, 194f/255f, 242f/255f, 1f);
                subStat3Image.enabled = true;
                break;
            case ERarity.Epic:
                targetColor = new Color(226f/255f, 155f/255f, 250f/255f, 1f);
                break;
            case ERarity.Legendary:
                targetColor = new Color(250f/255f, 217f/255f, 55f/255f, 1f);
                break;
            case ERarity.Mythic:
                targetColor = new Color(255f/255f, 112f/255f, 112f/255f, 1f);
                break;
        }
        
        foreach (var bg in rarityBackgrounds)
        {
            bg.color = targetColor;
        }
    }
}
