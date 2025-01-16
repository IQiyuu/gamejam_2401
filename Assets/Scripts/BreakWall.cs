using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakWall :MonoBehaviour
{
    private PlayerMovement mov;
    public LayerMask BreakWallLayer;
    public Tilemap tilemap;
    public Vector2 detectionSize = new Vector2(1f, 1f);
    void Start()
    {
        mov = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, detectionSize, 0, BreakWallLayer);
        if (hit != null && mov.IsRolling)
        {
            Vector3Int tilePosition = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(tilePosition, null);
            tilePosition = tilemap.WorldToCell(transform.position + new Vector3(1, 0, 0));
            tilemap.SetTile(tilePosition, null);
            tilePosition = tilemap.WorldToCell(transform.position + new Vector3(-1, 0, 0));
            tilemap.SetTile(tilePosition, null);
            tilePosition = tilemap.WorldToCell(transform.position + new Vector3(1, 1, 0));
            tilemap.SetTile(tilePosition, null);
            tilePosition = tilemap.WorldToCell(transform.position + new Vector3(-1, 1, 0));
            tilemap.SetTile(tilePosition, null);
            tilePosition = tilemap.WorldToCell(transform.position + new Vector3(0, 1, 0));
            tilemap.SetTile(tilePosition, null);
            
        }
    }
}