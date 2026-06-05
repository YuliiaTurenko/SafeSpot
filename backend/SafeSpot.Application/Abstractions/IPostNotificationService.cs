namespace SafeSpot.Application.Abstractions;

public interface IPostNotificationService
{
    Task NotifyNewPostAsync(long shelterId, object post);
    Task NotifyNewCommentAsync(long shelterId, object comment);
}
