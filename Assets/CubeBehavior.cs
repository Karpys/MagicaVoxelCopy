using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] Faces = new GameObject[6]; 
    void Start()
    {
        ReGenerate();
    }

    public void ReGenerate()
    {
        CalculateVoisin();
    }

    public void Reset()
    {
        DestroyAllFace();
        ReGenerate();
    }

    // Update is called once per frame
    public void DestroyAllFace()
    {
        foreach (GameObject face in Faces)
        {
            if(face)
                Destroy(face);
        }
    }

    public void DestroyFace(Vector3 Direction)
    {
        int valueDir = CubeManager.Manager.DirToPos(Direction);
        if (valueDir % 2 == 0)
        {
            valueDir += 1;
        }
        else
        {
            valueDir -= 1;
        }
        if(Faces[valueDir])
            Destroy(Faces[valueDir]);
    }

    public void AddFace(Vector3 Direction)
    {
        GameObject Face = Instantiate(CubeManager.Manager.Face, transform.position + Direction/2, transform.rotation);
        Face.transform.parent = this.transform;
        Face.transform.eulerAngles = new Vector3(Direction.y * 90, 
            (Direction.x * -90) + Mathf.Clamp(Direction.z * 180,0,180), 0);
        Faces[CubeManager.Manager.DirToPos(Direction)] = Face;
    }

    public void CalculateVoisin()
    {
        RaycastHit hit;
        foreach (Vector3 Dir in CubeManager.Manager.AllDirection)
        {
            
            if (Physics.Raycast(transform.position, Dir, out hit, 1, CubeManager.Manager.Layer))
            {
                hit.transform.gameObject.GetComponent<CubeBehavior>().DestroyFace(Dir);
            }
            else
            {
                AddFace(Dir);
            }
        }

    }
}
