using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapControl : MonoBehaviour {
    public GameObject prefab; //used for blank tile object
    public GameObject go; // used in tandem with prefab to create collision tiles

    private int mapSize = 15;
    private double hRatio = .9;
    // Use this for initialization

    void Start () {
        // Generate the outer frame of the map
        go = new GameObject();
        createFrame();
        createStorage();
        createFurniture();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void createTile(int x, int y) {
        go = (GameObject)GameObject.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    // Create a lxw rectangular block (coords = lower left corner)
    void createBlock(int x, int y, int l, int w ) {
        for(int j = 0; j < w; ++j)
        {
            for(int i = 0; i < l; ++i)
            {
                createTile(x + i, y + j);
            }
        }
    }

    // Create one of 4 corner pieces using coords as pivot point
    void createCorner(int x, int y, string dir) {
        createTile(x, y);
        if(dir=="SW")
        {
            createTile(x + 1, y);
            createTile(x, y + 1);
        }
        else if(dir=="NW") {
            createTile(x + 1, y);
            createTile(x, y - 1);
        }
        else if(dir=="SE") {
            createTile(x - 1, y);
            createTile(x, y + 1);
        }
        else if(dir == "NE") {
            createTile(x - 1, y);
            createTile(x, y - 1);
        }
    }

    void createFrame()
    { 
        // Generate the outer frame of the map
        for (int i = 0; i <= mapSize + (int)(mapSize * hRatio); ++i)
        {
            // Generate top of stage
            createTile(i, mapSize - 1);
            createTile(-i, mapSize - 1);

            // Generate Bottom of stage
            createTile(i, -mapSize + 1);
            createTile(-i, -mapSize + 1);

            // Generate sides of stage
            if (i < mapSize)
            {
                createTile(-mapSize - (int)(mapSize * hRatio), i);
                createTile(mapSize + (int)(mapSize * hRatio), i);
                createTile(-mapSize - (int)(mapSize * hRatio), -i);
                createTile(mapSize + (int)(mapSize * hRatio), -i);
            }
        }
    }

    void createStorage()
    {
        // Generate corner pieces
        createCorner(mapSize + (int)(mapSize * hRatio) - 2, -mapSize + 3, "SE");
        createCorner(mapSize + (int)(mapSize * hRatio) - 2, mapSize - 3, "NE");
        createCorner(-mapSize - (int)(mapSize * hRatio) + 2, -mapSize + 3, "SW");
        createCorner(-mapSize - (int)(mapSize * hRatio) + 2, mapSize - 3, "NW");

        // Generate 2 x 2 blocks
        createBlock(mapSize + (int)(mapSize * hRatio) - 5, -mapSize + 5, 2, 2);
        createBlock(mapSize + (int)(mapSize * hRatio) - 5, mapSize - 6, 2, 2);
        createBlock(-mapSize - (int)(mapSize * hRatio) + 4, -mapSize + 5, 2, 2);
        createBlock(-mapSize - (int)(mapSize * hRatio) + 4, mapSize - 6, 2, 2);

        // Generate 5 x 5 and 4x4 blocks
        createBlock(-4, -mapSize + 1, 8, 3);
        createBlock(-4, mapSize - 3, 8, 3);
        createBlock(-mapSize - (int)(mapSize * hRatio) + 2, -2, 5, 5);
        createBlock(mapSize + (int)(mapSize * hRatio) - 6, -2, 5, 5);

        // Generate some Long blocks
        createBlock(-mapSize - (int)(mapSize * hRatio) + 2, -6, 7, 2);
        createBlock(-mapSize - (int)(mapSize * hRatio) + 2, 5, 7, 2);
        createBlock(mapSize + (int)(mapSize * hRatio) - 8, -6, 7, 2);
        createBlock(mapSize + (int)(mapSize * hRatio) - 8, 5, 7, 2);

        // Generate some L's
        createBlock(-mapSize - (int)(mapSize * hRatio) + 7, -mapSize + 3, 6, 2); // NW L Body
        createBlock(-mapSize - (int)(mapSize * hRatio) + 7, mapSize - 4, 6, 2);  // SW L Body
        createBlock(-mapSize - (int)(mapSize * hRatio) + 11, -mapSize + 3, 2, 4); // NW L Top
        createBlock(-mapSize - (int)(mapSize * hRatio) + 11, mapSize - 6, 2, 4); // SW L Top
        createBlock(mapSize + (int)(mapSize * hRatio) - 12, -mapSize + 3, 6, 2); // NE L Body
        createBlock(mapSize + (int)(mapSize * hRatio) - 12, mapSize - 4, 6, 2);  // SE L Body
        createBlock(mapSize + (int)(mapSize * hRatio) - 12, -mapSize + 3, 2, 4); // NE L Top
        createBlock(mapSize + (int)(mapSize * hRatio) - 12, mapSize - 6, 2, 4); // SE L Top

        // Generate four rows of pillars
        for (int i = 15; i < 20; i += 4)
        {
            for (int j = 2; j < 13; j += 5)
            {
                createBlock(-mapSize - (int)(mapSize * hRatio) + i, -mapSize + j, 2, 2);
                createBlock(-mapSize - (int)(mapSize * hRatio) + i, mapSize - (j + 1), 2, 2);
                createBlock(mapSize + (int)(mapSize * hRatio) - (i + 1), -mapSize + j, 2, 2);
                createBlock(mapSize + (int)(mapSize * hRatio) - (i + 1), mapSize - (j + 1), 2, 2);
            }
        }
    }

    void createFurniture() {
        // create furniture
        createBlock(-2, -4, 4, 8); // Create table
        createBlock(-4, -2, 1, 1); // create chair
        createBlock(-4, 0, 1, 1); // create chair
        createBlock(-4, 2, 1, 1); // create chair
        createBlock(3, -2, 1, 1); // create chair
        createBlock(3, 0, 1, 1); // create chair
        createBlock(3, 2, 1, 1); // create chair
        createBlock(-2, 10, 4, 1); // create couch
        createBlock(-2, -10, 4, 1); // create couch
    }
}
