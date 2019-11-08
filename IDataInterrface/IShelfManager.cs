using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IShelfManager
    {
        void AddShelf(int shelfNumber);
        Shelf GetShelfByShelfNumber(int shelfNumber);
        void RemoveShelf(int shelfID);
        void MoveShelf(int shelfID, int sectionID);
    }
}
