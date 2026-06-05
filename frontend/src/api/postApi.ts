import { api } from "./axios";

export const getPostsByShelterId = (shelterId: number) =>
  api.get(`/posts/shelter-${shelterId}`);

export const createPost = (data: { shelterId: number; text: string }) =>
  api.post("/posts", data);

export const updatePost = (data: { postId: number; text: string }) =>
  api.put("/posts", data);

export const deletePost = (postId: number) =>
  api.delete(`/posts/${postId}`);
