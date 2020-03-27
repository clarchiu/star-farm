using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    ObjectTile NeighbourUp, NeighbourDown, NeighbourLeft, NeighbourRight;

    private void Start()
    {
        UpdateSprite(true);
    }

    void OnDestroy()
    {
        UpdateSprite(false);
    }

    public void UpdateSprite(bool updateSelf)
    {
        try
        {
            NeighbourUp = TileLayout.Instance.GetTile(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + 1));
            NeighbourDown = TileLayout.Instance.GetTile(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1));
            NeighbourLeft = TileLayout.Instance.GetTile(Mathf.RoundToInt(transform.position.x - 1), Mathf.RoundToInt(transform.position.y));
            NeighbourRight = TileLayout.Instance.GetTile(Mathf.RoundToInt(transform.position.x + 1), Mathf.RoundToInt(transform.position.y));
        } catch (System.Exception e)
        {
            return;
        }

        Sprite startingSprite = GetComponent<SpriteRenderer>().sprite;

        if (updateSelf)
        {
            if (MatchNeighbour(false, false, false, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[10]; } //Replace with a different block when drawn
            else if (MatchNeighbour(false, false, false, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[9]; }
            else if (MatchNeighbour(false, false, true, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[8]; }
            else if (MatchNeighbour(false, false, true, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[7]; }
            else if (MatchNeighbour(false, true, false, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[1]; }
            else if (MatchNeighbour(false, true, false, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[6]; }
            else if (MatchNeighbour(false, true, true, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[5]; }
            else if (MatchNeighbour(false, true, true, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[7]; }
            else if (MatchNeighbour(true, false, false, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[0]; }
            else if (MatchNeighbour(true, false, false, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[3]; }
            else if (MatchNeighbour(true, false, true, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[4]; }
            else if (MatchNeighbour(true, false, true, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[7]; }
            else if (MatchNeighbour(true, true, false, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[2]; }
            else if (MatchNeighbour(true, true, false, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[2]; }
            else if (MatchNeighbour(true, true, true, false)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[2]; }
            else if (MatchNeighbour(true, true, true, true)) { GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.wallSprites[7]; } //Replace later

            GetComponent<DropShadow>().UpdateShadow();
        }
        if (GetComponent<SpriteRenderer>().sprite != startingSprite || !updateSelf)
        {
            if (NeighbourUp.getObjectOnTile()    != null && NeighbourUp.getObjectOnTile().tag.Equals("Wall"))    { NeighbourUp.getObjectOnTile().GetComponent<Wall>().UpdateSprite(true); }
            if (NeighbourDown.getObjectOnTile()  != null && NeighbourDown.getObjectOnTile().tag.Equals("Wall"))  { NeighbourDown.getObjectOnTile().GetComponent<Wall>().UpdateSprite(true); }
            if (NeighbourLeft.getObjectOnTile()  != null && NeighbourLeft.getObjectOnTile().tag.Equals("Wall"))  { NeighbourLeft.getObjectOnTile().GetComponent<Wall>().UpdateSprite(true); }
            if (NeighbourRight.getObjectOnTile() != null && NeighbourRight.getObjectOnTile().tag.Equals("Wall")) { NeighbourRight.getObjectOnTile().GetComponent<Wall>().UpdateSprite(true); }
        } 
    }

    bool MatchNeighbour(bool up, bool down, bool left, bool right)
    {
        if (up    != (NeighbourUp.getObjectOnTile()    != null && NeighbourUp.getObjectOnTile().tag == "Wall")) { return false; }
        if (down  != (NeighbourDown.getObjectOnTile()  != null && NeighbourDown.getObjectOnTile().tag == "Wall")) { return false; }
        if (left  != (NeighbourLeft.getObjectOnTile()  != null && NeighbourLeft.getObjectOnTile().tag == "Wall")) { return false; }
        if (right != (NeighbourRight.getObjectOnTile() != null && NeighbourRight.getObjectOnTile().tag == "Wall")) { return false; }
        return true;
    }
    
}
