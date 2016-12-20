using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{

    public GameObject tile;
    public GameObject heartTile;
    public GameObject spawnTile;
    public GameObject[] stones;
    public Vector2 mapSize;
    public Camera cam;
    enum State { NONE, WALL, ARCHER }
    State state = State.NONE;

    // Use this for initialization
    void Start()
    {
        Renderer r = tile.GetComponent<Renderer>();
        Vector2 offset = new Vector2(r.bounds.size.x * mapSize.x / 2, r.bounds.size.y * mapSize.y / 2);
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (x == mapSize.x - 1 && y == 0)
                {
                    Instantiate(heartTile, new Vector3(x * r.bounds.size.x + 0.1f * x - offset.x, 0, y * r.bounds.size.y + 0.1f * y - offset.y), Quaternion.identity);
                }
                else if (x == 0 && y == mapSize.y - 1)
                {
                    Instantiate(spawnTile, new Vector3(x * r.bounds.size.x + 0.1f * x - offset.x, 0, y * r.bounds.size.y + 0.1f * y - offset.y), Quaternion.identity);
                }
                else
                {
                    (Instantiate(tile, new Vector3(x * r.bounds.size.x + 0.1f * x - offset.x, 0, y * r.bounds.size.y + 0.1f * y - offset.y), Quaternion.identity) as GameObject).GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                }
            }
        }
    }

    public void BuildWall()
    {
        state = State.WALL;
    }

    public void BuildArcher()
    {
        state = State.ARCHER;
    }

    void Update()
    {
        if (state != State.NONE && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider == null)
            {
                return;
            }
            switch (state)
            {
                case State.WALL:
                    Instantiate(stones[Random.Range(0, stones.Length - 1)], hit.transform.position + new Vector3(0, hit.collider.bounds.extents.y, 0),Quaternion.Euler(0,Random.Range(-180f,180f),0));
                    break;
                case State.ARCHER:
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
