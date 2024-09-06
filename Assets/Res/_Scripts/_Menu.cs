using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _Menu : MonoBehaviour
{
    [SerializeField] private RectTransform panelTransform; // Панель, которую мы будем перемещать
    [SerializeField] private float moveDuration = 1f;    // Длительность перемещения
    [SerializeField] private GameObject textMeshProUGUI;

    public bool isPanelMoved = false;  // Флаг, указывающий, перемещена ли уже панель
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

        // Инвертируем флаг после каждого нажатия кнопки
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
            // Загружаем сохраненное количество очков
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
            yield return null; // Подождать до следующего кадра
        }

        // Гарантируем, что мы в конечной позиции после завершения корутины
        panelTransform.anchoredPosition = new Vector2(endX, panelTransform.anchoredPosition.y);
    }

}
