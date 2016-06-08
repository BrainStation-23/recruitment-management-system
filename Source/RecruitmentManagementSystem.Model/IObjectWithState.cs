namespace RecruitmentManagementSystem.Model
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }
    }

    public enum ObjectState
    {
        Added,
        Modified,
        Deleted,
        Unchanged
    }
}
