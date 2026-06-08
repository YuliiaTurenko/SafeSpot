import { useEffect, useState } from "react";
import {
  createAnnouncement,
  deleteAnnouncement,
  getAnnouncementsByShelterId,
  updateAnnouncement,
} from "../api/announcementApi";
import { useTranslation } from "react-i18next";

interface Props {
  shelterId: number;
}

export default function AnnouncementList({ shelterId }: Props) {
  const { t } = useTranslation();
  const [announcements, setAnnouncements] = useState<any[]>([]);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [title, setTitle] = useState("");
  const [text, setText] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [currentPage, setCurrentPage] = useState(1);

  const itemsPerPage = 4;

  const load = async () => {
    const res = await getAnnouncementsByShelterId(shelterId);
    setAnnouncements(res.data);
  };

  useEffect(() => {
    setCurrentPage(1);
    load();
  }, [shelterId]);

  const clearForm = () => {
    setTitle("");
    setText("");
    setImageUrl("");
    setEditingId(null);
  };

  const handleSubmit = async () => {
    if (editingId !== null) {
      await updateAnnouncement({
        announcementId: editingId,
        title,
        text,
        imageUrl,
      });
    } else {
      await createAnnouncement({
        shelterId,
        title,
        text,
        imageUrl,
      });
    }

    clearForm();
    load();
  };

  const handleEdit = (a: any) => {
    setEditingId(a.id);

    setTitle(a.title);
    setText(a.text);

    setImageUrl(a.imageUrl || "");
  };

  const handleDelete = async (id: number) => {
    await deleteAnnouncement(id);
    load();
  };

  const totalPages = Math.ceil(announcements.length / itemsPerPage);

  const pagedAnnouncements = announcements.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage,
  );

  return (
    <div className="mt-10">
      <div className="bg-[#151A3C] rounded-2xl p-5">
        <h2 className="text-2xl font-semibold mb-5">{t("announcements")}</h2>

        <div className="bg-[#2F3E46] p-5 rounded-2xl mb-6">
          <div className="space-y-4">
            <input
              placeholder={t("title")}
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              className="w-full p-3 rounded bg-[#354F52]"
            />

            <textarea
              placeholder={t("content")}
              value={text}
              onChange={(e) => setText(e.target.value)}
              className="w-full p-3 rounded bg-[#354F52] h-32"
            />

            <input
              placeholder={t("imageUrl")}
              value={imageUrl}
              onChange={(e) => setImageUrl(e.target.value)}
              className="w-full p-3 rounded bg-[#354F52]"
            />
          </div>

          <div className="flex gap-3 mt-4">
            <button
              onClick={handleSubmit}
              className="bg-[#84A98C] px-4 py-2 rounded"
            >
              {editingId ? t("save") : t("create")}
            </button>

            {editingId && (
              <button
                onClick={clearForm}
                className="bg-gray-500 px-4 py-2 rounded"
              >
                {t("cancel")}
              </button>
            )}
          </div>
        </div>

        <div className="space-y-4">
          {pagedAnnouncements.map((a) => (
            <div key={a.id} className="bg-[#2F3E46] p-4 rounded-xl">
              <div className="flex justify-between gap-5">
                <div className="flex-1">
                  <h3 className="text-xl font-semibold">{a.title}</h3>

                  <p className="mt-2 text-gray-300">{a.text}</p>

                  {a.imageUrl && (
                    <img
                      src={a.imageUrl}
                      alt={a.title}
                      className="mt-4 rounded-xl max-h-[250px]"
                    />
                  )}
                </div>

                <div className="flex flex-col gap-2">
                  <button
                    onClick={() => handleEdit(a)}
                    className="bg-[#678ABE] hover:bg-[#5C858D] text-white px-3 py-2 rounded transition-colors"
                  >
                    {t("edit")}
                  </button>

                  <button
                    onClick={() => handleDelete(a.id)}
                    className="bg-red-500 hover:bg-red-600 px-3 py-2 rounded transition-colors"
                  >
                    {t("delete")}
                  </button>
                </div>
              </div>
            </div>
          ))}
          {totalPages > 1 && (
            <div className="flex justify-center items-center gap-2 mt-6">
              <button
                disabled={currentPage === 1}
                onClick={() => setCurrentPage((p) => p - 1)}
                className="bg-[#354F52] px-4 py-2 rounded disabled:opacity-50"
              >
                ←
              </button>

              <span>
                {currentPage} / {totalPages}
              </span>

              <button
                disabled={currentPage === totalPages}
                onClick={() => setCurrentPage((p) => p + 1)}
                className="bg-[#354F52] px-4 py-2 rounded disabled:opacity-50"
              >
                →
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
