using PodgórzynPanelSterowania.Controls.Cells;

namespace PodgórzynPanelSterowania.Controls
{
    public static class ElementExtensions
    {
        public static NewElement[] ModifySize(this NewElement[] oldArr, int newSize)
        {
            NewElement[] newArr = new NewElement[newSize];
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
