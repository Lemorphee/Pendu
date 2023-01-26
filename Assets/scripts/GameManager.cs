using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{                                       //constructeur
    private List<string> listeDeMots = new List<string>() { "lecture", "opiac�", "ouvrier", "charisme", "patate","soleil", "corde", "ensoleill�", "ch�vre", "�toile", "nuage"};
    [SerializeField]
    private string motaDeviner;
    private char[] motCache;
   // [SerializeField] = afficher une variable priv�e dans l'inspector
    private char inputChar;
    public int vie;
    private bool gameOver;
    public List<char> lettreUtilisee = new List<char>();

    private UIManager uIManager;


    private void Awake()
    {
        uIManager = GetComponent<UIManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        RandomWord();
        uIManager.InitUi();
    }

    // CheckInput est appel�e par l'Inputfield, l� ou le joueur �crira dans le jeu
    public void CheckInput()
    {
        if (gameOver == false)
        {
            char input;
            input = uIManager.inputField.text[uIManager.inputField.text.Length - 1];
            // si + d'une lettre = on efface toutes les lettres sauf la derni�re
            uIManager.inputField.text = input.ToString().ToUpper();
            // Debug.Log sert a afficher quelque chose dans la console
            Debug.Log(uIManager.inputField.text);
        }
        
    }
    private bool UsedLetter(char input)
    {
       if (lettreUtilisee.Contains(input))
        {
            return true;
        }
       else
        {
            return false;
        }
    }
    //d�s le lancement de la partie, le jeu choisira un mot al�atoire parmis la liste des mots donn�s
    private void RandomWord()
    {
        int rng = Random.Range(0, listeDeMots.Count);
        motaDeviner = listeDeMots[rng];
        HidingWord(motaDeviner);

    }
    // on cache le mot sous des underscores, qu'on espace dans Unity, comme un vrai pendu
    private void HidingWord(string mot)
    { //on d�finit d'abord la taille de l'array en fonction de la taille du mot
        motCache = new char[mot.Length];
        //ensuite la loop commence
        for (int i = 0; i < mot.Length; i++)
        {
            motCache[i] = '_';
        }
        //la loop finit, puis on prends le mot affich� dans le TextMeshPro (motMyst�reUI.text)
        uIManager.DisplayWord(Transformot());
    }
    //variable qui fait que une fois test�e, si la lettre est correcte, elle vient remplacer l'underscore ou elle doit �tre
    private string Transformot()
    {
        string wordToDisplay = "";
        for (int i = 0; i < motCache.Length; i++)
        {
            wordToDisplay += char.ToUpper(motCache[i]);
        }

        return wordToDisplay;
    }
    public void TestLettres()
    { //si le joueur trouve la lettre on affiche bravo dans le log et on affiche la lettre inscrite, sinon le joueur "gagne" une vie
        bool isLetterFound = false;
        inputChar = uIManager.inputField.text[0];

        if (UsedLetter(inputChar))
        {
            uIManager.DisplayNarration("cette lettre a d�j� �t� utilis�e", Color.red);

        }
        else
        {
            lettreUtilisee.Add(inputChar);
            uIManager.DisplayUsedLetter();
            //inputField.text = "";
            for (int i = 0; i < motaDeviner.Length; i++)
            {
                if (char.ToUpper(motaDeviner[i]) == char.ToUpper(inputChar))
                // if(motaDeviner[i].ToString().ToUpper() == inputChar.ToString().ToUpper())
                {
                    motCache[i] = inputChar;
                    isLetterFound = true;
                }
            }
            if (isLetterFound == true)
            {
                Debug.Log("Bravo");
                uIManager.DisplayWord(Transformot());
                Victoire();
            }
            else
            {
                vie++;
                uIManager.InserImage();
                Defaite();
            }
        }
    }
    //si le joueur trouve le mot, alors le narrateur le lui indique par une phrase
    private void Victoire()
    {
        if (motaDeviner.ToUpper() == uIManager.motMyst�reUI.text)
        {
            //on change la couleur en vert
            uIManager.DisplayGameOver("Bien jou� tu as gagn�", uIManager.couleurPositive);
            gameOver = true;
            uIManager.PannelCache(true);
        }
        else
        {
            uIManager.PhrasesDuNarrateur(uIManager.listeDeWin);
        }
    } 
    //si le joueur "gagne" jusqu'� 8 vies, alors il perd et le "narrateur" le lui dit
    private void Defaite()
    {
        if (vie == 8)
        {
            //on change la couleur en rouge
            uIManager.DisplayGameOver("Et voil�, pendu par nullit�", uIManager.couleurNegative);
            gameOver = true;
            uIManager.PannelCache(true);
        }
        else
        {
            uIManager.PhrasesDuNarrateur(uIManager.listeDeLoose);
        }
    }
}