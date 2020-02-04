using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlindDeer.Game.PiouPiou
{
    public class ButtonHandler : MonoBehaviour
    {
        public void OnMainMenuPlayButtonClicked()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}