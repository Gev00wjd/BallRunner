using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    
    [SerializeField] private Text LiveText; 
    
    private bool isGame;
    private float _timerLive;
    private Rigidbody _rb;

    [SerializeField] private float speed;
    

    private void Awake()
    {
        GameOverUI.SetActive(false);
        isGame = true;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isGame) return;
        _timerLive += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _rb.useGravity = false;
            Vector3 newPos = transform.position;
            newPos += Vector3.up * speed;
            _rb.MovePosition(newPos);
        }
        else
        {
            _rb.useGravity = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstracle"))
        {
            isGame = false;
            GameOverUI.SetActive(true);
            LiveText.text = "Вы выжили: " + (int)_timerLive + " секунд";
        }
    }

    public void ChangeLevel()
    {
        MenuManager.Game = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        MenuManager.Game = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
