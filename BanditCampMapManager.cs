using UnityEngine;
using ThunderRoad;

namespace DLLs
{
    public class BanditCampMapManager : MonoBehaviour
    {
        [SerializeField]
        GameObject MobsRoot;
        [SerializeField]
        GameObject LevelEndZone;
        [SerializeField]
        EventLoadLevel levelLoader;
        [SerializeField]
        EventMessage msgEvent;

        bool hasRespawned;

        string msgStart = "Get back here after you reached the top!";
        string msgLoot = "You got the loot! Be careful while escaping, guards came back..";
        string msgEnd = "Well done ! Going back home..";

        private void Start()
        {
            hasRespawned = false;
            ShowMessage(msgStart);
        }

        public void RespawnAllMobs()
        {
            if (!hasRespawned)
            {
                hasRespawned = true;
                //Despawn the remaining enemies
                foreach (GameObject g in FindObjectsOfType<GameObject>())
                    if (g.activeInHierarchy && g.GetComponent<Creature>() != null && !g.name.Contains("Player"))
                        g.GetComponent<Creature>().Despawn();

                //Then respawn them all
                for (int i = 0; i < MobsRoot.transform.childCount; i++)
                    MobsRoot.transform.GetChild(i).gameObject.GetComponent<CreatureSpawner>().Spawn();

                LevelEndZone.SetActive(true);
                ShowMessage(msgLoot);
            }
        }

        public void EndGame()
        {
            ShowMessage(msgEnd);
            levelLoader.LoadLevel("Home");
        }

        void ShowMessage(string msg)
        {
            msgEvent.text = msg;
            msgEvent.ShowMessage();
        }
    }
}
