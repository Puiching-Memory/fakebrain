using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间
using System.Collections;

public class FadeTextMeshPro : MonoBehaviour
{
    public float fadeDuration = 1f; // 透明度变化的持续时间
    private TextMeshPro textMeshPro; // 引用TextMeshPro组件
    private Coroutine fadeCoroutine; // 引用协程
    public GameObject TextObject;

    private bool IsMark = false;

    private void Awake()
    {
        textMeshPro = TextObject.GetComponent<TextMeshPro>(); // 获取TextMeshPro组件
        IsMark = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!IsMark) // 假设玩家的tag是"Player"
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine); // 如果已经在执行，先停止
            }
            fadeCoroutine = StartCoroutine(FadeText()); // 开始执行协程
            IsMark = true;
        }
    }

    private IEnumerator FadeText()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            Debug.Log("I faddddddddded");
            float alpha = time / fadeDuration; // 计算当前透明度
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha); // 设置透明度
            yield return null;
        }

        // 等待一秒
        yield return new WaitForSeconds(1f);

        time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = 1 - (time / fadeDuration); // 计算当前透明度，使其逐渐变透明
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha); // 设置透明度
            yield return null;
        }
    }
}
