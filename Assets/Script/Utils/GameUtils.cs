using UnityEngine;
using UnityEngine.UI;

public class GameUtils
{
    public static int Interpolate(int input, int minInput, int maxInput, int minOutput, int maxOutput)
    {
        float t = Mathf.InverseLerp(minInput, maxInput, input);
        return Mathf.RoundToInt(Mathf.Lerp(minOutput, maxOutput, t));
    }

    public static float Map(float input, float minInput, float maxInput, float minOutput, float maxOutput)
    {
        return (input - minInput) * (maxOutput - minOutput) / (maxInput - minInput) + minInput;
    }

    public static string TextHL(string text, string color)
    {
        return $"<color={color}>{text}</color>";
    }

    public static Color HexToColor(string hex)
    {
        hex = hex.TrimStart('#');
        byte red = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte green = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte blue = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(red, green, blue, 255);
    }

    public static Vector2 GetSizeScreen()
    {
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight * Screen.width / Screen.height;
        return new Vector2(screenWidth, screenHeight);
    }

    public static void RectTransformAnchor0(Transform transform)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    public static void ForceRebuildLayout(GameObject gameObject)
    {
        foreach (var layoutGroup in gameObject.GetComponentsInChildren<LayoutGroup>())
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
        }

        var layout = gameObject.GetComponent<LayoutGroup>();
        if (layout)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
        }
    }

    public static string CurrencyFormat(int currency)
    {
        return string.Format("{0:" + "#,##0" + "}", currency);
    }

    public static int ParseCurrency(string currencyString)
    {
        string currencyWithoutCommas = currencyString.Replace(",", "");
        return int.Parse(currencyWithoutCommas);
    }

    public static string AmountFormat(int currency)
    {
        if (currency >= 1000000)
        {
            return string.Format("{0:#,##0.##}M", currency / 1000000m);
        }
        else if (currency >= 1000)
        {
            return string.Format("{0:#,##0.##}K", currency / 1000m);
        }
        else
        {
            return string.Format("{0:#,##0}", currency);
        }
    }

    public static int ParseAmount(string value)
    {
        if (value.EndsWith("K") || value.EndsWith("k"))
        {
            float numericValue = float.Parse(value.Substring(0, value.Length - 1));
            return (int)(numericValue * 1000);
        }
        else if (value.EndsWith("M") || value.EndsWith("m"))
        {
            float numericValue = float.Parse(value.Substring(0, value.Length - 1));
            return (int)(numericValue * 1000000);
        }
        else
        {
            return int.Parse(value);
        }
    }

    public static bool CheckSpecialCharacters(string str)
    {
        foreach (char c in str)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return true;
            }
        }
        return false;
    }

    public static void ClearAllChild(Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static Vector2 CellSize(Transform transform, int count, int space)
    {
        RectTransform rectTransform = transform.parent.GetComponent<RectTransform>();
        var width = rectTransform.rect.width;
        var newWidth = ((width - (space * (count - 1))) / count) - 2;
        return new Vector2(newWidth, newWidth);
    }

    public static void RectTransformSize(Transform transform, Vector2 size)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
    }


}