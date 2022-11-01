using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startGameScene;
    
    [SerializeField] Road _levelManager;
    

    public void HardLevel()
    {
        _levelManager.ChangeMaxSpeed(40);
        
        MenuManager.instance.OpenMenu("StartGame");
    }
    public void MediumLevel()
    {
        _levelManager.ChangeMaxSpeed(20);
        MenuManager.instance.OpenMenu("StartGame");
    }
    public void EasyLevel()
    {
        _levelManager.ChangeMaxSpeed(10);
        MenuManager.instance.OpenMenu("StartGame");
    }
    public void Start_Game()
    {
        startGameScene.SetActive(true);
        gameObject.SetActive(false);
    }
}
