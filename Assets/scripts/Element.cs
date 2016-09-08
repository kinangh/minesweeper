using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Element : MonoBehaviour {

    public bool mine;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    void Start()
    {
        mine = Random.value < 0.15;

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        grid.elements[x, y] = this;
    }

    public void loadTexture(int adjacentCount)
    {
        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }   
    }

    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    void OnMouseUpAsButton()
    {
        if (mine)
        {
            grid.uncoverMines();

            Application.LoadLevel(1);
        }
        else
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(grid.adjacentMines(x, y));

            grid.FFuncover(x, y, new bool[grid.w, grid.h]);

            if (grid.isFinished())
            {
                print("you win");
            }
        }
    }
}