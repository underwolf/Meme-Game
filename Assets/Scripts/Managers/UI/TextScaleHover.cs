using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextScaleHover : MonoBehaviour
{
    public float m_Range = 0.05f;
    public float m_SmoothTime = 4.0f;
    private Vector3 m_Scale, m_OriginalScale, m_SelectedScale;
    private bool m_PointerState;

    private void Start()
    {
        m_Scale = transform.localScale;
        m_OriginalScale = transform.localScale;
        m_SelectedScale = 1.5f * (transform.localScale);
        m_PointerState = false;
    }

    public void ObjectSelected()
    {
        m_PointerState = true;
    }

    public void ObjectDeselected()
    {
        m_PointerState = false;
    }

    private void FixedUpdate()
    {
        if(m_PointerState == true)
        {

            transform.localScale = Vector3.Lerp(transform.localScale, m_SelectedScale, (m_SmoothTime * Time.deltaTime));
        }
        if(m_PointerState == false)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, m_OriginalScale, (m_SmoothTime * Time.deltaTime));
        }
       
    }
}
