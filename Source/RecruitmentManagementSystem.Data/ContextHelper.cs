using RecruitmentManagementSystem.Data.DbContext;
using RecruitmentManagementSystem.Model;

namespace RecruitmentManagementSystem.Data
{
    public static class ContextHelper
    {
        public static void ApplyStateChanges(this ApplicationDbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                var stateInfo = entry.Entity;
                entry.State = StateHelper.ConvertState(stateInfo.ObjectState);
            }
        }
    }
}