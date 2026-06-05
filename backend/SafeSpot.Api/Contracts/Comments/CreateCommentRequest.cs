namespace SafeSpot.Api.Contracts.Comments;

public record CreateCommentRequest(long PostId, string Text);
