using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private Roulette roulette;
    [SerializeField] private Button buttonSpin;

    private void Awake()
    {
        buttonSpin.onClick.AddListener(() =>
        {
            buttonSpin.interactable = false;
            roulette.Spin(EndofSpin);
        });
    }

    private void EndofSpin(RoulettePieceData data)
    {
        buttonSpin.interactable = true;
        
        Debug.Log($"WIN: desc={data.description}, chance={data.chance}");
    }
}
