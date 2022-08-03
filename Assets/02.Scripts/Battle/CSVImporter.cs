using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVImporter
{
    bool isOpened = false;
    TextAsset temp;
    List<string> lines;
    int ptr = 0;
    public bool OpenFile(string path)
    {
        ptr = 0;
        temp = Resources.Load<TextAsset>(path);
        if (temp == null)
            return false;
        else
        {
            isOpened = true;
            if (lines != null)
                lines.Clear();
            else
                lines = new List<string>();
            foreach(string line in temp.text.Split('\n'))
            {
                lines.Add(line);
            }
            return true;
        }
    }
    public void CloseFile()
    {
        if (!isOpened)
            return;
        else
        {
            ptr = 0;
            isOpened = false;
            temp = null;
            lines.Clear();
        }
            
    }
    public string ReadHeader()
    {
        if(!isOpened)
            return null;
        else
        {
            ptr++;
            return lines[0];
        }
    }

    public string Readline()
    {
        if (!isOpened)
            return null;
        else
        {
            return ptr == lines.Count ? null : lines[ptr++];
        }
    }

    public int CurLine()
    {
        return ptr;
    }


}
