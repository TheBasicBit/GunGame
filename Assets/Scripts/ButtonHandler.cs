using System;
using System.Collections;
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
            StartCoroutine(LoadScene("GameScene"));
        }

        public IEnumerator LoadScene(string name)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    Logger.LogInfo("Loaded scene: ");
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }

                yield return null;
            }
        }
    }
}