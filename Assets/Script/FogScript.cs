using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogScript : MonoBehaviour
{

    public Tilemap fogOfWar;

    public Tile fogTile;
    public int appearRange = 10;
    public int disappearRange = 4;

    private List<Vector2Int> visited = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a range around the player that create the fog of war
        Vector3Int pos = fogOfWar.WorldToCell(transform.position);
        for(int x = -appearRange; x <= appearRange; x++){
            for(int y = -appearRange; y <= appearRange; y++){

                if(appearRange <= disappearRange){
                    continue;
                }


                Vector3Int newPos = new Vector3Int(pos.x + x, pos.y + y, pos.z);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(newPos.x, newPos.y), 0.5f);
                foreach(Collider2D collider in colliders){
                    if(collider.gameObject.layer == 7){
                        Color col = collider.gameObject.GetComponent<SpriteRenderer>().color;
                        col.a = 0.0f;
                        collider.gameObject.GetComponent<SpriteRenderer>().color = col;
                    }
                }


                if(visited.Contains(new Vector2Int(newPos.x, newPos.y))){
                    Tile tile = new Tile();
                    tile.sprite = fogTile.sprite;
                    tile.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    fogOfWar.SetTile(newPos, tile);
                }
                if(fogOfWar.GetTile(newPos) == null){
                    fogOfWar.SetTile(newPos, fogTile);
                }

            }
        }

        // Create a range around the player that destroy the fog of war
        for(int x = -disappearRange; x <= disappearRange; x++){
            for(int y = -disappearRange; y <= disappearRange; y++){

                Vector3Int newPos = new Vector3Int(pos.x + x, pos.y + y, pos.z);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(newPos.x, newPos.y), 0.5f);
                foreach(Collider2D collider in colliders){
                    if(collider.gameObject.layer == 7){
                        Color col = collider.gameObject.GetComponent<SpriteRenderer>().color;
                        col.a = 1.0f;
                        collider.gameObject.GetComponent<SpriteRenderer>().color = col;
                    }
                }

                if(fogOfWar.GetTile(newPos) != null){
                    if(!visited.Contains(new Vector2Int(newPos.x, newPos.y))){
                        visited.Add(new Vector2Int(newPos.x, newPos.y));
                    }
                    fogOfWar.SetTile(newPos, null);
                }
            }
        }


    }


}
