using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Sprite[] tableauPendu;
    public Image imageDuPendu;
    public TMP_InputField inputField;
    public TextMeshProUGUI motMystèreUI;
    public TextMeshProUGUI narrateurUI;
    public TextMeshProUGUI gameOverUI;
    public TextMeshProUGUI lettreUtiliseeUI;
    public Color parDefaut;
    public Color couleurPositive;
    public Color couleurNegative;
    public GameObject PannelDeFin;
    public List<string> listeDeWin = new List<string>() { "bien joué !", "bravo !", "GG !", "ouais c'est bon t'a trouvé pas de quoi en faire tout un plat...", "pas mal, j'avoue" };
    public List<string> listeDeLoose = new List<string>() { "t'es nul", "ez jungle diff", };


    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    public void InitUi()
    {
        InserImage();
        Debut();
        PannelCache(false);
    }
    public void DisplayUsedLetter()
    {
        lettreUtiliseeUI.text = "";
        foreach (char lettre in gameManager.lettreUtilisee)
        {
            lettreUtiliseeUI.text += lettre;
        }
    }
    public void DisplayWord(string wordToDisplay)
    {
        motMystèreUI.text = wordToDisplay;
    }
    public void DisplayNarration(string wordToDisplay, Color color)
    {
        narrateurUI.color = color;
        narrateurUI.text = wordToDisplay;
    }
    // pour afficher l'image du pendu, et l'accorder selon les vies
    public void InserImage()
    {
        imageDuPendu.sprite = tableauPendu[gameManager.vie];
    }
    //attribuer une couleur spéciale a chaque type de mots, négatif ou positif
    public void PhrasesDuNarrateur(List<string> liste)
    {
        int rng = Random.Range(0, liste.Count);
        if (liste == listeDeWin)
        { // couleur positive
            DisplayNarration(liste[rng], couleurPositive);
        }
        else
        { // couleur négative
            DisplayNarration(liste[rng], couleurNegative);
        }
    }
    public void Debut()
    {
        narrateurUI.text = "";
    }
    public void DisplayGameOver(string message, Color color)
    {
        gameOverUI.color = color;
        gameOverUI.text = message;
    }
    public void PannelCache(bool value)
    {
        PannelDeFin.SetActive(value);
    }
    public void Rejouer()
    {
        SceneManager.LoadScene(0);
    }
    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
