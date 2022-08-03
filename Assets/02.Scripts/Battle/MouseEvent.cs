using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
   
    public Inventory inventory;
    private GameObject m_RayTarget;

    // Double Click 처리용 변수
    private float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    // Double Tap 처리용 변수
    private float m_LastTouchTime;
    private const float m_DoubleTouchDelay = 0.5f;

    void Start()
    {
        m_LastTouchTime = Time.time;
    }

    void Update()
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_IsOneClick = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick)
            {
                m_Timer = Time.time;
                m_IsOneClick = true;
            }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;

                // 마우스 더블 클릭시 Hero Inventory로 돌아간다.
                CastRay();

                if (m_RayTarget != null)
                {
                    IInventoryHero hero = m_RayTarget.GetComponent<IInventoryHero>();
                    inventory.AddItem(hero);
                    // Debug.Log(hero.Name + "마우스 클릭으로 들어감");
                }
            }
        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Time.time - m_LastTouchTime < m_DoubleTouchDelay) // Double Tap 판정
                    {
                        // 스크린 더블 탭일시 Hero Inventory로 돌아간다.
                        CastRay();

                        if (m_RayTarget != null)
                        {
                            IInventoryHero hero = m_RayTarget.GetComponent<IInventoryHero>();
                            inventory.AddItem(hero);
                        }
                    }
                    else
                    {
                        // 그냥 한번 Tap
                    }
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    m_LastTouchTime = Time.time;
                    break;
            }
        }
    }

    void CastRay()
    {
        m_RayTarget = null;
        Vector2 worldPoint = new Vector2(0, 0);

        if (Input.mousePosition != null)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        // Ray에 Object가 hit 되었다면
        if (hit.collider != null)
        {
            // Debug.Log (hit.collider.name);
            // hit된 게임 오브젝트를 타겟으로 지정
            m_RayTarget = hit.collider.gameObject;  

        }
    }
}
