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
    private GameObject speakerImageBG;
    private Image speakerImage;

    public bool isSpeaking = true;
    //private int currentDialogID;
    private Dictionary<string, Sprite> speakerImageDic;
    public string[] currentDialogContents;
    private int currentLine = -1;

    private Animator blackLine_Animator;
    private Animator speakerImageBG_Animator;

    private PlayableDirector playableDirector;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSpeaking)
        {
            ToNextSentence();
        }

    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_DialogPanelModel = gameObject.GetComponent<DialogPanelModel>();

        dialogBox = m_Transform.Find("DialogBackground").gameObject;
        dialogText = m_Transform.Find("DialogBackground/DialogBlackLine/DialogText").GetComponent<TextMeshProUGUI>();
        npcNameText = m_Transform.Find("DialogBackground/DialogBlackLine/NPCNameText").GetComponent<TextMeshProUGUI>();
        speakerImageBG = m_Transform.Find("DialogBackground/SpeakerImageBG").gameObject;
        speakerImage = m_Transform.Find("DialogBackground/SpeakerImageBG/SpeakerImage").GetComponent<Image>();

        speakerImageDic = new Dictionary<string, Sprite>();

        speakerImageDic = m_DialogPanelModel.GetSpeakerImageDic();

        blackLine_Animator = m_Transform.Find("DialogBackground/DialogBlackLine").GetComponent<Animator>();
        speakerImageBG_Animator = m_Transform.Find("DialogBackground/SpeakerImageBG").GetComponent<Animator>();

        playableDirector = GameObject.Find("TimelineManager").GetComponent<PlayableDirector>();

        //GetDialogFromDialogPanelModel("dialog_0");
        //ToNextSentence();
    }

    public void GetDialogFromDialogPanelModel(string dialogName)
    {
        //playableDirector.Pause();
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        DialogBoxShow();
        DialogBoxFadeIn();

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
                speakerImageBG.gameObject.SetActive(false);
            }
            else
            {
                speakerImageBG.gameObject.SetActive(true);
                speakerImage.sprite = speakerSprite;
            }

            npcNameText.text = temp[0];
            dialogText.text = temp[1];
        }
        else
        {
            DialogBoxFadeOut();
        }
    }

    public void DialogBoxHide()
    {
        dialogBox.SetActive(false);
    }

    public void DialogBoxFadeOut()
    {
        //playableDirector.Play();
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        currentLine = -1;
        isSpeaking = false;

        blackLine_Animator.Play("BlankLineFadeOut");
        speakerImageBG_Animator.Play("SpeakerImageBGFadeOut");
    }

    private void DialogBoxFadeIn()
    {
        isSpeaking = true;

        blackLine_Animator.Play("BlankLineFadeIn");
        speakerImageBG_Animator.Play("SpeakerImageBGFadeIn");
    }

    private void DialogBoxShow()
    {
        dialogBox.SetActive(true);
    }
}