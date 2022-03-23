
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject userPanel;
    int currentSceneIndex;
    int nextSceneIndex;

    public GameObject partSelector;
    public GameObject FirstPartLevels;
    public GameObject SecondPartLevels;
    public GameObject ThirdPartLevels;

    public static int chosenMapId = 1;

    [SerializeField]GameObject[] partPanels;

  
    public void SetPartPanelsInactive()
    {
        foreach(GameObject part in partPanels)
        {
            if (part.activeInHierarchy)
            {
                OpenExitButton.Exit(part);
            }
        }
    }

    public void SetFirstPanelActive()
    {
        SetPartPanelsInactive();
        OpenExitButton.Open(FirstPartLevels);
    }
    public void SetSecondPanelActive()
    {
        SetPartPanelsInactive();
        OpenExitButton.Open(SecondPartLevels);
    }
    public void SetThirdPanelActive()
    {
        SetPartPanelsInactive();
        OpenExitButton.Open(ThirdPartLevels);
    }

    public void usernamePanel()
    {
        OpenExitButton.Open(userPanel);
    }

    public void PartSelector()
    {
        OpenExitButton.Open(partSelector);
    }

    public void SetPartSelectorInactive()
    {
        OpenExitButton.Exit(partSelector);
    }

    void Start()
    {
       // CheckFirstLevel();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(chosenMapId, LoadSceneMode.Single);
    }

    public void FirstLevel()
    {
        SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);

    }

    public void SetUserPanelInactive()
    {
        OpenExitButton.Exit(userPanel);
    }

   


}
