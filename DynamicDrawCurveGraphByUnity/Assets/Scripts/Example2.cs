using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Example2 : MonoBehaviour
{

    /// <summary>
    /// 图标背景色
    /// </summary>
    public Color32 GraphBackground;
    /// <summary>
    /// 图线颜色
    /// </summary>
    public Color32 LineColor;
    /// <summary>
    /// 呈现图片的UGUI
    /// </summary>
    public RawImage m_rawImage;
    /// <summary>
    /// 最终的颜色赋值一维数组
    /// </summary>
    private Color32[] pixelsDrawLine;
    private Color32[] pixelsBg;
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


    // Start is called before the first frame update
    void Start()
    {
        graphWidth = (int)m_rawImage.rectTransform.rect.width;
        graphHeight = (int)m_rawImage.rectTransform.rect.height;

        //初始化纹理
        m_texture = new Texture2D(graphWidth, graphHeight);

        m_rawImage.texture = m_texture;

        m_rawImage.SetNativeSize();

        pixelsDrawLine = new Color32[graphWidth * graphHeight];
        pixelsBg = new Color32[graphWidth * graphHeight];
        
        data = new List<int>();
        data.Add(0);

        for (int i = 0; i < pixelsDrawLine.Length; i++)
        {
            pixelsDrawLine[i] = GraphBackground;
            pixelsBg[i] = GraphBackground;
        }
        m_texture.SetPixels32(pixelsBg);
        m_texture.Apply();
    }

    public List<Vector2> listPoints;

    // Update is called once per frame
    void Update()
    {
        Array.Copy(pixelsBg, pixelsDrawLine, pixelsBg.Length);
 
        // 基准线
        DrawLine(new Vector2(0f, graphHeight * 0.5f), new Vector2(graphWidth, graphHeight * 0.5f), Color.blue);
 
        for (int i = 0; i < listPoints.Count-1; i++)
        {
            Vector2 from = listPoints[i];
            Vector2 to = listPoints[i + 1];
            DrawLine(from, to, LineColor);
        }
 
        m_texture.SetPixels32(pixelsDrawLine);
        m_texture.Apply();
    }

    void DrawLine(Vector2 from,Vector2 to, Color color)
    {
        int i;
        int j;
 
        if (Mathf.Abs(to.x - from.x) > Mathf.Abs(to.y - from.y))
        {
            // Horizontal line.
            i = 0;
            j = 1;
        }
        else
        {
            // Vertical line.
            i = 1;
            j = 0;
        }
 
        int x = (int)from[i];
        int delta = (int)Mathf.Sign(to[i] - from[i]);
        while (x != (int)to[i])
        {
            int y = (int)Mathf.Round(from[j] + (x - from[i]) * (to[j] - from[j]) / (to[i] - from[i]));
 
            int index;
            if (i == 0)
                index = y * graphWidth + x;
            else
                index = x * graphHeight + y;
 
            index = Mathf.Clamp(index, 0, pixelsDrawLine.Length - 1);
            pixelsDrawLine[index] = color;
 
            x += delta;
        }
    }
}
