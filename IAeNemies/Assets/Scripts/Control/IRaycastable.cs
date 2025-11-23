using System.Collections.Generic;

namespace Control
{
    public interface IRaycastable : IEnumerable<IRaycastable>
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}
