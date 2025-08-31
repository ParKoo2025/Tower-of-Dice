using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Mime;
using Mono.Cecil;
using UnityEngine;


public class EquipmentFactory : MonoBehaviour
{
    [SerializeField] Equipment _baseEquipment;
    private Dictionary<ERarity, Sprite> _raritySprites;
    private Dictionary<EEquipments, Sprite> _equipmentSprites;
    
    public Equipment GetEquipment(ERarity rarity, EEquipments equipments)
    {
        var equipment = Instantiate(_baseEquipment);
        equipment.RaritySprite = _raritySprites[rarity];
        equipment.EquipmentSprite = _equipmentSprites[equipments];
        
        return equipment;
    }

    private void Awake()
    {
        _raritySprites = new Dictionary<ERarity, Sprite>();
        for (int i = 0; i < (int)ERarity.Size; ++i)
        {
            var sprite = Resources.Load<Sprite>($"PNG/Item/Rarity/{((ERarity)i).ToString()}");
            _raritySprites[(ERarity)i] = sprite;
        }
        _equipmentSprites = new Dictionary<EEquipments, Sprite>();
        for (int i = 0; i < (int)EEquipments.Size; ++i)
        {
            var sprite = Resources.Load<Sprite>($"PNG/Item/Equipments/{((EEquipments)i).ToString()}");
            _equipmentSprites[(EEquipments)i] = sprite;
        }
    }
}