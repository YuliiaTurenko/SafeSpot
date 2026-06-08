export type Comment = {
  id: number;
  userId: number | null;
  userName: string | null;
  postId: number;
  text: string;
  createdAt: string;
};