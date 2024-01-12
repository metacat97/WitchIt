using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class TestCustom : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject[] rightCustoms;
    private int rightCurrentCustom;

    public GameObject[] leftCustoms;
    private int leftCurrentCustom;

    public Button saveButton;
    public Button switchRightButton;
    public Button switchLeftButton;

    // Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(SaveCustom);

        switchRightButton.onClick.AddListener(SwitchRightCustom);
        switchLeftButton.onClick.AddListener(SwitchLeftCustom);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRightCustom();
        UpdateLeftCustom();
    }

    public void SwitchRightCustom()
    {
        if (rightCustoms.Length > 0)
        {
            rightCurrentCustom = (rightCurrentCustom + 1) % rightCustoms.Length;
        }
    }

    public void SwitchLeftCustom()
    {
        if (leftCustoms.Length > 0)
        {
            leftCurrentCustom = (leftCurrentCustom + 1) % leftCustoms.Length;
        }
    }


    void UpdateRightCustom()
    {
        for (int i = 0; i < rightCustoms.Length; i++)
        {
            if (i == rightCurrentCustom)
            {
                rightCustoms[i].SetActive(true);
            }
            else
            {
                rightCustoms[i].SetActive(false);
            }
        }
    }

    void UpdateLeftCustom()
    {
        for (int i = 0; i < leftCustoms.Length; i++)
        {
            if (i == leftCurrentCustom)
            {
                leftCustoms[i].SetActive(true);
            }
            else
            {
                leftCustoms[i].SetActive(false);
            }
        }
    }

    public void SaveCustom()
    {
        //Debug.Log("SaveCustom �Լ��� ȣ��Ǿ����ϴ�.");

        string path = "Assets/Resources";
        Directory.CreateDirectory(path);

        // Ŀ���� ������Ʈ�� ��ģ �� ����
        if (rightCurrentCustom >= 0 && rightCurrentCustom < rightCustoms.Length &&
            leftCurrentCustom >= 0 && leftCurrentCustom < leftCustoms.Length)
        {
            //Debug.Log("SaveCustomObject ȣ�� ��");
            SaveCustomObject(rightCustoms[rightCurrentCustom], leftCustoms[leftCurrentCustom]);
            //Debug.Log("SaveCustomObject ȣ�� ��");
        }
        else
        {
            //Debug.Log("SaveCustomObject ȣ�� ���� ������");
            //Debug.Log($"rightCurrentCustom: {rightCurrentCustom}, leftCurrentCustom: {leftCurrentCustom}");
            //Debug.Log($"rightCustoms.Length: {rightCustoms.Length}, leftCustoms.Length: {leftCustoms.Length}");
        }
    }

    void SaveCustomObject(GameObject rightCustomObject, GameObject leftCustomObject)
    {
        rightCustomObject.transform.SetParent(PlayerObject.transform);
        leftCustomObject.transform.SetParent(PlayerObject.transform);

        string prefabPath = Path.Combine("Assets/Resources", PlayerObject.name + ".prefab");

        // ������ ��� Ȯ��
        //Debug.Log("prefabPath ��: " + prefabPath);

#if UNITY_EDITOR
        // �÷��̾� ������Ʈ(���� Ŀ���� ������Ʈ�� ����)�� ���������� ����
        PrefabUtility.SaveAsPrefabAsset(PlayerObject, prefabPath);
#endif
        //Debug.Log("PrefabUtility.SaveAsPrefabAsset �Լ��� ȣ��Ǿ����ϴ�.");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
