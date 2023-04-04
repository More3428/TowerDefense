using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI; 

public class Node : MonoBehaviour
{
   public Color hoverColor;
   public Color  notenoughmoneyColor;
   public Vector3 positionOffset;

  

   [HideInInspector]
   public GameObject turret;
   [HideInInspector]
   public TurretBlueprint turretBlueprint;
   [HideInInspector]
   public bool isUpgraded = false;
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
         buildManager.selectNode(this);
         return;
         
      }
      if (!buildManager.CanBuild)
         return; 
      //Build Turret
      BuildTurret(buildManager.GetTurretToBuild());
   }

   void BuildTurret(TurretBlueprint blueprint)
   {
      if (PlayerStats.Money < blueprint.cost)
      {
         Debug.Log("Not Enough Mula to Build foo");
         return; 
      }

      PlayerStats.Money -= blueprint.cost;
      
      GameObject _turret =(GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
      turret = _turret;

      turretBlueprint = blueprint;

      GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
      Destroy(effect, 5f);
    
      Debug.Log("Turret Purchased!");
   }

   public void SellTurret()
   {
      PlayerStats.Money += turretBlueprint.GetSellAmount(); 
      
      //SPAwn cool effect
      GameObject effect = (GameObject) Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
      Destroy(effect, 5f);
      
      Destroy(turret);
      turretBlueprint = null; 
   }

   public void UpgradeTurret()
   {
      if (PlayerStats.Money < turretBlueprint.upgradeCost)
      {
         Debug.Log("Not Enough Mula to Upgrade foo");
         return; 
      }

      PlayerStats.Money -= turretBlueprint.upgradeCost;
      //DEStroy old Turret
      Destroy(turret);
      
      //Build new upgraded Turret
      GameObject _turret =(GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
      turret = _turret;

      GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
      Destroy(effect, 5f);

      isUpgraded = true; 
    
      Debug.Log("Turret Upgraded!");
   }

   private void OnMouseEnter()
   {
      if (EventSystem.current.IsPointerOverGameObject())
      {
         return; 
      }
      
      if (!buildManager.CanBuild)
         return;

      if (buildManager.HasMoney)
      {
         rend.material.color = hoverColor;
      }
      else
      {
         rend.material.color = notenoughmoneyColor;
      }
      
   }

   private void OnMouseExit()
   {
      rend.material.color = startColor;
   }
}
