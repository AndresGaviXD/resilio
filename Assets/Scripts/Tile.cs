using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [Header("Tile Sprites")]
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private List<Sprite> clickedTiles;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;

    [Header("GM set via code")]
    public BuscaGameManager gameManager;


    public SpriteRenderer spriteRenderer;
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if (active)
        {

            if (Input.GetMouseButtonDown(0))
            {
                ClickedTile();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                flagged = !flagged;
                if (flagged)
                {
                    spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    spriteRenderer.sprite = unclickedTile;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1))
            {
                gameManager.ExpandIfFlagged(this);
            }
        }

    }


    public void ClickedTile()
    {
        if (active & !flagged)
        {
            active = false;
            if (isMine)
            {
                spriteRenderer.sprite = mineHitTile;
                gameManager.GameOver();
            }
            else
            {
                spriteRenderer.sprite = clickedTiles[mineCount];
                if (mineCount == 0)
                {
                    gameManager.ClickNeighbours(this);
                }

                gameManager.CheckGameOver();
            }
        }
    }

    public void ShowGameOverState()
    {
        if (active)
        {
            active = false;
            if (isMine & !flagged)
            {
                spriteRenderer.sprite = mineTile;
            }
            else if (flagged & !isMine)
            {
                spriteRenderer.sprite = mineWrongTile;
            }

        }
    }



    public void SetFlaggedIfMine()
    {
        if (isMine)
        {
            flagged = true;
            spriteRenderer.sprite = flaggedTile;
        }
    }

}
