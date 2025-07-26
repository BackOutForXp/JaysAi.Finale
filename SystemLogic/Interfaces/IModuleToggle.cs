// Neural v3.0 — IModuleToggle.cs
namespace JaysAi.Finale.SystemLogic.Interfaces
{
    public interface IModuleToggle
    {
        string ModuleName { get; }
        bool IsActive { get; set; }

        void Activate();
        void Deactivate();
    }
}
