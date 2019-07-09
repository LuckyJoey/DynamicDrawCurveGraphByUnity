using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTGraph : MonoBehaviour
{

    /// <summary>
    /// 图标背景色
    /// </summary>
    public Color GraphBackground;
    /// <summary>
    /// 图线颜色
    /// </summary>
    public Color LineColor;
    /// <summary>
    /// 呈现图片的UGUI
    /// </summary>
    public RawImage m_rawImage;
    /// <summary>
    /// 最终的颜色赋值一维数组
    /// </summary>
    private Color[] pixels;
    /// <summary>
    /// 呈现图线的纹理
    /// </summary>
    private Texture2D m_texture;
    /// <summary>
    /// 数据记录包
    /// </summary>
    private List<int> data;
    /// <summary>
    /// 图表宽度
    /// </summary>
    private int graphWidth;
    /// <summary>
    /// 图表高度
    /// </summary>
    private int graphHeight;
    /// <summary>
    /// 控制数值的滑动条
    /// </summary>
    public Slider m_slider;

    // Start is called before the first frame update
    void Start()
    {
        graphWidth = (int)m_rawImage.rectTransform.rect.width;
        graphHeight = (int)m_rawImage.rectTransform.rect.height;

        //初始化纹理
        m_texture = new Texture2D(graphWidth, graphHeight);

        m_rawImage.texture = m_texture;

        m_rawImage.SetNativeSize();

        pixels = new Color[graphWidth * graphHeight];

        data = new List<int>();
        data.Add(0);

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = GraphBackground;
        }
        m_texture.SetPixels(pixels);
        m_texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        drawLine((int)(m_slider.value* graphHeight));
    }

    void drawLine(int datapoint)
    {
        data.Add(datapoint);

        for (int j = data.Count - 1; j >= Mathf.Max(0, data.Count - graphWidth); j--)
        {
            pixels[data[j] * graphWidth + graphWidth - data.Count + j] = LineColor;
        }
        m_texture.SetPixels(pixels);
        m_texture.Apply();
        for (int j = data.Count - 1; j >= Mathf.Max(0, data.Count - graphWidth); j--)
        {
            pixels[data[j] * graphWidth + graphWidth - data.Count + j] = GraphBackground;
        }
    }
}
