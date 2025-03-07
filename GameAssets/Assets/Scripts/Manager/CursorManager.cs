using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : Singlten<CursorManager>
{
    private Vector3 mouseWorldPos=>Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
    private bool canClick;

    private void Update()
    {
        canClick = ObjectAtMousePos();

        if(canClick&&Input.GetMouseButtonDown(0))
        {
            if(ObjectAtMousePos().gameObject.GetComponent<IClick>()!=null)
            {
                ObjectAtMousePos().gameObject.GetComponent<IClick>().ClickEvent();
            }
        }
    }

    private Collider2D ObjectAtMousePos()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
