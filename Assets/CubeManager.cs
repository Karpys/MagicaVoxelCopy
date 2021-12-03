using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> AllDirection;

    public LayerMask Layer;
    public GameObject Face;
    private static CubeManager inst;
    public static CubeManager Manager { get => inst; }
    void Awake()
    {
        if (Manager != null && Manager != this)
            Destroy(gameObject);
        AllDirection = new List<Vector3>(){
            new Vector3(1,0,0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1)
        };
        inst = this;
    }

    public int DirToPos(Vector3 Direction)
    {
        switch (Direction)
        {
            case Vector3 v when v.Equals(new Vector3(1,0,0)):
                return 0;

            case Vector3 v when v.Equals(new Vector3(-1, 0, 0)):
                return 1;

            case Vector3 v when v.Equals(new Vector3(0, 1, 0)):
                return 2;

            case Vector3 v when v.Equals(new Vector3(0, -1, 0)):
                return 3;

            case Vector3 v when v.Equals(new Vector3(0, 0, 1)):
                return 4;

            case Vector3 v when v.Equals(new Vector3(0, 0, -1)):
                return 5;

            default:
                return 0;
        }
    }
}
