/**
 *
 *                             GNAT EXAMPLE
 *
 *                      Copyright (C) 2016, AdaCore
 *
 * GNAT is free software;  you can  redistribute it  and/or modify it under
 * terms of the  GNU General Public License as published  by the Free Soft-
 * ware  Foundation;  either version 3,  or (at your option) any later ver-
 * sion.  GNAT is distributed in the hope that it will be useful, but WITH-
 * OUT ANY WARRANTY;  without even the  implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 *
 * As a special exception under Section 7 of GPL version 3, you are granted
 * additional permissions described in the GCC Runtime Library Exception,
 * version 3.1, as published by the Free Software Foundation.
 *
 * You should have received a copy of the GNU General Public License and
 * a copy of the GCC Runtime Library Exception along with this program;
 * see the files COPYING3 and COPYING.RUNTIME respectively.  If not, see
 * <http://www.gnu.org/licenses/>.
 *
 * GNAT was originally developed  by the GNAT team at  New York University.
 * Extensive contributions were provided by Ada Core Technologies Inc.
 *
 */

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
