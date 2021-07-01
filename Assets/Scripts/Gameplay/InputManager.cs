using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private NPCFactory npcFactory;
    [SerializeField] private GameObject plane;

    private Camera mainCam;
    private LayerMask planeMask;

	private void Awake()
	{
        mainCam = Camera.main;
        planeMask = 1 << plane.layer;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            var clickRay = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickRay, out RaycastHit hitInfo, Mathf.Infinity, planeMask))
			{
                npcFactory.Spawn(hitInfo.point);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
