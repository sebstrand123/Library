using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface ISectionManager
    {
        Section GetSectionBySectionNumber(int sectionNumber);
        void AddSection(int sectionNumber);
        void RemoveSection(int sectionID);
        List<Section> GetScrapList();
    }
}
