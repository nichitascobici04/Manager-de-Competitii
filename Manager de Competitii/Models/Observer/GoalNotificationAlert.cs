namespace Manager_de_Competitii.Models.Observer
{
    public class GoalNotificationAlert : IMatchObserver
    {
        public void Update(LiveMatch matchContext)
        {
            Console.WriteLine($"[GoalNotificationAlert] ALERT DISPATCHED: {matchContext.LastEvent}");
        }
    }
}
