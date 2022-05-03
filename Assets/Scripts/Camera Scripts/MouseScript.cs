using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    public GameObject mousePoint;
    private GameObject instantiatedMouse;


    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, mode);
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 0.25f;

                    if (instantiatedMouse == null)
                    {
                        instantiatedMouse = Instantiate(mousePoint) as GameObject;
                        instantiatedMouse.transform.position = temp;
                    }
                    else
                    {
                        Destroy(instantiatedMouse);
                        instantiatedMouse = Instantiate(mousePoint) as GameObject;
                        instantiatedMouse.transform.position = temp;
                    }
                }
            }
        }
    }
}
