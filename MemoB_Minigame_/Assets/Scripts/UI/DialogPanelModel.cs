using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Xml;
using UnityEngine.UI;

public class DialogPanelModel : MonoBehaviour
{
    //private Dictionary<int, List<string>> dialogDic;



    //void Awake()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //private Dictionary<int, List<string>> GetDialogDictionary(string jsonPath)
    //{
    //   // DialogDictionary temp = JsonMapper.ToObject<DialogDictionary>(jsonPath);

    //    List<string> stringList = new List<string>();




    //    Dictionary<int, List<string>> tempDic = new Dictionary<int, List<string>>;

    //    return tempDic;
    //}

    public Dictionary<string, Sprite> GetSpeakerImageDic()
    {
        Dictionary<string, Sprite> speakerImageDic = new Dictionary<string, Sprite>();

        Sprite[] spriteArr = Resources.LoadAll<Sprite>("SpeakerImage");

        for (int i = 0; i < spriteArr.Length; i++)
        {
            speakerImageDic.Add(spriteArr[i].name, spriteArr[i]);
        }

        return speakerImageDic;
    }

    public string[] GetDialogFromXml(string dialogName)
    {
        XmlDocument xmlDocument = new XmlDocument();
        TextAsset textAsset = Resources.Load<TextAsset>("Datas/DialogXMLData");
        xmlDocument.LoadXml(textAsset.text);
        Resources.UnloadAsset(textAsset);

        XmlElement root = xmlDocument.DocumentElement;
        XmlNode dialogNode = root.GetElementsByTagName(dialogName)[0];
        XmlNodeList contentList = dialogNode.ChildNodes;
        string[] contents = new string[contentList.Count];
        for (int i = 0; i < contentList.Count; i++)
        {
            contents[i] = contentList[i].InnerText;
        }
        return contents;
    }
}
