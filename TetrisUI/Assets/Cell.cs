using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    private GameObject explosionPrefab;
    private GameObject explosion;
    
    private Color color = new Color(0, 0, 0);

    public void Explode()
    {
        if (explosion != null)
        {
            Destroy(explosion);
        }

        explosion = GameObject.Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.GetComponent<ParticleSystem>().startColor = color;
    }
    
    public void SetKind(int kind)
    {
        switch (kind)
        {
            case 1:
                color = new Color(1, 0, 0);
                break;
            case 2:
                color = new Color(1, 1, 0);
                break;
            case 3:
                color = new Color(1, 0, 1);
                break;
            case 4:
                color = new Color(0, 1, 0);
                break;
            case 5:
                color = new Color(0, 0, 1);
                break;
            case 6:
                color = new Color(0, 1, 1);
                break;
            case 7:
                color = new Color(1, 1, 1);
                break;
        }

        GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color);
    }


    // Use this for initialization
    void Awake()
    {
        explosionPrefab = Resources.Load("Explosion") as GameObject;

        GetComponent<Renderer>().sharedMaterial = new Material(GetComponent<Renderer>().sharedMaterial);
    }
}
