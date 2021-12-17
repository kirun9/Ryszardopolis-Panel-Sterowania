namespace RyszardopolisPanelSterowania.Controls;

using System;

public class DataChangedEventArgs : EventArgs
{
    public string DataName { get; private set; }
    public bool Value { get; private set; }

    public DataChangedEventArgs(string dataName, bool value)
    {
        DataName = dataName;
        Value = value;
    }
}
