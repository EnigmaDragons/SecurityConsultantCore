using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public interface IPathFinder
    {
        bool PathExists(XYZ start, XYZ end);
        Path GetPath(XYZ start, XYZ end);
    }
}