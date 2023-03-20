using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
   public static BuildManager instance;

   private void Awake()
   {
      if (instance != null)
      {
         Debug.Log("More than one build manager in scene");
         return;
      }
      instance = this; 
   }

   public GameObject standardTurretPrefab;
   public GameObject missileLauncherPrefab; 
   
   private TurretBlueprint turretToBuild;
  
   public bool CanBuild { get { return turretToBuild != null; } }

   public void BuildTurretOn(Node node)
   {

      if (PlayerStats.Money < turretToBuild.cost)
      {
         Debug.Log("Not Enough Mula to Build foo");
         return; 
      }

      PlayerStats.Money -= turretToBuild.cost;
      
    GameObject turret =(GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
    node.turret = turret; 
    
    Debug.Log("Turret Purchased! Money left: " + PlayerStats.Money);
   }
   public void SelectTurretToBuild(TurretBlueprint turret)
   {
      turretToBuild = turret;
   }
}
