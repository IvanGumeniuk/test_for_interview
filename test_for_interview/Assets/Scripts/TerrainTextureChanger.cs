using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTextureChanger : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private RoofsChanger roofsChanger;

    private TreeInstance[] m_CurrentTreeList;
    private TerrainData m_TerrainData;
    
    private void Awake()
    {
        m_TerrainData = terrain.terrainData;
        m_CurrentTreeList = new TreeInstance[m_TerrainData.treeInstanceCount];
        System.Array.Copy(m_TerrainData.treeInstances, m_CurrentTreeList, m_TerrainData.treeInstances.Length);
    }

    public void changeToWinter()
    {
        UpdateTerrainTexture(terrain.terrainData, 0, 1);
        ChangeSeasons(1, 3);
        roofsChanger.changeRoof(true);
    }

    public void changeToNotWinter()
    {
        UpdateTerrainTexture(terrain.terrainData, 1, 0);
        ChangeSeasons(0, 2);
        roofsChanger.changeRoof(false);
    }
    
   /* void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTerrainTexture(terrain.terrainData, 0, 1);
            ChangeSeasons(1, 3);
            roofsChanger.changeRoof(true);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {       
            UpdateTerrainTexture(terrain.terrainData, 1, 0);
            ChangeSeasons(0,2);
            roofsChanger.changeRoof(false);
        }
    }*/

    void ChangeSeasons(int tree_1_NumberTo, int tree_2_NumberTo)
    {
        if(m_TerrainData.treeInstances.Length == m_CurrentTreeList.Length)
        {
            for(int tcnt = 0; tcnt < m_CurrentTreeList.Length; tcnt++)
            {
                m_CurrentTreeList[tcnt].prototypeIndex = tcnt % 2 == 0 ? tree_1_NumberTo : tree_2_NumberTo;
            }
            m_TerrainData.treeInstances = m_CurrentTreeList;
        }
    }

    static void UpdateTerrainTexture(TerrainData terrainData, int textureNumberFrom, int textureNumberTo)
    {
        //get current paint mask
        float[,,] alphas = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        // make sure every grid on the terrain is modified
        for(int i = 0; i < terrainData.alphamapWidth; i++)
        {
            for(int j = 0; j < terrainData.alphamapHeight; j++)
            {
                //for each point of mask do:
                //paint all from old texture to new texture (saving already painted in new texture)
                alphas[i, j, textureNumberTo] = Mathf.Max(alphas[i, j, textureNumberFrom], alphas[i, j, textureNumberTo]);
                //set old texture mask to zero
                alphas[i, j, textureNumberFrom] = 0f;
            }
        }
        // apply the new alpha
        terrainData.SetAlphamaps(0, 0, alphas);
    }

}

/*
TreeInstance[] currentTreeList;
TerrainData terrain;
int springTreeIndex;
int fallTreeIndex;
bool season;

void ChangeSeasons ()
{
    // The currentTreeList array must be at least terrain.treeInstances.Length in size
    System.Array.Copy (terrain.treeInstances, currentTreeList, terrain.treeInstances.Length);
    if (terrain.treeInstances.Length == currentTreeList.Length)
    {
        for (int tcnt=0; tcnt < currentTreeList.Length; tcnt++)
        {
            if (season)
            {
                if (currentTreeList [tcnt].prototypeIndex == springTreeIndex)
                    currentTreeList [tcnt].prototypeIndex = fallTreeIndex;
                }
                else
                {
                if (currentTreeList [tcnt].prototypeIndex == fallTreeIndex)
                    currentTreeList [tcnt].prototypeIndex = springTreeIndex;
            }
        }
        terrain.treeInstances = currentTreeList;
        season = !season;
    }
}
 
 */
