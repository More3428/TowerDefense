using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
   public Color hoverColor;
   public Vector3 positionOffset; 

   [Header("Optional")]
   public GameObject turret;

   BuildManager buildManager;
   

   private Renderer rend;
   private Color startColor;

   private void Start()
   {
      rend = GetComponent<Renderer>();
      startColor = rend.material.color; 
      
      buildManager = BuildManager.instance;
   }

   public Vector3 GetBuildPosition()
   {
      return transform.position + positionOffset;
   }

   private void OnMouseDown()
   {
      if (EventSystem.current.IsPointerOverGameObject())
      {
         return; 
      }
      
      if (!buildManager.CanBuild)
         return; 
      
      if (turret != null)
      {
         Debug.Log("Cant Build here! - TODO: Display onScreen");
         return;
         
      }
      //Build Turret
      buildManager.BuildTurretOn(this);
   }

   private void OnMouseEnter()
   {
      if (EventSystem.current.IsPointerOverGameObject())
      {
         return; 
      }
      
      if (!buildManager.CanBuild)
         return; 
      
      rend.material.color = hoverColor;
   }

   private void OnMouseExit()
   {
      rend.material.color = startColor;
   }
}
