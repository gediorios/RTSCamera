using MissionSharedLibrary.Config.HotKey;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace MissionLibrary.HotKey
{
    public interface IGameKeySequence
    {
        int Id { get; }
        string StringId { get; }
        string CategoryId { get; }

        bool Mandatory { get; }

        List<Key> Keys { get; }

        void SetGameKeys(List<InputKey> inputKeys);
        void ClearInvalidKeys();
        void ResetToDefault();

        bool IsKeyDownInOrder(IInputContext input = null);
        bool IsKeyPressedInOrder(IInputContext input = null);
        bool IsKeyReleasedInOrder(IInputContext input = null);
        bool IsKeyDown(IInputContext input = null);
        bool IsKeyPressed(IInputContext input = null);
        bool IsKeyReleased(IInputContext input = null);

        string ToSequenceString();
        SerializedGameKeySequence ToSerializedGameKeySequence();
    }
}
