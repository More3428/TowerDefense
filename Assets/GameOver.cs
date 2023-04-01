using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public TMP_Text roundsText;

   private void OnEnable()
   {
      roundsText.text = PlayerStats.Rounds.ToString(); 
   }

   public void Retry()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
   }

   public void Menu()
   {
      Debug.Log("Go to Menu");
   }
}
