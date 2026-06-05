namespace SafeSpot.Api.Contracts.Comments;

public record UpdateCommentRequest(long CommentId, string Text);
