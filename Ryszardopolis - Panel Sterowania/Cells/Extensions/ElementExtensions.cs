namespace RyszardopolisPanelSterowania.Cells.Extensions
{
    [System.Diagnostics.DebuggerStepThrough]
    public static class ElementExtensions
    {
        public static Element[] ModifySize(this Element[] oldArr, int newSize)
        {
            Element[] newArr = new Element[newSize];
            if (newSize < oldArr.Length)
            {
                for (int i = 0; i < newSize; i++)
                {
                    newArr[i] = oldArr[i];
                }

                return newArr;
            }

            oldArr.CopyTo(newArr, 0);
            return newArr;
        }
    }
}
