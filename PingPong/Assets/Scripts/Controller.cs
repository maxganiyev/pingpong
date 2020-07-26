using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public bool isPlaying;
    public int score;
    public int highScore;

    public Sprite[] skins;
    public SpriteRenderer ballRenderer;

    public GameObject mainMenuObj;
    public GameObject skinsMenuObj;
    public GameObject endMenuObj;

    public Text scoreText;
    public Text highScoreText;
    public Text endMenuScoreText;
    public Text endMenuHighScoreText;

    [HideInInspector]
    public Rect screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("score");
        highScoreText.text = highScore.ToString();

        ballRenderer.sprite = skins[PlayerPrefs.GetInt("skin")];

        //Calculate screen bounds
        screenBounds = new Rect(Camera.main.ScreenToWorldPoint(Vector3.zero).x,
                                Camera.main.ScreenToWorldPoint(Vector3.zero).y,
                                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x,
                                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void Play()
    {
        isPlaying = true;
        mainMenuObj.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        isPlaying = false;

        endMenuObj.SetActive(true);
        scoreText.gameObject.SetActive(false);

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("score", highScore);
        }

        PlayerPrefs.SetInt("skin", Random.Range(0, skins.Length));

        endMenuScoreText.text = score.ToString();
        endMenuHighScoreText.text = highScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSkins()
    {
        mainMenuObj.SetActive(false);
        skinsMenuObj.SetActive(true);
        ballRenderer.gameObject.SetActive(false);
    }

    public void CloseSkins()
    {
        mainMenuObj.SetActive(true);
        skinsMenuObj.SetActive(false);
        ballRenderer.gameObject.SetActive(true);
    }

    public void SelectSkin(int id)
    {
        ballRenderer.sprite = skins[id];
        PlayerPrefs.SetInt("skin", id);
    }
}
