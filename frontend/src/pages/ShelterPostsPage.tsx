import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { getPostsByShelterId, createPost, updatePost, deletePost } from "../api/postApi";
import { getCommentsByPostId, createComment, updateComment, deleteComment } from "../api/commentApi";
import { getShelterById } from "../api/shelterApi";
import LanguageButton from "../components/LanguageButton";
import { useTranslation } from "react-i18next";
import { jwtDecode } from "jwt-decode";

interface Post {
  id: number;
  userId: number;
  userName: string | null;
  shelterId: number;
  text: string;
  createdAt: string;
}

interface Comment {
  id: number;
  userId: number | null;
  userName: string | null;
  postId: number;
  text: string;
  createdAt: string;
}

export default function ShelterPostsPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const [posts, setPosts] = useState<Post[]>([]);
  const [shelter, setShelter] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [userRole, setUserRole] = useState<string | null>(null);
  const [currentUserId, setCurrentUserId] = useState<number | null>(null);

  const [postModalOpen, setPostModalOpen] = useState(false);
  const [editingPost, setEditingPost] = useState<Post | null>(null);
  const [postText, setPostText] = useState("");

  const [commentModalOpen, setCommentModalOpen] = useState(false);
  const [selectedPostId, setSelectedPostId] = useState<number | null>(null);
  const [comments, setComments] = useState<Comment[]>([]);
  const [commentText, setCommentText] = useState("");
  const [editingComment, setEditingComment] = useState<Comment | null>(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      const decoded: any = jwtDecode(token);
      const roles = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      const userId = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      
      if (Array.isArray(roles)) {
        setUserRole(roles[0]);
      } else {
        setUserRole(roles);
      }
      
      setCurrentUserId(parseInt(userId));
    }
  }, []);

  useEffect(() => {
    if (!id) return;
    loadPosts();
    loadShelter();
  }, [id]);

  const loadPosts = async () => {
    try {
      const res = await getPostsByShelterId(Number(id));
      setPosts(res.data);
    } finally {
      setLoading(false);
    }
  };

  const loadShelter = async () => {
    const res = await getShelterById(Number(id));
    setShelter(res.data);
  };

  const loadComments = async (postId: number) => {
    const res = await getCommentsByPostId(postId);
    setComments(res.data);
  };

  const handleCreatePost = async () => {
    if (!postText.trim()) return;

    await createPost({ shelterId: Number(id), text: postText });
    
    setPostText("");
    setPostModalOpen(false);
    loadPosts();
  };

  const handleUpdatePost = async () => {
    if (!editingPost || !postText.trim()) return;
    
    await updatePost({ postId: editingPost.id, text: postText });
    
    setEditingPost(null);
    setPostText("");
    setPostModalOpen(false);
    loadPosts();
  };

  const handleDeletePost = async (postId: number) => {
    if (!confirm(t("deleteConfirm"))) return;
    
    await deletePost(postId);
    loadPosts();
  };

  const handleOpenComments = async (postId: number) => {
    setSelectedPostId(postId);
    await loadComments(postId);
    setCommentModalOpen(true);
  };

  const handleCreateComment = async () => {
    if (!commentText.trim() || !selectedPostId) return;
    
    await createComment({ postId: selectedPostId, text: commentText });
    
    setCommentText("");
    loadComments(selectedPostId);
  };

  const handleUpdateComment = async () => {
    if (!editingComment || !commentText.trim()) return;
    
    await updateComment({ commentId: editingComment.id, text: commentText });
    
    setEditingComment(null);
    setCommentText("");
    loadComments(selectedPostId!);
  };

  const handleDeleteComment = async (commentId: number) => {
    if (!confirm(t("deleteConfirm"))) return;
    
    await deleteComment(commentId);
    loadComments(selectedPostId!);
  };

  const canCreatePost = userRole === "User";

  if (loading) {
    return (
      <div className="min-h-screen bg-[#354F52] text-white p-10">
        {t("loading")}
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#354F52] text-white p-6">
      <div className="flex justify-between items-center mb-6">
        <div>
          <button
            onClick={() => navigate(-1)}
            className="bg-[#52796F] hover:bg-[#2F3E46] text-white px-4 py-2 rounded-lg font-medium shadow-md transition-all mb-4"
          >
            ← {t("back")}
          </button>
          <h1 className="text-3xl font-bold">
            {shelter?.address} - {t("posts")}
          </h1>
        </div>

        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>
      </div>

      {canCreatePost && (
        <button
          onClick={() => {
            setEditingPost(null);
            setPostText("");
            setPostModalOpen(true);
          }}
          className="bg-[#84A98C] hover:bg-[#6B9080] text-white px-4 py-2 rounded-lg font-medium transition-all mb-6"
        >
          {t("createPost")}
        </button>
      )}

      <div className="space-y-4">
        {posts.map((post) => (
          <div key={post.id} className="bg-[#2F3E46] p-5 rounded-xl">
            <div className="flex justify-between items-start mb-3">
              <div>
                <p className="text-sm text-[#CAD2C5]">
                  {post.userName || "Anonymous"} • {new Date(post.createdAt).toLocaleString()}
                </p>
                <p className="mt-2">{post.text}</p>
              </div>

              {post.userId === currentUserId && (
                <div className="flex gap-2">
                  <button
                    onClick={() => {
                      setEditingPost(post);
                      setPostText(post.text);
                      setPostModalOpen(true);
                    }}
                    className="bg-[#678ABE] hover:bg-[#5C858D] text-white px-3 py-1 rounded text-sm transition-colors"
                  >
                    {t("edit")}
                  </button>
                  <button
                    onClick={() => handleDeletePost(post.id)}
                    className="bg-red-600 hover:bg-red-700 text-white px-3 py-1 rounded text-sm transition-colors"
                  >
                    {t("delete")}
                  </button>
                </div>
              )}
            </div>

            <button
              onClick={() => handleOpenComments(post.id)}
              className="text-[#84A98C] hover:text-[#6B9080] text-sm"
            >
              {t("viewComments")} ({comments.filter(c => c.postId === post.id).length})
            </button>
          </div>
        ))}

        {posts.length === 0 && <p className="text-[#CAD2C5]">{t("noPosts")}</p>}
      </div>

      {postModalOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
          <div className="bg-[#2F3E46] p-6 rounded-xl w-full max-w-md">
            <h2 className="text-2xl font-bold mb-4">
              {editingPost ? t("editPost") : t("createPost")}
            </h2>
            <textarea
              value={postText}
              onChange={(e) => setPostText(e.target.value)}
              className="w-full bg-[#354F52] text-white p-3 rounded-lg border border-[#52796F]/30 min-h-[120px]"
              placeholder={t("enterText")}
            />
            <div className="flex gap-3 mt-4">
              <button
                onClick={editingPost ? handleUpdatePost : handleCreatePost}
                className="flex-1 bg-[#84A98C] hover:bg-[#6B9080] text-white px-4 py-2 rounded-lg font-medium transition-all"
              >
                {editingPost ? t("update") : t("create")}
              </button>
              <button
                onClick={() => {
                  setPostModalOpen(false);
                  setEditingPost(null);
                  setPostText("");
                }}
                className="flex-1 bg-[#52796F] hover:bg-[#2F3E46] text-white px-4 py-2 rounded-lg font-medium transition-all"
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {commentModalOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
          <div className="bg-[#2F3E46] p-6 rounded-xl w-full max-w-lg max-h-[80vh] overflow-y-auto">
            <div className="flex justify-between items-center mb-4">
              <h2 className="text-2xl font-bold">{t("comments")}</h2>
              <button
                onClick={() => {
                  setCommentModalOpen(false);
                  setSelectedPostId(null);
                  setComments([]);
                  setEditingComment(null);
                  setCommentText("");
                }}
                className="text-[#CAD2C5] hover:text-white"
              >
                ✕
              </button>
            </div>

            <div className="space-y-3 mb-4">
              {comments.map((comment) => (
                <div key={comment.id} className="bg-[#354F52] p-3 rounded-lg">
                  <div className="flex justify-between items-start">
                    <div>
                      <p className="text-sm text-[#CAD2C5]">
                        {comment.userName || "Anonymous"} • {new Date(comment.createdAt).toLocaleString()}
                      </p>
                      <p className="mt-1">{comment.text}</p>
                    </div>

                    {comment.userId === currentUserId && (
                      <div className="flex gap-2">
                        <button
                          onClick={() => {
                            setEditingComment(comment);
                            setCommentText(comment.text);
                          }}
                          className="bg-[#678ABE] hover:bg-[#5C858D] text-white px-2 py-1 rounded text-xs transition-colors"
                        >
                          {t("edit")}
                        </button>
                        <button
                          onClick={() => handleDeleteComment(comment.id)}
                          className="bg-red-600 hover:bg-red-700 text-white px-2 py-1 rounded text-xs transition-colors"
                        >
                          {t("delete")}
                        </button>
                      </div>
                    )}
                  </div>
                </div>
              ))}

              {comments.length === 0 && <p className="text-[#CAD2C5]">{t("noComments")}</p>}
            </div>

            <div className="border-t border-[#52796F]/30 pt-4">
              <textarea
                value={commentText}
                onChange={(e) => setCommentText(e.target.value)}
                className="w-full bg-[#354F52] text-white p-3 rounded-lg border border-[#52796F]/30 min-h-[80px]"
                placeholder={t("enterComment")}
              />
              <button
                onClick={editingComment ? handleUpdateComment : handleCreateComment}
                className="w-full mt-3 bg-[#84A98C] hover:bg-[#6B9080] text-white px-4 py-2 rounded-lg font-medium transition-all"
              >
                {editingComment ? t("update") : t("send")}
              </button>
              {editingComment && (
                <button
                  onClick={() => {
                    setEditingComment(null);
                    setCommentText("");
                  }}
                  className="w-full mt-2 bg-[#52796F] hover:bg-[#2F3E46] text-white px-4 py-2 rounded-lg font-medium transition-all"
                >
                  {t("cancel")}
                </button>
              )}
            </div>
          </div>
        </div>
      )}
    </div>
  );
}