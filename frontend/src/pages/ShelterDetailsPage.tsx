import { useParams } from "react-router-dom";

export default function ShelterDetailsPage() {
  const { id } = useParams();

  return (
    <div className="min-h-screen bg-[#354F52] text-white p-6">
      Shelter Details: {id}
    </div>
  );
}