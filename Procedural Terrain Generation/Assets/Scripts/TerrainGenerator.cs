using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int depth = 20;
    public int height = 256;
    public int scale = 20;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public float offsetSpeed = 5f;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * offsetSpeed;
    }

    TerrainData GenerateTerrain(TerrainData terraindata)
    {
        terraindata.heightmapResolution = width + 1;
        terraindata.size = new Vector3(width, depth, height);
        terraindata.SetHeights(0, 0, GenerateHeights());

        return terraindata;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for(int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
