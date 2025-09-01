using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentController : MonoBehaviour, IDropHandler
{
    [SerializeField] private EEquipmentType _equipmentType;
    
    public void OnDrop(PointerEventData eventData)
    {
        var droppedObj = eventData.pointerDrag;
        var equipment = droppedObj.GetComponent<Equipment>();
        
        if (_equipmentType == equipment.EquipmentType)
        {
            Debug.Log($"장비 타입 일치! {_equipmentType}");
            
            equipment.IsEquipped = true;
            equipment.transform.SetParent(transform, false);
            equipment.transform.localPosition = Vector3.zero;
            equipment.transform.localScale = Vector3.one;
            
            var player = FindObjectOfType<PlayerStat>();
            player.SetEquipment(equipment);
        }
        else
        {
            Debug.Log($"장비 타입 불일치! {_equipmentType}");
        }
    }
}
