using System.Collections.Generic;
using UnityEngine;

public class GuiContainer
{
    struct Entry
    {
        public string description;
        public string value;

        public Entry(string _description)
        {
            description = _description;
            value = "-";
        }

        public string Content { get { return description + ": " + value; } }
    }

    public int Height { get; private set; } = 30;
    public int Width { get; private set; } = 250;

    // [TODO] expose as paramters
    private readonly int horizontalPadding = 5;
    private readonly int lineHeight = 20;
    private readonly int floatDecimals = 3;

    private readonly string name;
    private readonly Dictionary<int, Entry> data = new();

    public GuiContainer(string _name)
    {
        name = _name;
    }

    public void Draw(int x, int y)
    {
        GUI.Box(new Rect(x, y, Width, Height), name);

        int i = 1;
        foreach (KeyValuePair<int, Entry> item in data)
        {
            GUI.Label(new Rect(x + horizontalPadding, y + (lineHeight * i), Width - horizontalPadding, lineHeight), item.Value.Content);
            ++i;
        }
    }

    public void Add(int _id, string _description)
    {
        data.Add(_id, new Entry(_description));
        Height += 20;
    }

    private void UpdateData(int _id, string _formattedVal)
    {
        Entry newEntry = data[_id];
        newEntry.value = _formattedVal;
        data[_id] = newEntry;
    }

    public void UpdateVal(int _id, string _val)
    {
        UpdateData(_id, _val);
    }

    public void UpdateVal(int _id, int _val)
    {
        string formattedVal = _val.ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, float _val)
    {
        string formattedVal = System.Math.Round( _val, floatDecimals).ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, bool _val)
    {
        string formattedVal = _val.ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, Vector3 _val)
    {
        string formattedVal = _val.ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, Vector2 _val)
    {
        string formattedVal = _val.ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, Vector4 _val)
    {
        string formattedVal = _val.ToString();
        UpdateData(_id, formattedVal);
    }

    public void UpdateVal(int _id, Transform _val)
    {
        string formattedVal = _val.name;
        UpdateData(_id, formattedVal);
    }
}
