using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    //-------Init----------------

    public Transform dialoguePoint;
    public GameObject speechBubble;
    public GameObject respBubble;

    public SpriteData dialogueData;
    List<string> dialogues;
    List<SpriteData.ResponseOptions> responseOptions;

    int index = 0;

    int typewriterIndex = 0;

    bool isLoadFinished = false;
    bool isTextDisplayed = false;
    bool isResponseDisplayed = false;
    bool waitCor = false;

    bool specialChar = false;

    float scale = 3f;
    bool isShaky = false;
    bool isWeavy = false;

    Alphabet alphabet;

    //-------Update----------------

    public GameObject[] shakyText;
    public GameObject[] weavyText;

    //-------OpenShop-------------

    public bool openShop = false;

    void Start()
    {
        alphabet = Alphabet.instance;
        dialogues = dialogueData.GetDialogue();
    }

    void Update()
    {
        dialoguePoint.rotation = Quaternion.Euler(0, 0, 0);

        StartCoroutine(WaitTypewriteFx());

        if (isTextDisplayed)
        {
            shakyText = GameObject.FindGameObjectsWithTag("ShakyText");
            weavyText = GameObject.FindGameObjectsWithTag("WeavyText");


            if (shakyText.Length != 0)
            {
                foreach (GameObject sT in shakyText)
                {
                    sT.transform.position += Random.insideUnitSphere * 0.002f;
                    sT.transform.position.Set(sT.transform.position.x, sT.transform.position.y, 0);
                }
            }

            if (weavyText != null)
            {
                foreach (GameObject wT in weavyText)
                {
                    wT.transform.position += Vector3.up * Mathf.Sin(wT.transform.position.x * 1f + 10 * Time.time) * 0.004f;
                    wT.transform.position.Set(wT.transform.position.x, wT.transform.position.y, 0);
                }
            }
        }
    }

    public void LoadDialogue()
    {
        Vector3 spriteTransform = dialoguePoint.transform.position;
        spriteTransform.z = -4;
        Vector3 tempWidth = new Vector3(0, 0, 0);
        if (dialogues[index] != null)
        {
            foreach (char c in dialogues[index])
            {
                tempWidth.Set(0, 0, 0);

                if (c.Equals('<'))
                {
                    specialChar = true;
                }

                if (c.Equals('>'))
                {
                    specialChar = false;
                }

                if (c.Equals('/'))
                {
                    scale = 3f;
                    tempWidth.y = 0f;
                    isShaky = false;
                    isWeavy = false;
                }

                if (c.Equals('$'))
                {
                    isLoadFinished = true;
                    speechBubble.SetActive(true);
                    return;
                }

                if (c.Equals(' '))
                {
                    tempWidth.x += 0.4f;
                    spriteTransform += tempWidth;
                }

                if (specialChar)
                {
                    switch (c)
                    {
                        case 'n':
                            {
                                spriteTransform = dialoguePoint.transform.position;
                                spriteTransform.z = -4;
                                tempWidth.y = 0.5f;
                                spriteTransform.y -= tempWidth.y;
                            }
                            break;

                        case 's':
                            {
                                isShaky = true;
                            }
                            break;

                        case 'w':
                            {
                                isWeavy = true;
                            }
                            break;

                        case 'S':
                            {
                                Debug.Log("Open Shop");
                            }
                            break;

                        case 'C':
                            {
                                Debug.Log("Close dialogue");
                            }
                            break;
                    }
                }

                if (!specialChar && !c.Equals('>') && !c.Equals(' '))
                {
                    Sprite sprite = alphabet.inventoryNameDictionary[c].sprite;
                    GameObject go = new GameObject(c.ToString());
                    go.transform.parent = dialoguePoint.transform;
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();

                    go.transform.localScale /= scale;
                    tempWidth.x += 0.3f;
                    spriteTransform += tempWidth;
                    go.transform.localPosition += spriteTransform;

                    renderer.sprite = sprite;

                    if (isShaky)
                    {
                        go.tag = "ShakyText";
                    }

                    if (isWeavy)
                    {
                        go.tag = "WeavyText";
                    }

                    go.SetActive(false);
                }
            }
        }

    }

    public void nextDialogue()
    {
        if (index < dialogues.Count - 1)
        {
            isTextDisplayed = false;

            Transform dialoguePoint = gameObject.transform.Find("/" + this.name + "/DialoguePoint");
            foreach (Transform child in dialoguePoint)
            {
                GameObject.Destroy(child.gameObject);
            }


            index++;
            typewriterIndex = 0;
            waitCor = false;
            LoadDialogue();

        }

        else if(!isResponseDisplayed)
        {
            respBubble.SetActive(true);
            DisplayResponses();
            isResponseDisplayed = true;
        }
    }

    IEnumerator WaitTypewriteFx()
    {
        while (isLoadFinished && !isTextDisplayed && !waitCor)
        {
            waitCor = true;
            EnableChild(typewriterIndex);
            yield return new WaitForSeconds(0.05f);
            typewriterIndex += 1;
            waitCor = false;
        }
    }
    void EnableChild(int index)
    {
        if (dialoguePoint.transform.childCount > index)
        {
            dialoguePoint.transform.GetChild(index).gameObject.SetActive(true);
        }

        else
        {
            isTextDisplayed = true;
        }
    }

    public void InitDialogue()
    {
        isLoadFinished = false;
        isTextDisplayed = false;
        isResponseDisplayed = false;
        waitCor = false;
        typewriterIndex = 0;
        index = 0;

        specialChar = false;
        scale = 3f;
        isShaky = false;
        isWeavy = false;
    }

    public void DisplayResponses()
    {
        responseOptions = dialogueData.GetResponse();

        Vector3 resp1Transform = respBubble.transform.position;
        resp1Transform.z = -4;
        resp1Transform.x -= 1;
        resp1Transform.y += 0.6f;
        Vector3 temp1Width = new Vector3(0, 0, 0);

        foreach (char c in responseOptions[0].text)
        {
            temp1Width.Set(0, 0, 0);

            Sprite sprite = alphabet.inventoryNameDictionary[c].sprite;
            GameObject go = new GameObject(c.ToString());
            go.transform.parent = dialoguePoint.transform;
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();

            go.transform.localScale /= scale;
            temp1Width.x += 0.3f;
            resp1Transform += temp1Width;
            go.transform.localPosition += resp1Transform;
            go.transform.position.Set(go.transform.position.x, go.transform.position.y, -4);
            renderer.sprite = sprite;
        }


        Vector3 resp2Transform = respBubble.transform.position;
        resp2Transform.z = 0;
        resp2Transform.x -= 1;
        resp2Transform.y -= 0.1f;
        Vector3 temp2Width = new Vector3(0, 0, 0);

        foreach (char c in responseOptions[1].text)
        {
            temp2Width.Set(0, 0, 0);

            Sprite sprite = alphabet.inventoryNameDictionary[c].sprite;
            GameObject go = new GameObject(c.ToString());
            go.transform.parent = dialoguePoint.transform;
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();

            go.transform.localScale /= scale;
            temp2Width.x += 0.3f;
            resp2Transform += temp2Width;
            go.transform.localPosition += resp2Transform;
            go.transform.position.Set(go.transform.position.x, go.transform.position.y, 0);
            renderer.sprite = sprite;
        }

    }

    public void OnClickFirstChoice()
    {
        Transform dialoguePoint = gameObject.transform.Find("/" + this.name + "/DialoguePoint");
        if (dialogueData.GetResponse()[0].spriteData.name.Equals("OpenShop"))
        {

            openShop = true;

            foreach (Transform child in dialoguePoint)
            {
                GameObject.Destroy(child.gameObject);
            }

            speechBubble.SetActive(false);
            respBubble.SetActive(false);

            return;
        }

        isTextDisplayed = false;

        foreach (Transform child in dialoguePoint)
        {
            GameObject.Destroy(child.gameObject);
        }

        respBubble.SetActive(false);

        dialogueData = responseOptions[0].spriteData;
        dialogues = dialogueData.GetDialogue();
        InitDialogue();
        LoadDialogue();
    }

    public void OnClickSecondChoice()
    {
        Debug.Log("Non");

        Transform dialoguePoint = gameObject.transform.Find("/" + this.name + "/DialoguePoint");

        if (dialogueData.GetResponse()[1].spriteData.name.Equals("CloseDialogue"))
        {
            foreach (Transform child in dialoguePoint)
            {
                GameObject.Destroy(child.gameObject);
            }

            speechBubble.SetActive(false);
            respBubble.SetActive(false);

            return;
        }

        

        foreach (Transform child in dialoguePoint)
        {
            GameObject.Destroy(child.gameObject);
        }

        respBubble.SetActive(false);

        dialogueData = responseOptions[1].spriteData;
        dialogues = dialogueData.GetDialogue();
        InitDialogue();
        LoadDialogue();
    }
}
