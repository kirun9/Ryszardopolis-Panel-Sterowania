namespace RyszardopolisPanelSterowania;

using System.Drawing;

public struct StringLocation
{
    private ContentAlignment alignment = ContentAlignment.MiddleCenter;
    private float rotation;

    private float x;
    private float y;

    private float dX;
    private float dY;

    public float Rotation { get => rotation; set => rotation = value % 360; }

    public float X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public float DeltaX
    {
        get
        {
            return dX;
        }
        set
        {
            dX = value;
        }
    }

    public float DeltaY
    {
        get
        {
            return dY;
        }
        set
        {
            dY = value;
        }
    }

    public ContentAlignment Alignment { get => alignment; set => alignment = value; }
}
