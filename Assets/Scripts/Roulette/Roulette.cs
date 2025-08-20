using UnityEngine;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private RoulettePieceData[] roulettePieceData;

    private void Awake()
    {
        SpawnPieces();
    }

    private void SpawnPieces()
    {
        
    }
}


