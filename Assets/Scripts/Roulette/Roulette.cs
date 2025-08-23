using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UIElements;

public class Roulette : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private Transform pieceParent;

    [Header("Data")]
    [SerializeField] private RoulettePieceData[] roulettePieceData;

    [Header("Config")]
    [SerializeField] private float radius = 125f;
    
    [Header("Piece Config")]
    [SerializeField] private Vector3 pieceScale = Vector3.one; 
    
    [Header("Spin")]
    [SerializeField] private float halfPieceAngleWithPaddings = 22f;
    [SerializeField] private int spinDuration;
    [SerializeField] private Transform spinningRoulette;
    [SerializeField] private Ease easeType;

    private bool isSpinning = false;
    private int selectedIndex = 0;
    private List<RoulettePieceData> selected;
    
    private void Awake()
    {
        SpawnPieces();
    }

    private void SpawnPieces()
    {
        selected = DrawPiecesWeighted(roulettePieceData);

        for (int i = 0; i < selected.Count; ++i)
        {
            Transform t = Instantiate(piecePrefab, pieceParent);
            var piece = t.GetComponent<RoulettePiece>();
            piece.Setup(selected[i]);

            var rt = t as RectTransform;
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            
            t.localScale = pieceScale;

            float angCW = -22.5f + (-45f * i);
        
            float theta = (90f - angCW) * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * radius;
            rt.anchoredPosition = pos;
            
            float rotationOffset = 0f; 
            rt.localRotation = Quaternion.Euler(0f, 0f, -angCW + rotationOffset);
        }
    }
    private List<RoulettePieceData> DrawPiecesWeighted(RoulettePieceData[] source)
    {
        List<RoulettePieceData> result = new List<RoulettePieceData>();
        List<RoulettePieceData> pool = new List<RoulettePieceData>();
        foreach (var d in source) if (d != null && d.chance > 0) pool.Add(d);
        if (pool.Count == 0) return result;

        for (int k = 0; k < 8; ++k)
        {
            int total = 0;
            for (int i = 0; i < pool.Count; ++i) total += pool[i].chance;

            int r = Random.Range(0, total);
            int cum = 0;
            for (int i = 0; i < pool.Count; ++i)
            {
                cum += pool[i].chance;
                if (r < cum)
                {
                    result.Add(pool[i]);
                    break;
                }
            }
        }
        return result;
    }

    public void Spin(UnityAction<RoulettePieceData> onComplete = null)
    {
        if (isSpinning) return;
        
        selectedIndex = Random.Range(0, 8);
        
        float angle = -22.5f - (45f * selectedIndex);
        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360f;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360f;
        float randomAngle = Random.Range(leftOffset, rightOffset);

        int rotateSpeed = 2;
        float targetAngle = randomAngle + 360 * spinDuration * rotateSpeed;
        
        Debug.Log($"SelectedIndex : {selectedIndex}, Angle : {angle}");
        Debug.Log($"left/right/random : {leftOffset}/{rightOffset}/{randomAngle}");
        
        isSpinning = true;
        spinningRoulette
            .DOLocalRotate(new Vector3(0, 0, targetAngle), spinDuration, RotateMode.FastBeyond360)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                isSpinning = false;
                onComplete?.Invoke(selected[selectedIndex]);
            });
    }
}
