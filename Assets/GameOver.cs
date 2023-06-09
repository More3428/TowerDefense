using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public TMP_Text roundsText;

   public SceneFader sceneFader;
   
  
  public string menuSceneName = "MainMenu"; 

   private void OnEnable()
   {
      roundsText.text = PlayerStats.Rounds.ToString(); 
   }

   public void Retry()
   {
      sceneFader.FadeTo(SceneManager.GetActiveScene().name);
   }

   public void Menu()
   {
      sceneFader.FadeTo(menuSceneName);
   }
}
