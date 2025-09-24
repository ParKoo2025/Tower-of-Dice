using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EquipmentChanger : MonoBehaviour
{
    [SerializeField] private GameObject weaponObject;
    private SpriteRenderer weaponRenderer;
    [SerializeField] private GameObject helmetObject;
    private SpriteRenderer helmetRenderer;
    [SerializeField] private GameObject bodyObject;
    [SerializeField] private GameObject L_armObject;
    [SerializeField] private GameObject R_armObject;
    [SerializeField] private GameObject L_shoulderObject;
    [SerializeField] private GameObject R_shoulderObject;
    private SpriteRenderer bodyRenderer;
    private SpriteRenderer L_armRenderer;
    private SpriteRenderer R_armRenderer;
    private SpriteRenderer L_shoulderRenderer;
    private SpriteRenderer R_shoulderRenderer;
    [SerializeField] private GameObject L_footObject;
    [SerializeField] private GameObject R_footObject;
    private SpriteRenderer L_footRenderer;
    private SpriteRenderer R_footRenderer;



    void Awake()
    {
        weaponRenderer = weaponObject.GetComponent<SpriteRenderer>();
        helmetRenderer = helmetObject.GetComponent<SpriteRenderer>();
        bodyRenderer = bodyObject.GetComponent<SpriteRenderer>();
        L_armRenderer = L_armObject.GetComponent<SpriteRenderer>();
        R_armRenderer = R_armObject.GetComponent<SpriteRenderer>();
        L_shoulderRenderer = L_shoulderObject.GetComponent<SpriteRenderer>();
        R_shoulderRenderer = R_shoulderObject.GetComponent<SpriteRenderer>();
        L_footRenderer = L_footObject.GetComponent<SpriteRenderer>();
        R_footRenderer = R_footObject.GetComponent<SpriteRenderer>();
    }
    
    public void EquipSprite(Sprite sprite, EEquipmentType equipmentType)
    {
        if (equipmentType == EEquipmentType.Weapon)
        {
            weaponRenderer.sprite = sprite;
        }
        else if (equipmentType == EEquipmentType.Helmet)
        {
            helmetRenderer.sprite = sprite;
        }
        else if (equipmentType == EEquipmentType.Armor)
        {
            bodyRenderer.sprite = sprite;
        }
        else
        {
            L_footRenderer.sprite = sprite;
            R_footRenderer.sprite = sprite;
        }
    }
}
