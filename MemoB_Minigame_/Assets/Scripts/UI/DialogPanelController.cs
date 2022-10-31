using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogPanelController : MonoBehaviour
{
    private Transform m_Transform;

    private DialogPanelModel m_DialogPanelModel;

    private GameObject dialogBox;
    private TextMeshProUGUI npcNameText;
    private TextMeshProUGUI dialogText;
    private Image speakerImage;

    public bool isSpeaking = true;
    //private int currentDialogID;
    private Dictionary<string, Sprite> speakerImageDic;
    public string[] currentDialogContents;
    private int currentLine = -1;

    private PlayableDirector playableDirector;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogBox.activeInHierarchy)
        {
            ToNextSentence();
        }

        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playableDirector.state == PlayState.Playing)
            {
                playableDirector.Pause();
            }
            else
            {
                playableDirector.Play();
            }
        }
    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_DialogPanelModel = gameObject.GetComponent<DialogPanelModel>();

        dialogBox = m_Transform.Find("DialogBackground").gameObject;
        dialogText = m_Transform.Find("DialogBackground/DialogText").GetComponent<TextMeshProUGUI>();
        npcNameText = m_Transform.Find("DialogBackground/NPCNameText").GetComponent<TextMeshProUGUI>();
        speakerImage = m_Transform.Find("DialogBackground/SpeakerImage").GetComponent<Image>();

        speakerImageDic = new Dictionary<string, Sprite>();

        speakerImageDic = m_DialogPanelModel.GetSpeakerImageDic();

        playableDirector = GameObject.Find("TimelineManager").GetComponent<PlayableDirector>();

        //GetDialogFromDialogPanelModel("dialog_0");
        //ToNextSentence();
    }

    public void GetDialogFromDialogPanelModel(string dialogName)
    {
        playableDirector.Pause();
        DialogBoxShow();

        currentDialogContents = m_DialogPanelModel.GetDialogFromXml(dialogName);
        ToNextSentence(); 
    }

    private void ToNextSentence()
    {
        if (currentLine + 1 < currentDialogContents.Length)
        {
            currentLine++;
            string[] temp = currentDialogContents[currentLine].Split('£º', 2);

            Sprite speakerSprite;
            speakerImageDic.TryGetValue(temp[0], out speakerSprite);
            if (speakerSprite == null)
            {
                speakerImage.gameObject.SetActive(false);
            }
            else
            {
                speakerImage.gameObject.SetActive(true);
                speakerImage.sprite = speakerSprite;
            }

            npcNameText.text = temp[0];
            dialogText.text = temp[1];
        }
        else
        {
            DialogBoxHide();
        }
    }

    public void DialogBoxHide()
    {
        playableDirector.Play();
        currentLine = -1;
        isSpeaking = false;
        dialogBox.SetActive(false);
    }

    private void DialogBoxShow()
    {
        isSpeaking = true;
        dialogBox.SetActive(true);
    }
}