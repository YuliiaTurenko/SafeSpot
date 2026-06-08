export type Post = {
  id: number;
  userId: number;
  userName: string | null;
  shelterId: number;
  text: string;
  createdAt: string;
};