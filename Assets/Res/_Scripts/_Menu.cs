using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _Menu : MonoBehaviour
{
    [SerializeField] private RectTransform panelTransform; // ������, ������� �� ����� ����������
    [SerializeField] private float moveDuration = 1f;    // ������������ �����������
    [SerializeField] private GameObject textMeshProUGUI;

    public bool isPanelMoved = false;  // ����, �����������, ���������� �� ��� ������
    bool y = true;

    public void MovePanel()
    {
        if (!isPanelMoved)
        {
            StartCoroutine(MovePanelCoroutine(0f, -689f));
        }
        else
        {
            StartCoroutine(MovePanelCoroutine(-689f, 0f));
        }

        // ����������� ���� ����� ������� ������� ������
        isPanelMoved = !isPanelMoved;
    }    public void interectibleVoid(GameObject buttons)
    {
        buttons.GetComponent<Button>().interactable = false;
    }
    public void OnClickLeaderBoard()
    {
        int x;        

        if (y)
        {
            y = false;
            textMeshProUGUI.SetActive(true);
            // ��������� ����������� ���������� �����
            if (PlayerPrefs.HasKey("SavedScore"))
            {
                x = PlayerPrefs.GetInt("SavedScore");
                textMeshProUGUI.GetComponent<TextMeshProUGUI>().text = x.ToString();
            }
        }
        else
        {
            y = true;
            textMeshProUGUI.SetActive(false);
        }
    }
    private IEnumerator MovePanelCoroutine(float startX, float endX)
    {
        float elapsedTime = 0f;
        float currentX = startX;

        while (elapsedTime < moveDuration)
        {
            currentX = Mathf.Lerp(startX, endX, elapsedTime / moveDuration);
            panelTransform.anchoredPosition = new Vector2(currentX, panelTransform.anchoredPosition.y);

            elapsedTime += Time.deltaTime;
            yield return null; // ��������� �� ���������� �����
        }

        // �����������, ��� �� � �������� ������� ����� ���������� ��������
        panelTransform.anchoredPosition = new Vector2(endX, panelTransform.anchoredPosition.y);
    }

}
