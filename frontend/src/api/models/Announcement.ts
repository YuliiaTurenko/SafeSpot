export type AnnouncementDto = {
  id: number;
  title: string;
  text: string;
  imageUrl?: string | null;
};

export type CreateAnnouncementRequest = {
  shelterId: number;
  title: string;
  text: string;
  imageUrl?: string | null;
}

export type UpdateAnnouncementRequest = {
  announcementId: number;
  title: string;
  text: string;
  imageUrl?: string | null;
}