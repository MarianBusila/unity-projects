using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject prefab;
    GameObject prefabInstance;

	// Use this for initialization
	void Start () {
        prefabInstance = Instantiate(prefab);
        RemoveScriptsFromPrefab();
        AdjustPrefAlpha();
        prefabInstance.SetActive(false);
	}

    void RemoveScriptsFromPrefab()
    {
        Component[] components = prefabInstance.GetComponentsInChildren<TurretTargettingSystem>();
        foreach (Component component in components)
            Destroy(component);
    }

    void AdjustPrefAlpha()
    {
        MeshRenderer[] meshRenderers = prefabInstance.GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < meshRenderers.Length; i++)
        {
            Material mat = meshRenderers[i].material;
            meshRenderers[i].material.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 50f);
        if(hits != null && hits.Length > 0)
        {
            int terrainColliderQuadIndex = GetTerrainColliderQuadIndex(hits);
            if (terrainColliderQuadIndex != -1)
            {
                prefabInstance.transform.position = hits[terrainColliderQuadIndex].point;
                prefabInstance.SetActive(true);
            }
            else
                prefabInstance.SetActive(false);
        }
    }

    int GetTerrainColliderQuadIndex(RaycastHit[] hits)
    {
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.name.Equals("TerrainColliderQuad"))
                return i;
        }
        return -1;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (prefabInstance.activeSelf)
            Instantiate(prefab, prefabInstance.transform.position, Quaternion.identity);

        prefabInstance.SetActive(false);
    }


}
