import { api } from "./axios";

export const getCommentsByPostId = (postId: number) =>
  api.get(`/comments/post-${postId}`);

export const createComment = (data: { postId: number; text: string }) =>
  api.post("/comments", data);

export const updateComment = (data: { commentId: number; text: string }) =>
  api.put("/comments", data);

export const deleteComment = (commentId: number) =>
  api.delete(`/comments/${commentId}`);
