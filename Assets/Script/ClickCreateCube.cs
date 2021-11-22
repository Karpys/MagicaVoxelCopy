using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickCreateCube : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _baseCube;
    [SerializeField] private GameObject _basePreview;
    [SerializeField] private VoxelManager Manager;
    [SerializeField] private List<Vector3> NewPos;
    [SerializeField] private List<GameObject> PreviewObjects;
    [SerializeField] private int NumberCube = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 BasePosition = hit.collider.gameObject.transform.position;
                Vector3 Normal = hit.normal;
                CreateCubes(BasePosition, Normal);
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 BasePosition = hit.collider.gameObject.transform.position;
            Vector3 Normal = hit.normal;
            SetUpPreviewPosition(BasePosition,Normal);
        }
    }

    public void ProcessInput()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            AddLinePreviewCube();
        }else if (Input.mouseScrollDelta.y < 0)
        {
            RemoveLinePreview();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveAll();
        }
    }

    public void AddLinePreviewCube()
    {
        NumberCube += 1;
        GameObject Obj = Instantiate(_basePreview, transform.position, transform.rotation, Manager.PreviewHolder);
        PreviewObjects.Add(Obj);
    }

    public void RemoveLinePreview()
    {
        NumberCube -= 1;
        if (NumberCube != 0)
        {
            Destroy(PreviewObjects[PreviewObjects.Count-1]);
            PreviewObjects.RemoveAt(PreviewObjects.Count - 1);
        }
        NumberCube = Mathf.Max(1,NumberCube);
    }

    public void RemoveAll()
    {
        for (int i = NumberCube - 1; i > 0; i--)
        {
            Destroy(PreviewObjects[i]);
            PreviewObjects.RemoveAt(i);
        }
        NumberCube = 1;
    }

    public void CreateCubes(Vector3 Position,Vector3 Normal)
    {
        for (int i = 0; i < NumberCube; i++)
        {
            Instantiate(_baseCube, Position + (Normal*(i+1)), transform.rotation,Manager.CubeHolder);
        }
    }

    public void SetUpPreviewPosition(Vector3 Position,Vector3 Normal)
    {
        for (int i = 0; i < NumberCube; i++)
        {
            PreviewObjects[i].transform.position = Position + Normal*(i+1);
        }
    }

}
